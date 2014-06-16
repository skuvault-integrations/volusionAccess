using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using VolusionAccess.Misc;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Order;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionOrdersService : IVolusionOrdersService
	{
		private readonly WebRequestServices _webRequestServices;

		public VolusionOrdersService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
		}

		public VolusionOrder GetOrder( int orderId )
		{
			var orders = this.GetFilteredOrders( OrderColumns.OrderId, orderId );
			return orders.FirstOrDefault();
		}

		public async Task< VolusionOrder > GetOrderAsync( int orderId )
		{
			var orders = await this.GetFilteredOrdersAsync( OrderColumns.OrderId, orderId );
			return orders.FirstOrDefault();
		}

		public IEnumerable< VolusionOrder > GetNewOrUpdatedOrders()
		{
			return GetFilteredNewOrUpdatedOrders( x => true ).ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync()
		{
			var orders = await GetFilteredNewOrUpdatedOrdersAsync( x => true );
			return orders.ToList();
		}

		public IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = GetFilteredNewOrUpdatedOrders( x => ( x.OrderDateUtc >= startDateUtc && x.OrderDateUtc <= endDateUtc ) ||
			                                                 ( x.LastModifiedUtc >= startDateUtc && x.LastModifiedUtc <= endDateUtc ) );
			return orders.ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = await GetFilteredNewOrUpdatedOrdersAsync( x => ( x.OrderDateUtc >= startDateUtc && x.OrderDateUtc <= endDateUtc ) ||
			                                                            ( x.LastModifiedUtc >= startDateUtc && x.LastModifiedUtc <= endDateUtc ) );
			return orders.ToList();
		}

		public IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var statuses = GetAllStatusesExceptShippedAndCancelled();
			var orders = new HashSet< VolusionOrder >();
			foreach( var status in statuses )
			{
				var ordersPortion = this.GetFilteredOrders( OrderColumns.OrderStatus, status.ToString() ).ToList();
				var filtered = ordersPortion.Where( x => ( x.OrderDateUtc >= startDateUtc && x.OrderDateUtc <= endDateUtc ) ||
				                                         ( x.LastModifiedUtc >= startDateUtc && x.LastModifiedUtc <= endDateUtc ) );
				this.AddOrders( orders, filtered );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var statuses = GetAllStatusesExceptShippedAndCancelled();
			var orders = new HashSet< VolusionOrder >();
			var tasks = new List< Task< IEnumerable< VolusionOrder > > >();
			foreach( var status in statuses )
			{
				tasks.Add( this.GetFilteredOrdersAsync( OrderColumns.OrderStatus, status.ToString() ) );
			}
			await Task.WhenAll( tasks ).ConfigureAwait( false );

			foreach( var task in tasks )
			{
				var ordersPortion = task.Result.ToList();
				var filtered = ordersPortion.Where( x => ( x.OrderDateUtc >= startDateUtc && x.OrderDateUtc <= endDateUtc ) ||
				                                         ( x.LastModifiedUtc >= startDateUtc && x.LastModifiedUtc <= endDateUtc ) );
				this.AddOrders( orders, filtered );
			}

			return orders;
		}

		public IEnumerable< VolusionOrder > GetFinishedOrders( IEnumerable< int > ordersIds )
		{
			var orders = new List< VolusionOrder >();
			foreach( var orderId in ordersIds )
			{
				var order = this.GetOrder( orderId );
				if( order.OrderStatus == VolusionOrderStatusEnum.Shipped || order.OrderStatus == VolusionOrderStatusEnum.Cancelled )
					orders.Add( order );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetFinishedOrdersAsync( IEnumerable< int > ordersIds )
		{
			var orders = new List< VolusionOrder >();
			foreach( var orderId in ordersIds )
			{
				var order = await this.GetOrderAsync( orderId );
				if( order.OrderStatus == VolusionOrderStatusEnum.Shipped || order.OrderStatus == VolusionOrderStatusEnum.Cancelled )
					orders.Add( order );
			}

			return orders;
		}

		#region Misc
		private IEnumerable< VolusionOrder > GetFilteredOrders( OrderColumns column, object value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value );

			var ordersPortion = ActionPolicies.Get.Get( () => this._webRequestServices.GetResponse< VolusionOrders >( endpoint ) );
			if( ordersPortion != null && ordersPortion.Orders != null )
				orders.AddRange( ordersPortion.Orders );

			return orders;
		}

		private async Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value );

			var ordersPortion = await ActionPolicies.GetAsync.Get( () => this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint ) );
			if( ordersPortion != null && ordersPortion.Orders != null )
				orders.AddRange( ordersPortion.Orders );

			return orders;
		}

		private IEnumerable< VolusionOrder > GetFilteredNewOrUpdatedOrders( Func< VolusionOrder, bool > predicate )
		{
			var orders = new HashSet< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

			while( true )
			{
				var ordersPortion = ActionPolicies.Get.Get( () => this._webRequestServices.GetResponse< VolusionOrders >( endpoint ) );
				if( ordersPortion == null || ordersPortion.Orders == null || ordersPortion.Orders.Count == 0 )
					return orders;

				this.AddOrders( orders, ordersPortion.Orders.Where( predicate ) );
			}
		}

		private async Task< IEnumerable< VolusionOrder > > GetFilteredNewOrUpdatedOrdersAsync( Func< VolusionOrder, bool > predicate )
		{
			var orders = new HashSet< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

			while( true )
			{
				var ordersPortion = await ActionPolicies.GetAsync.Get( () => this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint ) );
				if( ordersPortion == null || ordersPortion.Orders == null || ordersPortion.Orders.Count == 0 )
					return orders;

				this.AddOrders( orders, ordersPortion.Orders.Where( predicate ) );
			}
		}

		private void AddOrders( HashSet< VolusionOrder > processedOrders, IEnumerable< VolusionOrder > fetchedOrdersPartition )
		{
			foreach( var order in fetchedOrdersPartition )
			{
				if( processedOrders.Contains( order ) )
				{
					var oldOrder = processedOrders.FirstOrDefault( x => x.Id == order.Id && x.LastModified <= order.LastModified );
					if( oldOrder != null )
						processedOrders.Remove( oldOrder );
				}

				processedOrders.Add( order );
			}
		}

		private IEnumerable< VolusionOrderStatusEnum > GetAllStatusesExceptShippedAndCancelled()
		{
			return new List< VolusionOrderStatusEnum >
			{
				VolusionOrderStatusEnum.New,
				VolusionOrderStatusEnum.Pending,
				VolusionOrderStatusEnum.Processing,
				VolusionOrderStatusEnum.PaymentDeclined,
				VolusionOrderStatusEnum.AwaitingPayment,
				VolusionOrderStatusEnum.ReadyToShip,
				VolusionOrderStatusEnum.PendingShipment,
				VolusionOrderStatusEnum.PartiallyShipped,
				VolusionOrderStatusEnum.PartiallyBackordered,
				VolusionOrderStatusEnum.Backordered,
				VolusionOrderStatusEnum.SeeLineItems,
				VolusionOrderStatusEnum.SeeOrderNotes,
				VolusionOrderStatusEnum.PartiallyReturned,
				VolusionOrderStatusEnum.Returned
			};
		}
		#endregion misc
	}
}
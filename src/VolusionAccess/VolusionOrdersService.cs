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
			var dateRange = new VolusionDateRange( startDateUtc, endDateUtc );
			var orders = GetFilteredNewOrUpdatedOrders( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, dateRange ) );
			return orders.ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var dateRange = new VolusionDateRange( startDateUtc, endDateUtc );
			var orders = await GetFilteredNewOrUpdatedOrdersAsync( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, dateRange ) );
			return orders.ToList();
		}

		public IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var dateRange = new VolusionDateRange( startDateUtc, endDateUtc );
			var notFinishedStatuses = this.GetNotFinishedStatuses();
			var orders = new HashSet< VolusionOrder >();
			foreach( var status in notFinishedStatuses )
			{
				var ordersPortion = this.GetFilteredOrders( OrderColumns.OrderStatus, status ).ToList();
				var filtered = ordersPortion.Where( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, dateRange ) );
				this.AddOrders( orders, filtered );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var dateRange = new VolusionDateRange( startDateUtc, endDateUtc );
			var notFinishedStatuses = this.GetNotFinishedStatuses();
			var orders = new HashSet< VolusionOrder >();
			var tasks = new List< Task< IEnumerable< VolusionOrder > > >();
			foreach( var status in notFinishedStatuses )
			{
				tasks.Add( this.GetFilteredOrdersAsync( OrderColumns.OrderStatus, status ) );
			}
			await Task.WhenAll( tasks ).ConfigureAwait( false );

			foreach( var task in tasks )
			{
				var ordersPortion = task.Result.ToList();
				var filtered = ordersPortion.Where( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, dateRange ) );
				this.AddOrders( orders, filtered );
			}

			return orders;
		}

		public IEnumerable< VolusionOrder > GetFinishedOrders( IEnumerable< int > ordersIds )
		{
			var finishedStatuses = this.GetFinishedStatuses();
			var orders = new List< VolusionOrder >();
			foreach( var orderId in ordersIds )
			{
				var order = this.GetOrder( orderId );
				if( finishedStatuses.Contains( order.OrderStatus.ToString() ) )
					orders.Add( order );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetFinishedOrdersAsync( IEnumerable< int > ordersIds )
		{
			var orders = new List< VolusionOrder >();
			var tasks = new List< Task< VolusionOrder > >();
			foreach( var orderId in ordersIds )
			{
				tasks.Add( this.GetOrderAsync( orderId ) );
				if( tasks.Count >= 10 )
				{
					await this.AddFinishedOrdersAsync( orders, tasks );
					tasks = new List< Task< VolusionOrder > >();
				}
			}

			if( tasks.Count > 0 )
				await this.AddFinishedOrdersAsync( orders, tasks );

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

		private async Task AddFinishedOrdersAsync( List< VolusionOrder > orders, List< Task< VolusionOrder > > tasks )
		{
			var finishedStatuses = this.GetFinishedStatuses();
			await Task.WhenAll( tasks ).ConfigureAwait( false );
			orders.AddRange( tasks.Select( t => t.Result )
				.Where( o => o != null && finishedStatuses.Contains( o.OrderStatus.ToString() ) ) );
		}

		private bool DoesOrderCreatedOrUpdatedInDateRange( VolusionOrder order, VolusionDateRange dateRange )
		{
			if( order.OrderDateUtc != DateTime.MinValue )
			{
				return ( order.OrderDateUtc >= dateRange.StartDateUtc && order.OrderDateUtc <= dateRange.EndDateUtc ) ||
				       ( order.LastModifiedUtc >= dateRange.StartDateUtc && order.LastModifiedUtc <= dateRange.EndDateUtc );
			}
			else
			{
				return ( order.OrderDate >= dateRange.ReservedStartDate && order.OrderDate <= dateRange.ReservedEndDate ) ||
				       ( order.LastModified >= dateRange.ReservedStartDate && order.LastModified <= dateRange.ReservedEndDate );
			}
		}

		private IEnumerable< String > GetNotFinishedStatuses()
		{
			return new List< String >
			{
				VolusionOrderStatusEnum.New.ToString(),
				VolusionOrderStatusEnum.Pending.ToString(),
				VolusionOrderStatusEnum.Processing.ToString(),
				"Payment Declined", //VolusionOrderStatusEnum.PaymentDeclined,
				"Awaiting Payment", //VolusionOrderStatusEnum.AwaitingPayment,
				"Ready To Ship", //VolusionOrderStatusEnum.ReadyToShip,
				"Pending Shipment", //VolusionOrderStatusEnum.PendingShipment,
				"Partially Shipped", //VolusionOrderStatusEnum.PartiallyShipped,
				"Partially Backordered", //VolusionOrderStatusEnum.PartiallyBackordered,
				VolusionOrderStatusEnum.Backordered.ToString(),
				"See Line Items", //VolusionOrderStatusEnum.SeeLineItems,
				"See Order Notes" //VolusionOrderStatusEnum.SeeOrderNotes,
			};
		}

		private IList< String > GetFinishedStatuses()
		{
			return new List< String >
			{
				VolusionOrderStatusEnum.Shipped.ToString(),
				VolusionOrderStatusEnum.Cancelled.ToString(),
				"Partially Returned", //VolusionOrderStatusEnum.PartiallyReturned,
				VolusionOrderStatusEnum.Returned.ToString()
			};
		}
		#endregion misc

		private class VolusionDateRange
		{
			public DateTime StartDateUtc { get; private set; }
			public DateTime EndDateUtc { get; private set; }
			public DateTime ReservedStartDate { get; private set; }
			public DateTime ReservedEndDate { get; private set; }

			public VolusionDateRange( DateTime startDateUtc, DateTime endDateUtc )
			{
				this.StartDateUtc = startDateUtc.AddSeconds( -startDateUtc.Second );
				this.EndDateUtc = endDateUtc;
				this.ReservedStartDate = this.StartDateUtc.AddHours( -12 );
				this.ReservedEndDate = this.EndDateUtc.AddHours( -12 );
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Netco.Extensions;
using VolusionAccess.Misc;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Order;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionOrdersService: IVolusionOrdersService
	{
		private readonly VolusionConfig _config;
		private readonly WebRequestServices _webRequestServices;

		private readonly IEnumerable< string > OpenOrdersStatuses = new List< string >
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
			"See Order Notes", //VolusionOrderStatusEnum.SeeOrderNotes,
			"New - See Order Notes",
			"Awaiting Payment - See Order Notes"
		};

		private readonly IList< string > FinishedStatuses = new List< string >
		{
			VolusionOrderStatusEnum.Shipped.ToString(),
			VolusionOrderStatusEnum.Cancelled.ToString(),
			"Cancel Order", //VolusionOrderStatusEnum.CancelOrder,
			"Partially Returned", //VolusionOrderStatusEnum.PartiallyReturned,
			VolusionOrderStatusEnum.Returned.ToString()
		};

		public VolusionOrdersService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._config = config;
			this._webRequestServices = new WebRequestServices( config );
		}

		#region GetOrder
		public VolusionOrder GetOrder( int orderId, bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			var orders = this.GetFilteredOrders( OrderColumns.OrderId, orderId, marker, isAddOrderComments );
			return orders.FirstOrDefault();
		}

		public async Task< VolusionOrder > GetOrderAsync( int orderId, bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			var orders = await this.GetFilteredOrdersAsync( OrderColumns.OrderId, orderId, marker, isAddOrderComments );
			return orders.FirstOrDefault();
		}
		#endregion

		#region GetNewOrUpdatedOrders
		public IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			return this.GetFilteredNewOrUpdatedOrders( x => true, marker, isAddOrderComments ).ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			var orders = await this.GetFilteredNewOrUpdatedOrdersAsync( x => true, marker, isAddOrderComments );
			return orders.ToList();
		}
		#endregion

		#region GetNewOrUpdatedOrders by date range
		public IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments )
		{
			startDateUtc = startDateUtc.AddMinutes( -1 );
			var marker = this.GetMarker();
			var orders = this.GetFilteredNewOrUpdatedOrders( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, startDateUtc, endDateUtc ), marker, isAddOrderComments );
			return orders.ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments )
		{
			startDateUtc = startDateUtc.AddMinutes( -1 );
			var marker = this.GetMarker();
			var orders = await this.GetFilteredNewOrUpdatedOrdersAsync( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, startDateUtc, endDateUtc ), marker, isAddOrderComments );
			return orders.ToList();
		}
		#endregion

		#region GetOpenOrders
		// [ Obsolete( "Because method filter server responses by date, instead of requesting orders in date range" ) ] TODO: make it obsolete, when we will completely rid of this
		public IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments )
		{
			startDateUtc = startDateUtc.AddMinutes( -1 );
			var marker = this.GetMarker();
			var orders = new HashSet< VolusionOrder >();
			foreach( var status in this.OpenOrdersStatuses )
			{
				var ordersPortion = this.GetFilteredOrders( OrderColumns.OrderStatus, status, marker, isAddOrderComments ).ToList();
				var filtered = ordersPortion.Where( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, startDateUtc, endDateUtc ) );
				this.AddOrders( orders, filtered );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetOpenOrdersAsync( HashSet< string > includeColumns, HashSet< string > includeColumnsDetails )
		{
			var marker = this.GetMarker();
			var orders = new HashSet< VolusionOrder >();
			foreach( var status in this.OpenOrdersStatuses )
			{
				var ordersPortion = ( await this.GetFilteredOrdersAsync( OrderColumns.OrderStatus, status, marker, includeColumns, includeColumnsDetails ) ).ToList();
				this.AddOrders( orders, ordersPortion );
			}

			return orders;
		}

		// [ Obsolete( "Because method filter server responses by date, instead of requesting orders in date range" )] TODO: make it obsolete, when we will completely rid of this
		public async Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments )
		{
			startDateUtc = startDateUtc.AddMinutes( -1 );
			var marker = this.GetMarker();
			var orders = new HashSet< VolusionOrder >();

			var filteredByStatus = await this.OpenOrdersStatuses.ProcessInBatchAsync( 15, async status =>
			{
				var ordersPortion = await this.GetFilteredOrdersAsync( OrderColumns.OrderStatus, status, marker, isAddOrderComments );
				ordersPortion = ordersPortion.Where( x => this.DoesOrderCreatedOrUpdatedInDateRange( x, startDateUtc, endDateUtc ) ).ToList();
				return ordersPortion;
			} );

			foreach( var filtered in filteredByStatus )
				this.AddOrders( orders, filtered );

			//TODO: Remove it if all works fine
			VolusionLogger.Log.Trace( "Received order ids: {0}",
				string.Join( ",", orders.Select( x => x.Id ) ) );

			return orders;
		}
		#endregion

		#region GetFinishedOrders
		public IEnumerable< VolusionOrder > GetFinishedOrders( IEnumerable< int > ordersIds, bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			var orders = new List< VolusionOrder >();
			foreach( var orderId in ordersIds )
			{
				var filteredOrders = this.GetFilteredOrders( OrderColumns.OrderId, orderId, marker, isAddOrderComments );
				var order = filteredOrders.FirstOrDefault();
				if( order != null && this.FinishedStatuses.Contains( order.OrderStatusStr ) )
					orders.Add( order );
			}

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetFinishedOrdersAsync( IEnumerable< int > ordersIds, bool isAddOrderComments )
		{
			var marker = this.GetMarker();
			var orders = await ordersIds.ProcessInBatchAsync( 10, async orderId =>
			{
				var filteredOrders = await this.GetFilteredOrdersAsync( OrderColumns.OrderId, orderId, marker, isAddOrderComments );
				var order = filteredOrders.FirstOrDefault();
				if( order != null && this.FinishedStatuses.Contains( order.OrderStatusStr ) )
					return order;
				return null;
			} );

			return orders;
		}
		#endregion

		#region Misc
		private List< VolusionOrder > GetFilteredOrders( OrderColumns column, object value, string marker, bool isAddOrderComments )
		{
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value, isAddOrderComments );

			var result = ActionPolicies.Get.Get( () => this._webRequestServices.GetResponse< VolusionOrders >( endpoint, marker ) );
			if( result == null || result.Orders == null )
				return new List< VolusionOrder >();

			this.SetDefaultTimeZone( result.Orders );
			return result.Orders;
		}

		private async Task< List< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value, string marker, bool isAddOrderComments )
		{
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value, isAddOrderComments );

			var result = await ActionPolicies.GetAsync.Get( async () => await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint, marker ) );
			if( result == null || result.Orders == null )
				return new List< VolusionOrder >();

			this.SetDefaultTimeZone( result.Orders );
			return result.Orders;
		}

		private async Task< List< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value, string marker, HashSet< string > includeColumns, HashSet< string > includeColumnsDetails )
		{
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value, includeColumns, includeColumnsDetails );

			var result = await ActionPolicies.GetAsync.Get( async () => await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint, marker ) );
			if( result == null || result.Orders == null )
				return new List< VolusionOrder >();

			this.SetDefaultTimeZone( result.Orders );
			return result.Orders;
		}

		private IEnumerable< VolusionOrder > GetFilteredNewOrUpdatedOrders( Func< VolusionOrder, bool > predicate, string marker, bool isAddOrderComments )
		{
			var orders = new HashSet< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint( isAddOrderComments );

			while( true )
			{
				var ordersPortion = ActionPolicies.Get.Get( () => this._webRequestServices.GetResponse< VolusionOrders >( endpoint, marker ) );
				if( ordersPortion == null || ordersPortion.Orders == null || ordersPortion.Orders.Count == 0 )
					return orders;

				this.SetDefaultTimeZone( ordersPortion.Orders );
				this.AddOrders( orders, ordersPortion.Orders.Where( predicate ) );
			}
		}

		private async Task< IEnumerable< VolusionOrder > > GetFilteredNewOrUpdatedOrdersAsync( Func< VolusionOrder, bool > predicate, string marker, bool isAddOrderComments )
		{
			var orders = new HashSet< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint( isAddOrderComments );

			while( true )
			{
				var ordersPortion = await ActionPolicies.GetAsync.Get( async () => await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint, marker ) );
				if( ordersPortion == null || ordersPortion.Orders == null || ordersPortion.Orders.Count == 0 )
					return orders;

				this.SetDefaultTimeZone( ordersPortion.Orders );
				this.AddOrders( orders, ordersPortion.Orders.Where( predicate ) );
			}
		}

		private void AddOrders( HashSet< VolusionOrder > processedOrders, IEnumerable< VolusionOrder > fetchedOrdersPartition )
		{
			foreach( var order in fetchedOrdersPartition )
			{
				var oldOrder = processedOrders.FirstOrDefault( x => x.Id == order.Id && x.LastModified <= order.LastModified );
				if( oldOrder != null )
					processedOrders.Remove( oldOrder );

				processedOrders.Add( order );
			}
		}

		private bool DoesOrderCreatedOrUpdatedInDateRange( VolusionOrder order, DateTime startDateUtc, DateTime endDateUtc )
		{
			var isValid = ( order.OrderDateUtc >= startDateUtc && order.OrderDateUtc <= endDateUtc ) ||
			              ( order.LastModifiedUtc >= startDateUtc && order.LastModifiedUtc <= endDateUtc );
			if( !isValid )
			{
				//TODO: Remove it if all works fine
				VolusionLogger.Log.Trace( "Order '{0}' with OrderDateUtc '{1}' LastModifiedUtc '{2}' DefaultTimeZone '{3}' and TimeZone '{4}' isn't in date range startDateUtc '{5}' and endDateUtc '{6}'",
					order.Id, order.OrderDateUtc, order.LastModifiedUtc, order.DefaultTimeZone, order.TimeZone, startDateUtc, endDateUtc );
			}
			return isValid;
		}

		private void SetDefaultTimeZone( IEnumerable< VolusionOrder > orders )
		{
			foreach( var order in orders )
			{
				order.DefaultTimeZone = this._config.DefaultTimeZone;
			}
		}

		private string GetMarker()
		{
			return Guid.NewGuid().ToString();
		}
		#endregion misc
	}
}
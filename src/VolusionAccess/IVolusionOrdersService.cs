using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		VolusionOrder GetOrder( int orderId, bool isAddOrderComments );
		Task< VolusionOrder > GetOrderAsync( int orderId, bool isAddOrderComments );

		IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( bool isAddOrderComments );
		Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( bool isAddOrderComments );

		IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments );
		Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments );

		// [ Obsolete( "Because method filter server responses by date, instead of requesting orders in date range" )] TODO: make it obsolete, when we will completely rid of this
		IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments );

		// [ Obsolete( "Because method filter server responses by date, instead of requesting orders in date range" )] TODO: make it obsolete, when we will completely rid of this
		Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc, bool isAddOrderComments );

		Task< IEnumerable< VolusionOrder > > GetOpenOrdersAsync( HashSet< string > includeColumns, HashSet< string > includeColumnsDetails );

		IEnumerable< VolusionOrder > GetFinishedOrders( IEnumerable< int > ordersIds, bool isAddOrderComments );
		Task< IEnumerable< VolusionOrder > > GetFinishedOrdersAsync( IEnumerable< int > ordersIds, bool isAddOrderComments );
		Task< IEnumerable< VolusionOrder > > GetFinishedOrdersAsync( IEnumerable< int > ordersIds, HashSet< string > includeColumns, HashSet< string > includeColumnsDetails );
	}
}
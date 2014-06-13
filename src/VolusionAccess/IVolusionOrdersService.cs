using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		VolusionOrder GetOrder( int orderId );
		Task< VolusionOrder > GetOrderAsync( int orderId );

		IEnumerable< VolusionOrder > GetAllOrders();
		Task< IEnumerable< VolusionOrder > > GetAllOrdersAsync();

		IEnumerable< VolusionOrder > GetOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		IEnumerable< VolusionOrder > GetFilteredOrders( OrderColumns column, object value );
		Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value );
	}
}
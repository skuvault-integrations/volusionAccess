using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		VolusionOrder GetOrder( int orderId );
		Task< VolusionOrder > GetOrderAsync( int orderId );

		IEnumerable< VolusionOrder > GetNewOrUpdatedOrders();
		Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync();

		IEnumerable< VolusionOrder > GetNewOrUpdatedOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< VolusionOrder > > GetNewOrUpdatedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		IEnumerable< VolusionOrder > GetNotFinishedOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< VolusionOrder > > GetNotFinishedOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );
	}
}
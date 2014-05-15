using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		IEnumerable< VolusionOrder > GetAllOrders();
		Task< IEnumerable< VolusionOrder > > GetAllOrdersAsync();

		/// <summary>
		/// This method uses yield for getting orders therefore this method more preferred for big data than <see cref="GetOrdersAsync"/>
		/// </summary>
		IEnumerable< VolusionOrder > GetOrders( DateTime startDate, DateTime endDate );
		Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime startDate, DateTime endDate );

		IEnumerable< VolusionOrder > GetFilteredOrders( OrderColumns column, object value );
		Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value );
	}
}
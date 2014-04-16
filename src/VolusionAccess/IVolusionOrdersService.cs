using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		IEnumerable< VolusionOrder > GetOrders( DateTime dateFrom, DateTime dateTo );
		Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo );
	}
}
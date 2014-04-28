using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Order;

namespace VolusionAccess
{
	public interface IVolusionOrdersService
	{
		IEnumerable< VolusionOrder > GetOrders();
		Task< IEnumerable< VolusionOrder > > GetOrdersAsync();

		IEnumerable< VolusionOrder > GetFilteredOrders( string columnName, string value );
		Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( string columnName, string value );
	}
}
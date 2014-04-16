using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Order;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionOrdersService : VolusionServiceBase, IVolusionOrdersService
	{
		private readonly WebRequestServices _webRequestServices;

		public VolusionOrdersService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
		}

		public IEnumerable< VolusionOrder > GetOrders( DateTime dateFrom, DateTime dateTo )
		{
			throw new NotImplementedException();
		}

		public Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo )
		{
			throw new NotImplementedException();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using VolusionAccess.Misc;
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
			var orders = new List< VolusionOrder >();
			IList< VolusionOrder > ordersPortion = null;
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint( dateFrom, dateTo );

			do
			{
				ActionPolicies.Get.Do( () =>
				{
					var tmp = this._webRequestServices.GetResponse< VolusionOrders >( endpoint );
					ordersPortion = tmp != null ? tmp.Orders : null;
					if( ordersPortion != null )
						orders.AddRange( ordersPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( ordersPortion != null && ordersPortion.Count != 0 );

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo )
		{
			var orders = new List< VolusionOrder >();
			IList< VolusionOrder > ordersPortion = null;
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint( dateFrom, dateTo );

			do
			{
				await ActionPolicies.GetAsync.Do( async () =>
				{
					var tmp = await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint );
					ordersPortion = tmp != null ? tmp.Orders : null;
					if( ordersPortion != null )
						orders.AddRange( ordersPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( ordersPortion != null && ordersPortion.Count != 0 );

			return orders;
		}
	}
}
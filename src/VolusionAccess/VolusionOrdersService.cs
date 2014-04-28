using System;
using System.Collections.Generic;
using System.Linq;
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

		public IEnumerable< VolusionOrder > GetOrders()
		{
			var orders = new List< VolusionOrder >();
			IList< VolusionOrder > ordersPortion = null;
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

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

		public async Task< IEnumerable< VolusionOrder > > GetOrdersAsync()
		{
			var orders = new List< VolusionOrder >();
			IList< VolusionOrder > ordersPortion = null;
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

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

		public IEnumerable< VolusionOrder > GetOrders( DateTime startDate, DateTime endDate )
		{
			var orders = GetOrders();
			orders = orders.Where( x => x.OrderDateUtc >= startDate && x.OrderDateUtc <= endDate ).ToList();
			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime startDate, DateTime endDate )
		{
			var orders = await GetOrdersAsync();
			orders = orders.Where( x => x.OrderDateUtc >= startDate && x.OrderDateUtc <= endDate ).ToList();
			return orders;
		}

		public IEnumerable< VolusionOrder > GetFilteredOrders( string columnName, string value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( columnName, value );

			ActionPolicies.Get.Do( () =>
			{
				var tmp = this._webRequestServices.GetResponse< VolusionOrders >( endpoint );
				if( tmp != null && tmp.Orders != null )
					orders.AddRange( tmp.Orders );

				//API requirement
				this.CreateApiDelay().Wait();
			} );

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( string columnName, string value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( columnName, value );

			await ActionPolicies.GetAsync.Do( async () =>
			{
				var tmp = await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint );
				if( tmp != null && tmp.Orders != null )
					orders.AddRange( tmp.Orders );

				//API requirement
				this.CreateApiDelay().Wait();
			} );

			return orders;
		}
	}
}
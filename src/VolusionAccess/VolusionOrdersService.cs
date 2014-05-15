using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using VolusionAccess.Misc;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Order;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionOrdersService : IVolusionOrdersService
	{
		private readonly WebRequestServices _webRequestServices;

		public VolusionOrdersService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
		}

		public IEnumerable< VolusionOrder > GetAllOrders()
		{
			return GetOrders().ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetAllOrdersAsync()
		{
			var orders = await GetOrdersAsync();
			return orders.ToList();
		}

		/// <summary>
		/// This method uses yield for getting orders therefore this method more preferred for big data than <see cref="GetOrdersAsync"/>
		/// </summary>
		public IEnumerable< VolusionOrder > GetOrders( DateTime startDate, DateTime endDate )
		{
			var orders = GetOrders();
			orders = orders.Where( x => x.OrderDateUtc >= startDate && x.OrderDateUtc <= endDate ||
			                            x.LastModifiedUtc >= startDate && x.LastModifiedUtc <= endDate );
			return orders.ToList();
		}

		public async Task< IEnumerable< VolusionOrder > > GetOrdersAsync( DateTime startDate, DateTime endDate )
		{
			var orders = await GetOrdersAsync();
			orders = orders.Where( x => x.OrderDateUtc >= startDate && x.OrderDateUtc <= endDate ||
			                            x.LastModifiedUtc >= startDate && x.LastModifiedUtc <= endDate );
			return orders.ToList();
		}

		public IEnumerable< VolusionOrder > GetFilteredOrders( OrderColumns column, object value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value );

			ActionPolicies.Get.Do( () =>
			{
				var tmp = this._webRequestServices.GetResponse< VolusionOrders >( endpoint );
				if( tmp != null && tmp.Orders != null )
					orders.AddRange( tmp.Orders );
			} );

			return orders;
		}

		public async Task< IEnumerable< VolusionOrder > > GetFilteredOrdersAsync( OrderColumns column, object value )
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetFilteredOrdersEndpoint( column, value );

			await ActionPolicies.GetAsync.Do( async () =>
			{
				var tmp = await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint );
				if( tmp != null && tmp.Orders != null )
					orders.AddRange( tmp.Orders );
			} );

			return orders;
		}

		private IEnumerable< VolusionOrder > GetOrders()
		{
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

			while( true )
			{
				var ordersPortion = ActionPolicies.Get.Get( () =>
				{
					var tmp = this._webRequestServices.GetResponse< VolusionOrders >( endpoint );
					return tmp == null || tmp.Orders == null ? new List< VolusionOrder >() : tmp.Orders;
				} );

				if( ordersPortion.Count == 0 )
					yield break;

				foreach( var volusionOrder in ordersPortion )
				{
					yield return volusionOrder;
				}
			}
		}

		private async Task< IEnumerable< VolusionOrder > > GetOrdersAsync()
		{
			var orders = new List< VolusionOrder >();
			var endpoint = EndpointsBuilder.CreateGetOrdersEndpoint();

			while( true )
			{
				var ordersPortion = await ActionPolicies.Get.Get( async () =>
				{
					var tmp = await this._webRequestServices.GetResponseAsync< VolusionOrders >( endpoint );
					return tmp == null || tmp.Orders == null ? new List< VolusionOrder >() : tmp.Orders;
				} );

				if( ordersPortion.Count == 0 )
					return orders;

				orders.AddRange( ordersPortion );
			}
		}
	}
}
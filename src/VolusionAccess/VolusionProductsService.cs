using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using VolusionAccess.Misc;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Product;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionProductsService : VolusionServiceBase, IVolusionProductsService
	{
		private readonly WebRequestServices _webRequestServices;

		public VolusionProductsService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._webRequestServices = new WebRequestServices( config );
		}

		#region Get
		public IEnumerable< VolusionProduct > GetProducts()
		{
			var products = new List< VolusionProduct >();
			var productsPortion = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();

			do
			{
				ActionPolicies.Get.Do( () =>
				{
					productsPortion = this._webRequestServices.GetResponse< IList< VolusionProduct > >( endpoint ).ToList();
					products.AddRange( productsPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( productsPortion.Count != 0 );

			return products;
		}

		public async Task< IEnumerable< VolusionProduct > > GetProductsAsync()
		{
			var products = new List< VolusionProduct >();
			var productsPortion = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();

			do
			{
				await ActionPolicies.GetAsync.Do( async () =>
				{
					productsPortion = ( await this._webRequestServices.GetResponseAsync< IList< VolusionProduct > >( endpoint ) ).ToList();
					products.AddRange( productsPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( productsPortion.Count != 0 );

			return products;
		}
		#endregion

		#region Update
		public void UpdateProducts( IEnumerable< VolusionProduct > products )
		{
			throw new NotImplementedException();
		}

		public Task UpdateProductsAsync( IEnumerable< VolusionProduct > products )
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
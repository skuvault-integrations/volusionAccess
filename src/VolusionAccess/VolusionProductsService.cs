using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using ServiceStack;
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
			IList< VolusionProduct > productsPortion = null;
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();

			do
			{
				ActionPolicies.Get.Do( () =>
				{
					productsPortion = this._webRequestServices.GetResponse< VolusionProducts >( endpoint );
					if( productsPortion != null )
						products.AddRange( productsPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( productsPortion != null && productsPortion.Count != 0 );

			return products;
		}

		public async Task< IEnumerable< VolusionProduct > > GetProductsAsync()
		{
			var products = new List< VolusionProduct >();
			IList< VolusionProduct > productsPortion = null;
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();

			do
			{
				await ActionPolicies.GetAsync.Do( async () =>
				{
					productsPortion = await this._webRequestServices.GetResponseAsync< VolusionProducts >( endpoint );
					if( productsPortion != null )
						products.AddRange( productsPortion );

					//API requirement
					this.CreateApiDelay().Wait();
				} );
			} while( productsPortion != null && productsPortion.Count != 0 );

			return products;
		}
		#endregion

		#region Update
		/// <summary>
		/// Update products
		/// </summary>
		/// <param name="products">The products. Need to use SKU as key.</param>
		public void UpdateProducts( IEnumerable< VolusionProduct > products )
		{
			var endpoint = EndpointsBuilder.CreateProductsUpdateEndpoint();
			var vp = new VolusionProducts( products );
			var xmlContent = vp.ToXml();

			ActionPolicies.Submit.Do( () =>
			{
				this._webRequestServices.PostData( endpoint, xmlContent );

				//API requirement
				this.CreateApiDelay().Wait();
			} );
		}

		/// <summary>
		/// Update products
		/// </summary>
		/// <param name="products">The products. Need to use SKU as key.</param>
		public async Task UpdateProductsAsync( IEnumerable< VolusionProduct > products )
		{
			var endpoint = EndpointsBuilder.CreateProductsUpdateEndpoint();
			var vp = new VolusionProducts( products );
			var xmlContent = vp.ToXml();

			await ActionPolicies.SubmitAsync.Do( async () =>
			{
				await this._webRequestServices.PostDataAsync( endpoint, xmlContent );

				//API requirement
				this.CreateApiDelay().Wait();
			} );
		}
		#endregion
	}
}
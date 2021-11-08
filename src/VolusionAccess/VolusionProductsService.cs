﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using Netco.Extensions;
using VolusionAccess.Misc;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;
using VolusionAccess.Models.Product;
using VolusionAccess.Services;

namespace VolusionAccess
{
	public class VolusionProductsService: IVolusionProductsService
	{
		private const int UpdateInventoryLimit = 100;
		private readonly TimeSpan UpdateInventoryDelay = TimeSpan.FromSeconds( 2 );
		private readonly WebRequestServices _webRequestServices;
		private readonly VolusionConfig _config;
		
		private readonly VolusionThrottler _throttler = new VolusionThrottler();
		private readonly VolusionThrottlerAsync _throttlerAsync = new VolusionThrottlerAsync();

		public VolusionProductsService( VolusionConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();
			this._webRequestServices = new WebRequestServices( config );
			this._config = config;
		}

		#region Get
		/// <summary>
		/// The All Products Export Feature of the API are basic functions that are auto-generated by the Volusion system.
		/// Once generated, the XML code is updated automatically on a regularly, timed basis. The auto-update takes place at 12:00 AM US Central Standard Time.
		/// </summary>
		/// <returns></returns>
		public IEnumerable< VolusionPublicProduct > GetPublicProducts()
		{
			var products = new List< VolusionPublicProduct >();
			var endpoint = EndpointsBuilder.CreateGetPublicProductsEndpoint().GetFullEndpoint( this._config );
			var marker = GetMarker();

			var productsPortion = ActionPolicies.Get.Get( () => this._throttler.Execute( () => this._webRequestServices.GetResponseForSpecificUrl< VolusionPublicProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products.AddRange( productsPortion.Products );

			return products;
		}

		/// <summary>
		/// The All Products Export Feature of the API are basic functions that are auto-generated by the Volusion system.
		/// Once generated, the XML code is updated automatically on a regularly, timed basis. The auto-update takes place at 12:00 AM US Central Standard Time.
		/// </summary>
		/// <returns></returns>
		public async Task< IEnumerable< VolusionPublicProduct > > GetPublicProductsAsync()
		{
			var products = new List< VolusionPublicProduct >();
			var endpoint = EndpointsBuilder.CreateGetPublicProductsEndpoint().GetFullEndpoint( this._config );
			var marker = GetMarker();

			var productsPortion = await ActionPolicies.GetAsync.Get( () => this._throttlerAsync.ExecuteAsync( async () => await this._webRequestServices.GetResponseForSpecificUrlAsync< VolusionPublicProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products.AddRange( productsPortion.Products );

			return products;
		}

		public IEnumerable< VolusionProduct > GetProducts()
		{
			var products = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();
			var marker = GetMarker();

			while( true )
			{
				var productsPortion = ActionPolicies.Get.Get( () => this._throttler.Execute( () => this._webRequestServices.GetResponse< VolusionProducts >( endpoint, marker ) ) );
				if( productsPortion == null || productsPortion.Products == null || productsPortion.Products.Count == 0 )
					return products;

				products.AddRange( productsPortion.Products );
			}
		}

		public async Task< IEnumerable< VolusionProduct > > GetProductsAsync()
		{
			var products = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetProductsEndpoint();
			var marker = GetMarker();

			while( true )
			{
				var productsPortion = await ActionPolicies.GetAsync.Get( () => this._throttlerAsync.ExecuteAsync( async () => await this._webRequestServices.GetResponseAsync< VolusionProducts >( endpoint, marker ) ) );
				if( productsPortion == null || productsPortion.Products == null || productsPortion.Products.Count == 0 )
					return products;

				products.AddRange( productsPortion.Products );
			}
		}

		public IEnumerable< VolusionProduct > GetFilteredProducts( ProductColumns column, object value )
		{
			var products = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetFilteredProductsEndpoint( column, value );
			var marker = GetMarker();

			var productsPortion = ActionPolicies.Get.Get( () => this._throttler.Execute( () => this._webRequestServices.GetResponse< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products.AddRange( productsPortion.Products );

			return products;
		}

		public async Task< IEnumerable< VolusionProduct > > GetFilteredProductsAsync( ProductColumns column, object value )
		{
			var products = new List< VolusionProduct >();
			var endpoint = EndpointsBuilder.CreateGetFilteredProductsEndpoint( column, value );
			var marker = GetMarker();

			var productsPortion = await ActionPolicies.GetAsync.Get( () => this._throttlerAsync.ExecuteAsync( async () => await this._webRequestServices.GetResponseAsync< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products.AddRange( productsPortion.Products );

			return products;
		}

		public IEnumerable< VolusionProduct > GetFakeFilteredProducts()
		{
			var products = this.GetFilteredProducts( ProductColumns.AddToPONow, "N" );
			return products;
		}

		public async Task< IEnumerable< VolusionProduct > > GetFakeFilteredProductsAsync()
		{
			var products = await this.GetFilteredProductsAsync( ProductColumns.AddToPONow, "N" );
			return products;
		}

		public VolusionProduct GetProduct( string sku )
		{
			VolusionProduct product = null;
			var endpoint = EndpointsBuilder.CreateGetProductEndpoint( sku );
			var marker = GetMarker();

			var productsPortion = ActionPolicies.Get.Get( () => this._throttler.Execute( () => this._webRequestServices.GetResponse< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				product = productsPortion.Products.FirstOrDefault();

			return product;
		}

		public async Task< VolusionProduct > GetProductAsync( string sku )
		{
			VolusionProduct product = null;
			var endpoint = EndpointsBuilder.CreateGetProductEndpoint( sku );
			var marker = GetMarker();

			var productsPortion = await ActionPolicies.GetAsync.Get( () => this._throttlerAsync.ExecuteAsync( async () => await this._webRequestServices.GetResponseAsync< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				product = productsPortion.Products.FirstOrDefault();

			return product;
		}

		public IEnumerable< VolusionProduct > GetChildProducts( string sku )
		{
			List< VolusionProduct > products = null;
			var endpoint = EndpointsBuilder.CreateGetChildProductsEndpoint( sku );
			var marker = GetMarker();

			var productsPortion = ActionPolicies.Get.Get( () => this._throttler.Execute( () => this._webRequestServices.GetResponse< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products = productsPortion.Products;

			return products;
		}

		public async Task< IEnumerable< VolusionProduct > > GetChildProductAsync( string sku )
		{
			List< VolusionProduct > products = null;
			var endpoint = EndpointsBuilder.CreateGetChildProductsEndpoint( sku );
			var marker = GetMarker();

			var productsPortion = await ActionPolicies.GetAsync.Get( () => this._throttlerAsync.ExecuteAsync( async () => await this._webRequestServices.GetResponseAsync< VolusionProducts >( endpoint, marker ) ) );
			if( productsPortion != null && productsPortion.Products != null )
				products = productsPortion.Products;

			return products;
		}
		#endregion

		#region Update
		public void UpdateProducts( IEnumerable< VolusionUpdatedProduct > products )
		{
			var endpoint = EndpointsBuilder.CreateProductsUpdateEndpoint();

			var parts = products.Slice( UpdateInventoryLimit );

			var batchMark = GetMarker();
			VolusionLogger.Log.Trace( string.Format( "Marker:{0}\t Start update products", batchMark ) );
			var i = 0;

			foreach( var part in parts )
			{
				var vp = new VolusionUpdatedProducts { Products = part.ToList() };
				var xmlContent = XmlSerializeHelpers.Serialize( vp );
				var iterMark = GetMarker( batchMark );

				VolusionLogger.Log.Trace( string.Format( "Marker:{0}\t Trace update products. Step {1}. Updating {2} products. ", batchMark, i++, vp.Products.Count ) );

				ActionPolicies.Submit.Do( () => this._webRequestServices.PostData( endpoint, xmlContent, iterMark ) );
				Task.Delay( this.UpdateInventoryDelay ).Wait();
			}
		}

		public async Task UpdateProductsAsync( IEnumerable< VolusionUpdatedProduct > products )
		{
			var endpoint = EndpointsBuilder.CreateProductsUpdateEndpoint();

			var parts = products.Slice( UpdateInventoryLimit );

			var batchMarker = GetMarker();
			VolusionLogger.Log.Trace( string.Format( "Marker:{0}\t Start update products", batchMarker ) );
			var i = 0;

			foreach( var part in parts )
			{
				var vp = new VolusionUpdatedProducts { Products = part.ToList() };
				var xmlContent = XmlSerializeHelpers.Serialize( vp );
				var iterMarker = GetMarker( batchMarker );

				VolusionLogger.Log.Trace( string.Format( "Marker:{0}\t Trace update products. Step {1}. Updating {2} products. ", batchMarker, i++, vp.Products.Count ) );

				await ActionPolicies.SubmitAsync.Do( async () => await this._webRequestServices.PostDataAsync( endpoint, xmlContent, iterMarker ) );
				await Task.Delay( this.UpdateInventoryDelay );
			}
			VolusionLogger.Log.Trace( string.Format( "Marker:{0}\t End update products", batchMarker ) );
		}
		#endregion

		private static string GetMarker()
		{
			return Guid.NewGuid().ToString();
		}

		private static string GetMarker( string prefixMarker )
		{
			prefixMarker = prefixMarker ?? string.Empty;
			return prefixMarker + "--" + Guid.NewGuid();
		}
	}
}
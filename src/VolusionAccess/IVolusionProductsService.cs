﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Product;

namespace VolusionAccess
{
	public interface IVolusionProductsService
	{
		/// <summary>
		/// The All Products Export Feature of the API are basic functions that are auto-generated by the Volusion system. 
		/// Once generated, the XML code is updated automatically on a regularly, timed basis. The auto-update takes place at 12:00 AM US Central Standard Time. 
		/// </summary>
		/// <returns></returns>
		IEnumerable< VolusionPublicProduct > GetPublicProducts();

		/// <summary>
		/// The All Products Export Feature of the API are basic functions that are auto-generated by the Volusion system. 
		/// Once generated, the XML code is updated automatically on a regularly, timed basis. The auto-update takes place at 12:00 AM US Central Standard Time. 
		/// </summary>
		/// <returns></returns>
		Task< IEnumerable< VolusionPublicProduct > > GetPublicProductsAsync();

		IEnumerable< VolusionProduct > GetProducts();
		Task< IEnumerable< VolusionProduct > > GetProductsAsync();

		VolusionProduct GetProduct( string sku );
		Task< VolusionProduct > GetProductAsync( string sku );

		void UpdateProducts( IEnumerable< VolusionUpdatedProduct > products );
		Task UpdateProductsAsync( IEnumerable< VolusionUpdatedProduct > products );
	}
}
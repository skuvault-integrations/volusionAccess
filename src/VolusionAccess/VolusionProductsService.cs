using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
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
			throw new NotImplementedException();
		}

		public Task< IEnumerable< VolusionProduct > > GetProductsAsync()
		{
			throw new NotImplementedException();
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
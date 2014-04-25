using System.Collections.Generic;
using System.Threading.Tasks;
using VolusionAccess.Models.Product;

namespace VolusionAccess
{
	public interface IVolusionProductsService
	{
		IEnumerable< VolusionProduct > GetPublicProducts();
		Task< IEnumerable< VolusionProduct > > GetPublicProductsAsync();

		IEnumerable< VolusionProduct > GetProducts();
		Task< IEnumerable< VolusionProduct > > GetProductsAsync();

		void UpdateProducts( IEnumerable< VolusionProduct > products );
		Task UpdateProductsAsync( IEnumerable< VolusionProduct > products );
	}
}
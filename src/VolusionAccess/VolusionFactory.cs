using VolusionAccess.Models.Configuration;

namespace VolusionAccess
{
	public interface IVolusionFactory
	{
		IVolusionProductsService CreateProductsService( VolusionConfig config );
	}

	public sealed class VolusionFactory : IVolusionFactory
	{
		public IVolusionProductsService CreateProductsService( VolusionConfig config )
		{
			return new VolusionProductsService( config );
		}
	}
}

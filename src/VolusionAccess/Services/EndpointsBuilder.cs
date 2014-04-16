using System.Text;
using VolusionAccess.Models.Command;

namespace VolusionAccess.Services
{
	internal static class EndpointsBuilder
	{
		public static readonly string EmptyParams = string.Empty;

		public static string CreateGetProductsEndpoint()
		{
			var endpoint = string.Format( "&{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns, "*" );
			return endpoint;
		}

		public static string CreateGetOrdersEndpoint()
		{
			var endpoint = string.Format( "&{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns, "*" );
			return endpoint;
		}

		public static string ConcatParams( this string mainEndpoint, params string[] endpoints )
		{
			var result = new StringBuilder( mainEndpoint );

			foreach( var endpoint in endpoints )
			{
				result.Append( endpoint.Replace( "?", "&" ) );
			}

			return result.ToString();
		}
	}
}
using System.Text;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;

namespace VolusionAccess.Services
{
	internal static class EndpointsBuilder
	{
		public static readonly string EmptyParams = string.Empty;

		public static string CreateGetProductsEndpoint()
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, "*" );
			return endpoint;
		}

		public static string CreateProductsUpdateEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.Import.Name, VolusionParam.Update.Name );
			return endpoint;
		}

		public static string CreateGetOrdersEndpoint()
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, "*" );
			return endpoint;
		}

		public static string GetFullEndpoint( this string endpoint, VolusionConfig config )
		{
			var fullEndpoint = string.Format( "{0}?{1}={2}&{3}={4}&{5}",
				config.Host,
				VolusionParam.Login.Name, config.UserName,
				VolusionParam.EncryptedPassword.Name, config.Password,
				endpoint );
			return fullEndpoint;
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
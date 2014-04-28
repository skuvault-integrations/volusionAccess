using System;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;

namespace VolusionAccess.Services
{
	internal static class EndpointsBuilder
	{
		public static readonly string EmptyParams = string.Empty;

		public static string CreateGetPublicProductsEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.ApiName.Name, VolusionCommand.GetPublicProducts.Command );
			return endpoint;
		}

		public static string CreateGetProductsEndpoint()
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns() );
			return endpoint;
		}

		public static string CreateGetFilteredProductsEndpoint( string columnName, string value )
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns(),
				VolusionParam.WhereColumn.Name, columnName,
				VolusionParam.WhereValue.Name, value );
			return endpoint;
		}

		public static string CreateGetProductEndpoint( string sku )
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns(),
				VolusionParam.WhereColumn.Name, ProductColumns.Sku.Name,
				VolusionParam.WhereValue.Name, sku );
			return endpoint;
		}

		public static string CreateProductsUpdateEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.Import.Name, VolusionParam.Update.Name );
			return endpoint;
		}

		public static string CreateGetOrdersEndpoint( DateTime startDate, DateTime endDate )
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}", //&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, "*" //,
				//VolusionParam.WhereColumn.Name, "o.OrderDate",
				//VolusionParam.WhereValue.Name, DateTime.SpecifyKind( startDate, DateTimeKind.Utc ).ToString( "o" ) 
				);
			return endpoint;
		}

		public static string GetFullEndpoint( this string endpoint, VolusionConfig config )
		{
			var fullEndpoint = string.Format( "{0}?{1}", config.Host, endpoint );
			return fullEndpoint;
		}

		public static string GetFullEndpointWithAuth( this string endpoint, VolusionConfig config )
		{
			var fullEndpoint = string.Format( "{0}?{1}={2}&{3}={4}&{5}",
				config.Host,
				VolusionParam.Login.Name, config.UserName,
				VolusionParam.EncryptedPassword.Name, config.Password,
				endpoint );
			return fullEndpoint;
		}

		private static string GetProductColumns()
		{
			var columns = string.Format( "{0},{1},{2},{3},{4},{5},{6},{7}",
				ProductColumns.ProductID.Name,
				ProductColumns.Sku.Name,
				ProductColumns.Quantity.Name,
				ProductColumns.ProductName.Name,
				ProductColumns.LastModified.Name,
				ProductColumns.ProductPrice.Name,
				ProductColumns.SalePrice.Name,
				ProductColumns.IsChildOfSku.Name );
			return columns;
		}
	}
}
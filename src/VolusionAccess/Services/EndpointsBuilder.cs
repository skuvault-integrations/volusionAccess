using System;
using System.Globalization;
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

		public static string CreateGetFilteredProductsEndpoint( ProductColumns column, object value )
		{
			var endpoint = string.Format( _culture, "{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns(),
				VolusionParam.WhereColumn.Name, column.Name,
				VolusionParam.WhereValue.Name, value );
			return endpoint;
		}

		public static string CreateGetProductEndpoint( string sku )
		{
			return CreateGetFilteredProductsEndpoint( ProductColumns.Sku, sku );
		}

		public static string CreateGetChildProductsEndpoint( string sku )
		{
			return CreateGetFilteredProductsEndpoint( ProductColumns.IsChildOfSku, sku );
		}

		public static string CreateProductsUpdateEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.Import.Name, VolusionParam.Update.Name );
			return endpoint;
		}

		public static string CreateGetOrdersEndpoint( bool isAddOrderComments )
		{
			var endpoint = string.Format( "{0}={1}&{2}={3},{4}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, GetOrderColumns( isAddOrderComments ), GetOrderDetailsColumns() );
			return endpoint;
		}

		public static string CreateGetFilteredOrdersEndpoint( OrderColumns column, object value, bool isAddOrderComments )
		{
			var endpoint = string.Format( _culture, "{0}={1}&{2}={3},{4}&{5}={6}&{7}={8}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, GetOrderColumns( isAddOrderComments ), GetOrderDetailsColumns(),
				VolusionParam.WhereColumn.Name, column.Name,
				VolusionParam.WhereValue.Name, value );
			return endpoint;
		}

		public static string CreateGetFilteredOrdersEndpoint( OrderColumns column, object value, string[] includeColumns )
		{
			var endpoint = string.Format( _culture, "{0}={1}&{2}={3},{4}&{5}={6}&{7}={8}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, GetOrderColumns( includeColumns ), GetOrderDetailsColumns(),
				VolusionParam.WhereColumn.Name, column.Name,
				VolusionParam.WhereValue.Name, value );
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
			var columns = string.Format( "{0},{1},{2},{3},{4},{5},{6},{7},{8}",
				ProductColumns.ProductID.Name,
				ProductColumns.Sku.Name,
				ProductColumns.Quantity.Name,
				ProductColumns.ProductName.Name,
				ProductColumns.LastModified.Name,
				ProductColumns.ProductPrice.Name,
				ProductColumns.SalePrice.Name,
				ProductColumns.IsChildOfSku.Name,
				ProductColumns.Warehouses.Name );
			return columns;
		}

		private static string GetOrderColumns( params string[] columns )
		{
			var joinedColumns = string.Join( ",", columns );
			return joinedColumns;
		}

		private static string GetOrderColumns( bool isAddOrderComments )
		{
			var columns = "o.OrderID," +
			              "o.AccountNumber," +
			              "o.AccountType," +
			              "o.AddressValidated," +
			              "o.Affiliate_Commissionable_Value," +
			              "o.BankName," +
			              "o.CustomerID," +
			              "o.IsAGift," +
			              "o.IsGTSOrder," +
			              "o.Locked," +
			              "o.Order_Entry_System," +
			              "o.CancelDate," +
			              "o.CancelReason," +
			              "o.InitiallyShippedDate," +
			              "o.LastModified," +
			              "o.OrderDate," +
			              "o.OrderDateUtc," +
			              "o.OrderStatus," +
						  ( isAddOrderComments ? "o.Order_Comments," : string.Empty ) +
						  "o.PaymentAmount," +
			              "o.PaymentDeclined," +
			              "o.PaymentMethodID," +
			              "o.Stock_Priority," +
			              "o.Total_Payment_Authorized," +
			              "o.Total_Payment_Received," +
			              "o.TotalShippingCost," +
			              "o.SalesTax1," +
			              "o.SalesTax2," +
			              "o.SalesTax3," +
			              "o.SalesTaxRate," +
			              "o.SalesTaxRate1," +
			              "o.SalesTaxRate2," +
			              "o.SalesTaxRate3," +
			              "o.BillingAddress1," +
			              "o.BillingAddress2," +
			              "o.BillingCity," +
			              "o.BillingCompanyName," +
			              "o.BillingCountry," +
			              "o.BillingFaxNumber," +
			              "o.BillingFirstName," +
			              "o.BillingLastName," +
			              "o.BillingPhoneNumber," +
			              "o.BillingPostalCode," +
			              "o.BillingState," +
			              "o.ShipAddress1," +
			              "o.ShipAddress2," +
			              "o.ShipCity," +
			              "o.ShipCompanyName," +
			              "o.ShipCountry," +
			              "o.ShipDate," +
			              "o.ShipFaxNumber," +
			              "o.ShipFirstName," +
			              "o.ShipLastName," +
			              "o.Shipped," +
			              "o.ShipPhoneNumber," +
			              "o.Shipping_Locked," +
			              "o.ShippingMethodID," +
			              "o.ShipPostalCode," +
			              "o.ShipResidential," +
			              "o.ShipState," +
			              "o.OrderDetails";
			return columns;
		}

		private static string GetOrderDetailsColumns()
		{
			var columns = "od.OrderDetailID," +
			              "od.AutoDropShip," +
			              "od.CategoryID," +
			              "od.CouponCode," +
			              "od.CustomLineItem," +
			              "od.Fixed_ShippingCost," +
			              "od.Fixed_ShippingCost_Outside_LocalRegion," +
			              "od.FreeShippingItem," +
			              "od.GiftTrakNumber," +
			              "od.GiftWrapCost," +
						  "od.IsKitID," +
			              "od.KitID," +
			              "od.Locked," +
			              "od.LastModified," +
			              "od.OnOrder_Qty," +
			              "od.OptionID," +
			              "od.OptionIDs," +
			              "od.Package_Type," +
			              "od.Product_Keys_Shipped," +
			              "od.ProductCode," +
			              "od.ProductID," +
			              "od.ProductName," +
						  "od.ProductPrice," +
			              "od.QtyOnBackOrder," +
			              "od.QtyOnHold," +
			              "od.QtyOnPackingSlip," +
			              "od.QtyShipped," +
			              "od.Quantity," +
			              "od.Returned," +
			              "od.Returned_Date," +
			              "od.Reward_Points_Given_For_Purchase," +
			              "od.ShipDate," +
			              "od.Shipped," +
			              "od.Ships_By_Itself," +
			              "od.TaxableProduct," +
			              "od.TotalPrice," +
			              "od.Vendor_Price," +
			              "od.Warehouses";
			return columns;
		}

		private static readonly CultureInfo _culture = new CultureInfo( "en-US" );
	}
}
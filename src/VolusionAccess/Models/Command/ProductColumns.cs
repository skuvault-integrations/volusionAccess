namespace VolusionAccess.Models.Command
{
	public class ProductColumns
	{
		public static readonly ProductColumns Unknown = new ProductColumns( string.Empty );
		public static readonly ProductColumns ProductID = new ProductColumns( "p.ProductID" );
		public static readonly ProductColumns Sku = new ProductColumns( "p.ProductCode" );
		public static readonly ProductColumns ProductName = new ProductColumns( "p.ProductName" );
		public static readonly ProductColumns LastModified = new ProductColumns( "p.LastModified" );
		public static readonly ProductColumns Quantity = new ProductColumns( "p.StockStatus" );
		public static readonly ProductColumns IsChildOfSku = new ProductColumns( "p.IsChildOfProductCode" );
		
		public static readonly ProductColumns ProductPrice = new ProductColumns( "pe.ProductPrice" );
		public static readonly ProductColumns SalePrice = new ProductColumns( "pe.SalePrice" );
		public static readonly ProductColumns Warehouses = new ProductColumns( "pe.warehouses" );
		public static readonly ProductColumns HideYouSave = new ProductColumns( "pe.Hide_YouSave" );
		public static readonly ProductColumns HideFreeAccessories = new ProductColumns( "pe.Hide_FreeAccessories" );
		public static readonly ProductColumns AddToPONow = new ProductColumns( "pe.AddToPO_Now" );

		private ProductColumns( string name )
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}
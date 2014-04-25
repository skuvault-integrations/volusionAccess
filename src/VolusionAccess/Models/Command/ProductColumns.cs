namespace VolusionAccess.Models.Command
{
	internal class ProductColumns
	{
		public static readonly ProductColumns Unknown = new ProductColumns( string.Empty );
		public static readonly ProductColumns ProductID = new ProductColumns( "p.ProductID" );
		public static readonly ProductColumns Sku = new ProductColumns( "p.ProductCode" );
		public static readonly ProductColumns Quantity = new ProductColumns( "p.StockStatus" );
		public static readonly ProductColumns ProductPrice = new ProductColumns( "pe.ProductPrice" );
		public static readonly ProductColumns SalePrice = new ProductColumns( "pe.SalePrice" );
		public static readonly ProductColumns IsChildOfSku = new ProductColumns( "p.IsChildOfProductCode" );

		private ProductColumns( string name )
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}
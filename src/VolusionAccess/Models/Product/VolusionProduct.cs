using System.Runtime.Serialization;

namespace VolusionAccess.Models.Product
{
	[ DataContract( Name = "Products", Namespace = "" ) ]
	public class VolusionProduct
	{
		[ DataMember( Name = "ProductID" ) ]
		public int Id { get; set; }

		[ DataMember( Name = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ DataMember( Name = "ProductCode" ) ]
		public string Sku { get; set; }
	}
}
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Product
{
	[ DataContract( Name = "Products" ) ]
	public class VolusionProduct
	{
		[ DataMember( Name = "p.ProductID" ) ]
		public long Id { get; set; }

		[ DataMember( Name = "p.StockStatus" ) ]
		public string Quantity { get; set; }

		[ DataMember( Name = "p.ProductCode" ) ]
		public string Sku { get; set; }
	}
}
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionPublicProduct
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }
		
		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "ProductPrice" ) ]
		public decimal ProductPrice { get; set; }

		[ XmlElement( ElementName = "SalePrice" ) ]
		public decimal SalePrice { get; set; }
	}
}
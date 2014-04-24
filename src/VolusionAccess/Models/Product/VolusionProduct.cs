using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionProduct
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }
	}
}
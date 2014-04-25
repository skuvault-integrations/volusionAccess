using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionUpdatedProduct
	{
		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }

		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }
	}
}
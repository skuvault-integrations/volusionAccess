using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionUpdatedProduct
	{
		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }

		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "DoNotAllowBackOrders" ) ]
		public string DoNotAllowBackOrders { get; set; }

		public VolusionUpdatedProduct() {
			this.DoNotAllowBackOrders = "Y";
		}
	}
}
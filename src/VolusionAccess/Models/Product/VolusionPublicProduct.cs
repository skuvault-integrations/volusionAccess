using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionPublicProduct
	{
		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string Name { get; set; }

		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "ProductPrice" ) ]
		public decimal ProductPrice { get; set; }

		[ XmlElement( ElementName = "SalePrice" ) ]
		public decimal SalePrice { get; set; }

		[ XmlArray( ElementName = "Categories" ) ]
		[ XmlArrayItem( ElementName = "Category" ) ]
		public List< VolusionProductCategory > Categories { get; set; }

		[ XmlElement( ElementName = "OptionCategory" ) ]
		public List< VolusionOptionCategory > OptionCategories { get; set; }
	}
}
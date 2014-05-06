using System;
using System.Globalization;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionProduct
	{
		[ XmlElement( ElementName = "ProductID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "ProductCode" ) ]
		public string Sku { get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string Name { get; set; }

		[ XmlElement( ElementName = "IsChildOfProductCode" ) ]
		public string IsChildOfSku { get; set; }

		[ XmlElement( ElementName = "StockStatus" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "ProductPrice" ) ]
		public decimal ProductPrice { get; set; }

		[ XmlElement( ElementName = "SalePrice" ) ]
		public decimal SalePrice { get; set; }

		[ XmlIgnore ]
		public DateTime LastModified { get; set; }

		[ XmlElement( ElementName = "LastModified" ) ]
		public string LastModifiedStr
		{
			get { return this.LastModified.ToString( _culture ); }
			set { this.LastModified = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "warehouses" ) ]
		public string Warehouses { get; set; }

		private readonly CultureInfo _culture = new CultureInfo( "en-US" );
	}
}
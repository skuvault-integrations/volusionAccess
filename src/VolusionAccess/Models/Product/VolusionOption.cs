using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionOption
	{
		[ XmlElement( ElementName = "OptionID" ) ]
		public int OptionID { get; set; }

		[ XmlElement( ElementName = "OptionsDesc" ) ]
		public string Description { get; set; }

		[ XmlElement( ElementName = "PriceDiff" ) ]
		public int PriceDiff { get; set; }

		[ XmlElement( ElementName = "ProductID" ) ]
		public int ProductID { get; set; }
	}
}
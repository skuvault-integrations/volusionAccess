using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionProductCategory
	{
		[ XmlElement( ElementName = "CategoryID" ) ]
		public int CategoryID { get; set; }

		[ XmlElement( ElementName = "CategoryName" ) ]
		public string Name { get; set; }
	}
}
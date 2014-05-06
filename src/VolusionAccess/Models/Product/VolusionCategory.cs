using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionCategory
	{
		[ XmlElement( ElementName = "CategoryID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "CategoryName" ) ]
		public string Name { get; set; }
	}
}
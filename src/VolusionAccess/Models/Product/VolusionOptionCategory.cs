using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	public class VolusionOptionCategory
	{
		[ XmlElement( ElementName = "OptionCategoryID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "HeadingGroup" ) ]
		public string Name { get; set; }

		[ XmlElement( ElementName = "OptionCategoriesDesc" ) ]
		public string Description { get; set; }

		[ XmlElement( ElementName = "IsRequired" ) ]
		public string IsRequired { get; set; }

		[ XmlElement( ElementName = "Options" ) ]
		public List< VolusionOption > Options { get; set; }
	}
}
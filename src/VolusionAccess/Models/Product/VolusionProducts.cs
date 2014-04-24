using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	[ XmlRoot( ElementName = "xmldata", Namespace = "" ) ]
	public class VolusionProducts
	{
		[ XmlElement( ElementName = "Products" ) ]
		public List< VolusionProduct > Products { get; set; }
	}
}
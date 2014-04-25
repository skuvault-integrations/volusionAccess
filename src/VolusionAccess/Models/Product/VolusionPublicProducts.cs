using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	[ XmlRoot( ElementName = "All_Products", Namespace = "" ) ]
	public class VolusionPublicProducts
	{
		[ XmlElement( ElementName = "Product" ) ]
		public List< VolusionProduct > Products { get; set; }
	}
}
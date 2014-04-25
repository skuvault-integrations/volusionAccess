using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Product
{
	[ XmlRoot( ElementName = "xmldata", Namespace = "" ) ]
	public class VolusionUpdatedProducts
	{
		[ XmlElement( ElementName = "Products" ) ]
		public List< VolusionUpdatedProduct > Products { get; set; }
	}
}
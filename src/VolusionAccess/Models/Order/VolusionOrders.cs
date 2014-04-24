using System.Collections.Generic;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Order
{
	[ XmlRoot( ElementName = "xmldata", Namespace = "" ) ]
	public class VolusionOrders
	{
		[ XmlElement( ElementName = "Orders" ) ]
		public List< VolusionOrder > Orders { get; set; }
	}
}
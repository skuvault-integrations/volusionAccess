using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ CollectionDataContract( Name = "xmldata", ItemName = "Orders", Namespace = "" ) ]
	public class VolusionOrders : List< VolusionOrder >
	{
		public VolusionOrders()
		{
		}

		public VolusionOrders( IEnumerable< VolusionOrder > orders )
			: base( orders )
		{
		}
	}
}
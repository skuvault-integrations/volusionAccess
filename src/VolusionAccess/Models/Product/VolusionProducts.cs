using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Product
{
	[ CollectionDataContract( Name = "xmldata", ItemName = "Products", Namespace = "" ) ]
	public class VolusionProducts : List< VolusionProduct >
	{
		public VolusionProducts()
		{
		}

		public VolusionProducts( IEnumerable< VolusionProduct > products )
			: base( products )
		{
		}
	}
}
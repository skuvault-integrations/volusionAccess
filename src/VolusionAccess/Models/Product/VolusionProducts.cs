using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Product
{
	[ CollectionDataContract( Name = "xmldata", ItemName = "Products", Namespace = "" ) ]
	public class VolusionProducts : Collection< VolusionProduct >
	{
	}
}
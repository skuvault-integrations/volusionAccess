using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionOrder
	{
		[ DataMember( Name = "o.OrderID" ) ]
		public long Id { get; set; }
	}
}
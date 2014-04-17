using System;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionOrder
	{
		[ DataMember( Name = "o.OrderID" ) ]
		public long Id { get; set; }

		[ DataMember( Name = "o.OrderDate" ) ]
		public DateTime OrderDate { get; set; }

		[ DataMember( Name = "o.OrderStatus" ) ]
		public int OrderStatus { get; set; }
		
		[ DataMember( Name = "o.ShipDate" ) ]
		public DateTime ShipDate { get; set; }

		[ DataMember( Name = "o.Shipped" ) ]
		public bool Shipped { get; set; }

		[ DataMember( Name = "o.Shipping_Locked" ) ]
		public bool ShipShippingLockedped { get; set; }

		[ DataMember( Name = "od.ProductID" ) ]
		public long ProductID { get; set; }

		[ DataMember( Name = "od.ProductCode" ) ]
		public string ProductCode { get; set; }

		[ DataMember( Name = "od.ProductName" ) ]
		public string ProductName { get; set; }

		[ DataMember( Name = "od.ProductPrice" ) ]
		public string ProductPrice { get; set; }

		[ DataMember( Name = "od.Quantity" ) ]
		public int Quantity { get; set; }

		[ DataMember( Name = "od.TotalPrice" ) ]
		public decimal TotalPrice { get; set; }

		public VolusionBillingAddress VolusionBillingAddress { get; set; }

		public VolusionShippingAddress VolusionShippingAddress { get; set; }
	}
}
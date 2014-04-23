using System;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionShippingAddress
	{
		[ DataMember( Name = "ShipAddress1" ) ]
		public string Address1 { get; set; }

		[ DataMember( Name = "ShipAddress2" ) ]
		public string Address2 { get; set; }

		[ DataMember( Name = "ShipCity" ) ]
		public string City { get; set; }

		[ DataMember( Name = "ShipCompanyName" ) ]
		public string Company { get; set; }

		[ DataMember( Name = "ShipCountry" ) ]
		public string Country { get; set; }

		[ DataMember( Name = "ShipDate" ) ]
		public DateTime ShipDate { get; set; }

		[ DataMember( Name = "ShipFaxNumber" ) ]
		public string FaxNumber { get; set; }

		[ DataMember( Name = "ShipFirstName" ) ]
		public string FirstName { get; set; }

		[ DataMember( Name = "ShipLastName" ) ]
		public string LastName { get; set; }

		[ DataMember( Name = "Shipped" ) ]
		public string Shipped { get; set; }

		[ DataMember( Name = "ShipPhoneNumber" ) ]
		public string PhoneNumber { get; set; }

		[ DataMember( Name = "Shipping_Locked" ) ]
		public string ShippingLockedped { get; set; }

		[ DataMember( Name = "ShippingMethodID" ) ]
		public int ShippingMethodID { get; set; }

		[ DataMember( Name = "ShipPostalCode" ) ]
		public string PostalCode { get; set; }

		[ DataMember( Name = "ShipResidential" ) ]
		public string Residential { get; set; }

		[ DataMember( Name = "ShipState" ) ]
		public string State { get; set; }
	}
}
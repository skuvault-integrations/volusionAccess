using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionShippingAddress
	{
		[ DataMember( Name = "o.ShipAddress1" ) ]
		public string Address1 { get; set; }

		[ DataMember( Name = "o.ShipAddress2" ) ]
		public string Address2 { get; set; }

		[ DataMember( Name = "o.ShipCity" ) ]
		public string City { get; set; }

		[ DataMember( Name = "o.ShipCompanyName" ) ]
		public string Company { get; set; }

		[ DataMember( Name = "o.ShipCountry" ) ]
		public string Country { get; set; }

		[ DataMember( Name = "o.ShipState" ) ]
		public string State { get; set; }

		[ DataMember( Name = "o.ShipFirstName" ) ]
		public string FirstName { get; set; }

		[ DataMember( Name = "o.ShipLastName" ) ]
		public string LastName { get; set; }

		[ DataMember( Name = "o.ShipPhoneNumber" ) ]
		public string Phone { get; set; }

		[ DataMember( Name = "o.ShipPostalCode" ) ]
		public string PostalCode { get; set; }

		[ DataMember( Name = "o.ShipFaxNumber" ) ]
		public string ShipFaxNumber { get; set; }
	}
}
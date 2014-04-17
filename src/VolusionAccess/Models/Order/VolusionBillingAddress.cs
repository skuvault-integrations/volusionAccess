using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionBillingAddress
	{
		[ DataMember( Name = "o.BillingAddress1" ) ]
		public string Address1 { get; set; }

		[ DataMember( Name = "o.BillingAddress" ) ]
		public string Address2 { get; set; }

		[ DataMember( Name = "o.BillingCity" ) ]
		public string City { get; set; }

		[ DataMember( Name = "o.BillingCompanyName" ) ]
		public string Company { get; set; }

		[ DataMember( Name = "o.BillingCountry" ) ]
		public string Country { get; set; }

		[ DataMember( Name = "o.BillingState" ) ]
		public string State { get; set; }

		[ DataMember( Name = "o.BillingFirstName" ) ]
		public string FirstName { get; set; }

		[ DataMember( Name = "o.BillingLastName" ) ]
		public string LastName { get; set; }

		[ DataMember( Name = "o.BillingPhoneNumber" ) ]
		public string Phone { get; set; }

		[ DataMember( Name = "o.BillingPostalCode" ) ]
		public string PostalCode { get; set; }

		[ DataMember( Name = "o.BillingFaxNumber" ) ]
		public string ShipFaxNumber { get; set; }
	}
}
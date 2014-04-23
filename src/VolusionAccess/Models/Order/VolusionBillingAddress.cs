using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract ]
	public class VolusionBillingAddress
	{
		[ DataMember( Name = "BillingAddress1" ) ]
		public string Address1 { get; set; }

		[ DataMember( Name = "BillingAddress2" ) ]
		public string Address2 { get; set; }

		[ DataMember( Name = "BillingCity" ) ]
		public string City { get; set; }

		[ DataMember( Name = "BillingCompanyName" ) ]
		public string Company { get; set; }

		[ DataMember( Name = "BillingCountry" ) ]
		public string Country { get; set; }

		[ DataMember( Name = "BillingFaxNumber" ) ]
		public string FaxNumber { get; set; }

		[ DataMember( Name = "BillingFirstName" ) ]
		public string FirstName { get; set; }

		[ DataMember( Name = "BillingLastName" ) ]
		public string LastName { get; set; }

		[ DataMember( Name = "BillingPhoneNumber" ) ]
		public string PhoneNumber { get; set; }

		[ DataMember( Name = "BillingPostalCode" ) ]
		public string PostalCode { get; set; }

		[ DataMember( Name = "BillingState" ) ]
		public string State { get; set; }
	}
}
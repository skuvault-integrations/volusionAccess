using System;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract( Name = "Orders", Namespace = "" ) ]
	public class VolusionOrder
	{
		[ DataMember( Name = "OrderID" ) ]
		public int Id { get; set; }

		[ DataMember( Name = "AddressValidated" ) ]
		public string AddressValidated { get; set; }

		[ DataMember( Name = "CancelDate" ) ]
		public DateTime CancelDate { get; set; }

		[ DataMember( Name = "CancelReason" ) ]
		public string CancelReason { get; set; }

		[ DataMember( Name = "InitiallyShippedDate" ) ]
		public DateTime InitiallyShippedDate { get; set; }

		[ DataMember( Name = "LastModified" ) ]
		public DateTime LastModified { get; set; }

		[ DataMember( Name = "OrderDate" ) ]
		public DateTime OrderDate { get; set; }

		[ DataMember( Name = "OrderDateUtc" ) ]
		public DateTime OrderDateUtc { get; set; }

		[ DataMember( Name = "OrderStatus" ) ]
		public string OrderStatus { get; set; }

		[ DataMember( Name = "PaymentAmount" ) ]
		public Decimal PaymentAmount { get; set; }

		[ DataMember( Name = "PaymentDeclined" ) ]
		public string PaymentDeclined { get; set; }

		[ DataMember( Name = "PaymentMethodID" ) ]
		public int PaymentMethodID { get; set; }

		[ DataMember( Name = "Stock_Priority" ) ]
		public int StockPriority { get; set; }

		[ DataMember( Name = "Total_Payment_Authorized" ) ]
		public decimal TotalPaymentAuthorized { get; set; }

		[ DataMember( Name = "Total_Payment_Received" ) ]
		public decimal TotalPaymentReceived { get; set; }

		[ DataMember( Name = "TotalShippingCost" ) ]
		public decimal TotalShippingCost { get; set; }

		#region billing
		[ DataMember( Name = "BillingAddress1" ) ]
		public string BillingAddress1 { get; set; }

		[ DataMember( Name = "BillingAddress2" ) ]
		public string BillingAddress2 { get; set; }

		[ DataMember( Name = "BillingCity" ) ]
		public string BillingCity { get; set; }

		[ DataMember( Name = "BillingCompanyName" ) ]
		public string BillingCompanyName { get; set; }

		[ DataMember( Name = "BillingCountry" ) ]
		public string BillingCountry { get; set; }

		[ DataMember( Name = "BillingFaxNumber" ) ]
		public string BillingFaxNumber { get; set; }

		[ DataMember( Name = "BillingFirstName" ) ]
		public string BillingFirstName { get; set; }

		[ DataMember( Name = "BillingLastName" ) ]
		public string BillingLastName { get; set; }

		[ DataMember( Name = "BillingPhoneNumber" ) ]
		public string BillingPhoneNumber { get; set; }

		[ DataMember( Name = "BillingPostalCode" ) ]
		public string BillingPostalCode { get; set; }

		[ DataMember( Name = "BillingState" ) ]
		public string BillingState { get; set; }
		#endregion

		#region shipping
		[ DataMember( Name = "ShipAddress1" ) ]
		public string ShipAddress1 { get; set; }

		[ DataMember( Name = "ShipAddress2" ) ]
		public string ShipAddress2 { get; set; }

		[ DataMember( Name = "ShipCity" ) ]
		public string ShipCity { get; set; }

		[ DataMember( Name = "ShipCompanyName" ) ]
		public string ShipCompanyName { get; set; }

		[ DataMember( Name = "ShipCountry" ) ]
		public string ShipCountry { get; set; }

		[ DataMember( Name = "ShipDate" ) ]
		public DateTime ShipDate { get; set; }

		[ DataMember( Name = "ShipFaxNumber" ) ]
		public string ShipFaxNumber { get; set; }

		[ DataMember( Name = "ShipFirstName" ) ]
		public string ShipFirstName { get; set; }

		[ DataMember( Name = "ShipLastName" ) ]
		public string ShipLastName { get; set; }

		[ DataMember( Name = "Shipped" ) ]
		public string Shipped { get; set; }

		[ DataMember( Name = "ShipPhoneNumber" ) ]
		public string ShipPhoneNumber { get; set; }

		[ DataMember( Name = "Shipping_Locked" ) ]
		public string ShippingLockedped { get; set; }

		[ DataMember( Name = "ShippingMethodID" ) ]
		public int ShippingMethodID { get; set; }

		[ DataMember( Name = "ShipPostalCode" ) ]
		public string ShipPostalCode { get; set; }

		[ DataMember( Name = "ShipResidential" ) ]
		public string ShipResidential { get; set; }

		[ DataMember( Name = "ShipState" ) ]
		public string ShipState { get; set; }
		#endregion

		[ DataMember( Name = "OrderDetails" ) ]
		public VolusionOrderDetails OrderDetails { get; set; }
	}
}
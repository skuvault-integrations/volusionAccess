using System;
using System.Globalization;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Order
{
	public class VolusionOrder
	{
		[ XmlElement( ElementName = "OrderID" ) ]
		public string OrderID { get; set; }

		[ XmlElement( ElementName = "AddressValidated" ) ]
		public string AddressValidated { get; set; }

		[ XmlElement( ElementName = "AccountNumber" ) ]
		public string AccountNumber { get; set; }

		[ XmlElement( ElementName = "AccountType" ) ]
		public string AccountType { get; set; }

		[ XmlIgnore ]
		public DateTime CancelDate { get; set; }

		[ XmlElement( ElementName = "CancelDate" ) ]
		public string CancelDateStr
		{
			get { return this.CancelDate.ToString( _culture ); }
			set { this.CancelDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "CancelReason" ) ]
		public string CancelReason { get; set; }

		[ XmlIgnore ]
		public DateTime InitiallyShippedDate { get; set; }

		[ XmlElement( ElementName = "InitiallyShippedDate" ) ]
		public string InitiallyShippedDateStr
		{
			get { return this.InitiallyShippedDate.ToString( _culture ); }
			set { this.InitiallyShippedDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlIgnore ]
		public DateTime LastModified { get; set; }

		[ XmlElement( ElementName = "LastModified" ) ]
		public string LastModifiedStr
		{
			get { return this.LastModified.ToString( _culture ); }
			set { this.LastModified = DateTime.Parse( value, _culture ); }
		}

		[ XmlIgnore ]
		public DateTime OrderDate { get; set; }

		[ XmlElement( ElementName = "OrderDate" ) ]
		public string OrderDateStr
		{
			get { return this.OrderDate.ToString( _culture ); }
			set { this.OrderDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlIgnore ]
		public DateTime OrderDateUtc { get; set; }

		[ XmlElement( ElementName = "OrderDateUtc" ) ]
		public string OrderDateUtcStr
		{
			get { return this.OrderDateUtc.ToString( _culture ); }
			set { this.OrderDateUtc = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "OrderStatus" ) ]
		public string OrderStatus { get; set; }

		[ XmlElement( ElementName = "PaymentAmount" ) ]
		public Decimal PaymentAmount { get; set; }

		[ XmlElement( ElementName = "PaymentDeclined" ) ]
		public string PaymentDeclined { get; set; }

		[ XmlElement( ElementName = "PaymentMethodID" ) ]
		public int PaymentMethodID { get; set; }

		[ XmlElement( ElementName = "Stock_Priority" ) ]
		public int StockPriority { get; set; }

		[ XmlElement( ElementName = "Total_Payment_Authorized" ) ]
		public decimal TotalPaymentAuthorized { get; set; }

		[ XmlElement( ElementName = "Total_Payment_Received" ) ]
		public decimal TotalPaymentReceived { get; set; }

		[ XmlElement( ElementName = "TotalShippingCost" ) ]
		public decimal TotalShippingCost { get; set; }

		#region billing
		[ XmlElement( ElementName = "BillingAddress1" ) ]
		public string BillingAddress1 { get; set; }

		[ XmlElement( ElementName = "BillingAddress2" ) ]
		public string BillingAddress2 { get; set; }

		[ XmlElement( ElementName = "BillingCity" ) ]
		public string BillingCity { get; set; }

		[ XmlElement( ElementName = "BillingCompanyName" ) ]
		public string BillingCompanyName { get; set; }

		[ XmlElement( ElementName = "BillingCountry" ) ]
		public string BillingCountry { get; set; }

		[ XmlElement( ElementName = "BillingFaxNumber" ) ]
		public string BillingFaxNumber { get; set; }

		[ XmlElement( ElementName = "BillingFirstName" ) ]
		public string BillingFirstName { get; set; }

		[ XmlElement( ElementName = "BillingLastName" ) ]
		public string BillingLastName { get; set; }

		[ XmlElement( ElementName = "BillingPhoneNumber" ) ]
		public string BillingPhoneNumber { get; set; }

		[ XmlElement( ElementName = "BillingPostalCode" ) ]
		public string BillingPostalCode { get; set; }

		[ XmlElement( ElementName = "BillingState" ) ]
		public string BillingState { get; set; }
		#endregion

		#region shipping
		[ XmlElement( ElementName = "ShipAddress1" ) ]
		public string ShipAddress1 { get; set; }

		[ XmlElement( ElementName = "ShipAddress2" ) ]
		public string ShipAddress2 { get; set; }

		[ XmlElement( ElementName = "ShipCity" ) ]
		public string ShipCity { get; set; }

		[ XmlElement( ElementName = "ShipCompanyName" ) ]
		public string ShipCompanyName { get; set; }

		[ XmlElement( ElementName = "ShipCountry" ) ]
		public string ShipCountry { get; set; }

		[ XmlIgnore ]
		public DateTime ShipDate { get; set; }

		[ XmlElement( ElementName = "ShipDate" ) ]
		public string ShipDateStr
		{
			get { return this.ShipDate.ToString( _culture ); }
			set { this.ShipDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "ShipFaxNumber" ) ]
		public string ShipFaxNumber { get; set; }

		[ XmlElement( ElementName = "ShipFirstName" ) ]
		public string ShipFirstName { get; set; }

		[ XmlElement( ElementName = "ShipLastName" ) ]
		public string ShipLastName { get; set; }

		[ XmlElement( ElementName = "Shipped" ) ]
		public string Shipped { get; set; }

		[ XmlElement( ElementName = "ShipPhoneNumber" ) ]
		public string ShipPhoneNumber { get; set; }

		[ XmlElement( ElementName = "Shipping_Locked" ) ]
		public string ShippingLockedped { get; set; }

		[ XmlElement( ElementName = "ShippingMethodID" ) ]
		public int ShippingMethodID { get; set; }

		[ XmlElement( ElementName = "ShipPostalCode" ) ]
		public string ShipPostalCode { get; set; }

		[ XmlElement( ElementName = "ShipResidential" ) ]
		public string ShipResidential { get; set; }

		[ XmlElement( ElementName = "ShipState" ) ]
		public string ShipState { get; set; }
		#endregion

		[ XmlElement( ElementName = "OrderDetails" ) ]
		public VolusionOrderDetails OrderDetails { get; set; }

		private readonly CultureInfo _culture = new CultureInfo( "en-US" );
	}
}
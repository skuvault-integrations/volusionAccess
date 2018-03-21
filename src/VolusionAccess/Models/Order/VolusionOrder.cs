using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Netco.Extensions;

namespace VolusionAccess.Models.Order
{
	public class VolusionOrder : IEquatable< VolusionOrder >
	{
		[ XmlElement( ElementName = "OrderID" ) ]
		public int Id { get; set; }

		[ XmlElement( ElementName = "AccountNumber" ) ]
		public string AccountNumber { get; set; }

		[ XmlElement( ElementName = "AccountType" ) ]
		public string AccountType { get; set; }

		[ XmlElement( ElementName = "AddressValidated" ) ]
		public string AddressValidated { get; set; }

		[ XmlElement( ElementName = "Affiliate_Commissionable_Value" ) ]
		public decimal AffiliateCommissionableValue { get; set; }

		[ XmlElement( ElementName = "BankName" ) ]
		public string BankName { get; set; }

		[ XmlElement( ElementName = "CustomerID" ) ]
		public int CustomerID { get; set; }

		[ XmlElement( ElementName = "IsAGift" ) ]
		public string IsAGift { get; set; }

		[ XmlElement( ElementName = "IsGTSOrder" ) ]
		public string IsGTSOrder { get; set; }

		[ XmlIgnore ]
		public bool IsLocked
		{
			get { return this.IsLockedStr == "Y"; }
		}

		[ XmlElement( ElementName = "Locked" ) ]
		public string IsLockedStr { get; set; }

		[ XmlElement( ElementName = "Order_Entry_System" ) ]
		public string OrderEntrySystem { get; set; }

		[ XmlElement( ElementName = "CancelDate" ) ]
		public string CancelDateStr
		{
			get { return this.CancelDate.ToString( this._culture ); }
			set { this.CancelDate = DateTime.Parse( value, this._culture ); }
		}

		[ XmlIgnore ]
		public DateTime CancelDate { get; set; }

		[ XmlIgnore ]
		public DateTime CancelDateUtc
		{
			get { return this.GetUtcDate( this.CancelDate ); }
		}

		[ XmlElement( ElementName = "CancelReason" ) ]
		public string CancelReason { get; set; }

		[ XmlElement( ElementName = "InitiallyShippedDate" ) ]
		public string InitiallyShippedDateStr
		{
			get { return this.InitiallyShippedDate.ToString( this._culture ); }
			set { this.InitiallyShippedDate = DateTime.Parse( value, this._culture ); }
		}

		[ XmlIgnore ]
		public DateTime InitiallyShippedDate { get; set; }

		[ XmlIgnore ]
		public DateTime InitiallyShippedDateUtc
		{
			get { return this.GetUtcDate( this.InitiallyShippedDate ); }
		}

		[ XmlElement( ElementName = "LastModified" ) ]
		public string LastModifiedStr
		{
			get { return this.LastModified.ToString( this._culture ); }
			set { this.LastModified = DateTime.Parse( value, this._culture ); }
		}

		[ XmlIgnore ]
		public DateTime LastModified { get; set; }

		[ XmlIgnore ]
		public DateTime LastModifiedUtc
		{
			get { return this.GetUtcDate( this.LastModified ); }
		}

		[ XmlElement( ElementName = "OrderDate" ) ]
		public string OrderDateStr
		{
			get { return this.OrderDate.ToString( this._culture ); }
			set { this.OrderDate = DateTime.Parse( value, this._culture ); }
		}

		[ XmlIgnore ]
		public DateTime OrderDate { get; set; }

		[ XmlElement( ElementName = "OrderDateUtc" ) ]
		public string OrderDateUtcStr { get; set; }

		[ XmlIgnore ]
		public DateTime OrderDateUtc
		{
			get { return string.IsNullOrEmpty( this.OrderDateUtcStr ) ? this.OrderDate.AddHours( -this.DefaultTimeZone ) : DateTime.Parse( this.OrderDateUtcStr, this._culture ); }
		}

		[ XmlIgnore ]
		public VolusionOrderStatusEnum OrderStatus
		{
			get
			{
				if( string.Equals( this.OrderStatusStr, "New - See Order Notes", StringComparison.InvariantCultureIgnoreCase ) )
					return VolusionOrderStatusEnum.NewSeeOrderNotes;

				if( string.Equals( this.OrderStatusStr, "Awaiting Payment - See Order Notes", StringComparison.InvariantCultureIgnoreCase ) )
					return VolusionOrderStatusEnum.AwaitingPaymentSeeOrderNotes;

				return this.OrderStatusStr.ToEnum< VolusionOrderStatusEnum >();
			}
		}
		
		[ XmlElement( ElementName = "Order_Comments" ) ]
		public string OrderComments{ get; set; }

		[ XmlElement( ElementName = "OrderStatus" ) ]
		public string OrderStatusStr { get; set; }

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

		#region tax
		[ XmlElement( ElementName = "SalesTax1" ) ]
		public decimal SalesTax1 { get; set; }

		[ XmlElement( ElementName = "SalesTax2" ) ]
		public decimal SalesTax2 { get; set; }

		[ XmlElement( ElementName = "SalesTax3" ) ]
		public decimal SalesTax3 { get; set; }

		[ XmlElement( ElementName = "SalesTaxRate" ) ]
		public decimal SalesTaxRate { get; set; }

		[ XmlElement( ElementName = "SalesTaxRate1" ) ]
		public decimal SalesTaxRate1 { get; set; }

		[ XmlElement( ElementName = "SalesTaxRate2" ) ]
		public decimal SalesTaxRate2 { get; set; }

		[ XmlElement( ElementName = "SalesTaxRate3" ) ]
		public decimal SalesTaxRate3 { get; set; }
		#endregion

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

		[ XmlElement( ElementName = "ShipDate" ) ]
		public string ShipDateStr
		{
			get { return this.ShipDate.ToString( this._culture ); }
			set { this.ShipDate = DateTime.Parse( value, this._culture ); }
		}

		[ XmlIgnore ]
		public DateTime ShipDate { get; set; }

		[ XmlIgnore ]
		public DateTime ShipDateUtc
		{
			get { return this.GetUtcDate( this.ShipDate ); }
		}

		[ XmlElement( ElementName = "ShipFaxNumber" ) ]
		public string ShipFaxNumber { get; set; }

		[ XmlElement( ElementName = "ShipFirstName" ) ]
		public string ShipFirstName { get; set; }

		[ XmlElement( ElementName = "ShipLastName" ) ]
		public string ShipLastName { get; set; }

		[ XmlIgnore ]
		public bool IsShipped
		{
			get { return this.IsShippedStr == "Y"; }
		}

		[ XmlElement( ElementName = "Shipped" ) ]
		public string IsShippedStr { get; set; }

		[ XmlElement( ElementName = "ShipPhoneNumber" ) ]
		public string ShipPhoneNumber { get; set; }

		[ XmlIgnore ]
		public bool IsShippingLocked
		{
			get { return this.IsShippingLockedStr == "Y"; }
		}

		[ XmlElement( ElementName = "Shipping_Locked" ) ]
		public string IsShippingLockedStr { get; set; }

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
		public List< VolusionOrderDetails > OrderDetails { get; set; }

		[ XmlIgnore ]
		public int DefaultTimeZone { get; internal set; }

		public int TimeZone
		{
			get
			{
				if( this._timeZone == int.MinValue )
					this._timeZone = this.OrderDate == DateTime.MinValue || this.OrderDateUtc == DateTime.MinValue ? this.DefaultTimeZone : ( this.OrderDate - this.OrderDateUtc ).Hours;
				return this._timeZone;
			}
		}

		private int _timeZone = int.MinValue;
		private readonly CultureInfo _culture = new CultureInfo( "en-US" );

		private DateTime GetUtcDate( DateTime localDateTime )
		{
			return localDateTime == DateTime.MinValue ? DateTime.MinValue : localDateTime.AddHours( -this.TimeZone );
		}

		public bool Equals( VolusionOrder other )
		{
			if( ReferenceEquals( null, other ) )
				return false;
			if( ReferenceEquals( this, other ) )
				return true;
			return this.Id == other.Id;
		}

		public override bool Equals( object obj )
		{
			if( ReferenceEquals( null, obj ) )
				return false;
			if( ReferenceEquals( this, obj ) )
				return true;
			if( obj.GetType() != this.GetType() )
				return false;
			return this.Equals( ( VolusionOrder )obj );
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return this.Id.GetHashCode() * 397;
			}
		}
	}

	public enum VolusionOrderStatusEnum
	{
		New,
		Pending,
		Processing,
		PaymentDeclined,
		AwaitingPayment,
		ReadyToShip,
		PendingShipment,
		PartiallyShipped,
		Shipped,
		PartiallyBackordered,
		Backordered,
		SeeLineItems,
		SeeOrderNotes,
		PartiallyReturned,
		Returned,
		Cancelled,
		CancelOrder,
		NewSeeOrderNotes,
		AwaitingPaymentSeeOrderNotes
	}
}
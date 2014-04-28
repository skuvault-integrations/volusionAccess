using System;
using System.Globalization;
using System.Xml.Serialization;

namespace VolusionAccess.Models.Order
{
	public class VolusionOrderDetails
	{
		[ XmlElement( ElementName = "OrderDetailID" ) ]
		public int OrderDetailID { get; set; }

		[ XmlElement( ElementName = "AutoDropShip" ) ]
		public string AutoDropShip { get; set; }

		[ XmlElement( ElementName = "CategoryID" ) ]
		public int CategoryID { get; set; }

		[ XmlElement( ElementName = "CouponCode" ) ]
		public string CouponCode { get; set; }

		[ XmlElement( ElementName = "CustomLineItem" ) ]
		public string CustomLineItem { get; set; }

		[ XmlElement( ElementName = "Fixed_ShippingCost" ) ]
		public decimal FixedShippingCost { get; set; }

		[ XmlElement( ElementName = "Fixed_ShippingCost_Outside_LocalRegion" ) ]
		public decimal FixedShippingCostOutsideLocalRegion { get; set; }

		[ XmlElement( ElementName = "FreeShippingItem" ) ]
		public string FreeShippingItem { get; set; }

		[ XmlElement( ElementName = "GiftTrakNumber" ) ]
		public int GiftTrakNumber { get; set; }

		[ XmlElement( ElementName = "GiftWrapCost" ) ]
		public decimal GiftWrapCost { get; set; }

		[ XmlElement( ElementName = "IsKitID" ) ]
		public string IsKitID { get; set; }

		[ XmlElement( ElementName = "KitID" ) ]
		public string KitID { get; set; }

		[ XmlElement( ElementName = "Locked" ) ]
		public string Locked { get; set; }

		[ XmlIgnore ]
		public DateTime LastModified { get; set; }

		[ XmlElement( ElementName = "LastModified" ) ]
		public string LastModifiedStr
		{
			get { return this.LastModified.ToString( _culture ); }
			set { this.LastModified = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "OnOrder_Qty" ) ]
		public int OnOrderQty { get; set; }

		[ XmlElement( ElementName = "OptionID" ) ]
		public int OptionID { get; set; }

		[ XmlElement( ElementName = "OptionIDs" ) ]
		public string OptionIDs { get; set; }

		[ XmlElement( ElementName = "Package_Type" ) ]
		public string PackageType { get; set; }

		[ XmlElement( ElementName = "Product_Keys_Shipped" ) ]
		public string ProductKeysShipped { get; set; }

		[ XmlElement( ElementName = "ProductCode" ) ]
		public string ProductCode { get; set; }

		[ XmlElement( ElementName = "ProductID" ) ]
		public int ProductID { get; set; }

		[ XmlElement( ElementName = "ProductName" ) ]
		public string ProductName { get; set; }

		[ XmlElement( ElementName = "ProductPrice" ) ]
		public decimal ProductPrice { get; set; }

		[ XmlElement( ElementName = "QtyOnBackOrder" ) ]
		public int QtyOnBackOrder { get; set; }

		[ XmlElement( ElementName = "QtyOnHold" ) ]
		public int QtyOnHold { get; set; }

		[ XmlElement( ElementName = "QtyOnPackingSlip" ) ]
		public int QtyOnPackingSlip { get; set; }

		[ XmlElement( ElementName = "QtyShipped" ) ]
		public int QtyShipped { get; set; }

		[ XmlElement( ElementName = "Quantity" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "Returned" ) ]
		public string Returned { get; set; }

		[ XmlIgnore ]
		public DateTime ReturnedDate { get; set; }

		[ XmlElement( ElementName = "Returned_Date" ) ]
		public string ReturnedDateStr
		{
			get { return this.ReturnedDate.ToString( _culture ); }
			set { this.ReturnedDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "Reward_Points_Given_For_Purchase" ) ]
		public int RewardPointsGivenForPurchase { get; set; }

		[ XmlIgnore ]
		public DateTime ShipDate { get; set; }

		[ XmlElement( ElementName = "ShipDate" ) ]
		public string ShipDateStr
		{
			get { return this.ShipDate.ToString( _culture ); }
			set { this.ShipDate = DateTime.Parse( value, _culture ); }
		}

		[ XmlElement( ElementName = "Shipped" ) ]
		public string Shipped { get; set; }

		[ XmlElement( ElementName = "Ships_By_Itself" ) ]
		public string ShipsByItself { get; set; }

		[ XmlElement( ElementName = "TaxableProduct" ) ]
		public string TaxableProduct { get; set; }

		[ XmlElement( ElementName = "TotalPrice" ) ]
		public decimal TotalPrice { get; set; }

		[ XmlElement( ElementName = "Vendor_Price" ) ]
		public decimal Vendor_Price { get; set; }

		[ XmlElement( ElementName = "Warehouses" ) ]
		public string Warehouses { get; set; }

		private readonly CultureInfo _culture = new CultureInfo( "en-US" );
	}
}
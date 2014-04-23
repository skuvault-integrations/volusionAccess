using System;
using System.Runtime.Serialization;

namespace VolusionAccess.Models.Order
{
	[ DataContract( Name = "OrderDetails", Namespace = "" ) ]
	public class VolusionOrderDetails
	{
		[ DataMember( Name = "OrderDetailID" ) ]
		public int OrderDetailID { get; set; }

		[ DataMember( Name = "AutoDropShip" ) ]
		public string AutoDropShip { get; set; }

		[ DataMember( Name = "CategoryID" ) ]
		public int CategoryID { get; set; }

		[ DataMember( Name = "CouponCode" ) ]
		public string CouponCode { get; set; }

		[ DataMember( Name = "CustomLineItem" ) ]
		public string CustomLineItem { get; set; }

		[ DataMember( Name = "Fixed_ShippingCost" ) ]
		public decimal FixedShippingCost { get; set; }

		[ DataMember( Name = "Fixed_ShippingCost_Outside_LocalRegion" ) ]
		public decimal FixedShippingCostOutsideLocalRegion { get; set; }

		[ DataMember( Name = "FreeShippingItem" ) ]
		public string FreeShippingItem { get; set; }

		[ DataMember( Name = "IsKitID" ) ]
		public string IsKitID { get; set; }

		[ DataMember( Name = "KitID" ) ]
		public string KitID { get; set; }

		[ DataMember( Name = "Locked" ) ]
		public string Locked { get; set; }

		[ DataMember( Name = "LastModified" ) ]
		public DateTime LastModified { get; set; }

		[ DataMember( Name = "OnOrder_Qty" ) ]
		public int OnOrderQty { get; set; }

		[ DataMember( Name = "OptionID" ) ]
		public int OptionID { get; set; }

		[ DataMember( Name = "OptionIDs" ) ]
		public string OptionIDs { get; set; }

		[ DataMember( Name = "Package_Type" ) ]
		public string PackageType { get; set; }

		[ DataMember( Name = "Product_Keys_Shipped" ) ]
		public string ProductKeysShipped { get; set; }

		[ DataMember( Name = "ProductCode" ) ]
		public string ProductCode { get; set; }

		[ DataMember( Name = "ProductID" ) ]
		public int ProductID { get; set; }

		[ DataMember( Name = "ProductName" ) ]
		public string ProductName { get; set; }

		[ DataMember( Name = "ProductPrice" ) ]
		public decimal ProductPrice { get; set; }

		[ DataMember( Name = "QtyOnBackOrder" ) ]
		public int QtyOnBackOrder { get; set; }

		[ DataMember( Name = "QtyOnHold" ) ]
		public int QtyOnHold { get; set; }

		[ DataMember( Name = "QtyOnPackingSlip" ) ]
		public int QtyOnPackingSlip { get; set; }

		[ DataMember( Name = "QtyShipped" ) ]
		public int QtyShipped { get; set; }

		[ DataMember( Name = "Quantity" ) ]
		public int Quantity { get; set; }

		[ DataMember( Name = "Returned" ) ]
		public string Returned { get; set; }

		[ DataMember( Name = "Returned_Date" ) ]
		public DateTime ReturnedDate { get; set; }

		[ DataMember( Name = "Reward_Points_Given_For_Purchase" ) ]
		public int RewardPointsGivenForPurchase { get; set; }

		[ DataMember( Name = "ShipDate" ) ]
		public DateTime ShipDate { get; set; }

		[ DataMember( Name = "Shipped" ) ]
		public string Shipped { get; set; }

		[ DataMember( Name = "Ships_By_Itself" ) ]
		public string ShipsByItself { get; set; }

		[ DataMember( Name = "TaxableProduct" ) ]
		public string TaxableProduct { get; set; }

		[ DataMember( Name = "TotalPrice" ) ]
		public decimal TotalPrice { get; set; }

		[ DataMember( Name = "Vendor_Price" ) ]
		public decimal Vendor_Price { get; set; }

		[ DataMember( Name = "Warehouses" ) ]
		public string Warehouses { get; set; }
	}
}
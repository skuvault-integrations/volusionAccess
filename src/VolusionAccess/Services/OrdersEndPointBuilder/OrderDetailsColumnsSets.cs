using System.Collections.Generic;
using Netco.Extensions;

namespace VolusionAccess.Services.OrdersEndPointBuilder
{
	public class OrderDetailsColumnsSets
	{
		public static HashSet< string > AllColumnsSet
		{
			get { return _allColumnsSet.ToHashSet(); }
		}

		public static HashSet< string > MinimalColumnsSet
		{
			get { return _minimalColumnsSet.ToHashSet(); }
		}

		private static readonly HashSet< string > _allColumnsSet = new HashSet< string >()
		{
			OrderDetailsColumnsNamesWithPrefix.OrderDetailID,
			OrderDetailsColumnsNamesWithPrefix.AutoDropShip,
			OrderDetailsColumnsNamesWithPrefix.CategoryID,
			OrderDetailsColumnsNamesWithPrefix.CouponCode,
			OrderDetailsColumnsNamesWithPrefix.CustomLineItem,
			OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost,
			OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost_Outside_LocalRegion,
			OrderDetailsColumnsNamesWithPrefix.FreeShippingItem,
			OrderDetailsColumnsNamesWithPrefix.GiftTrakNumber,
			OrderDetailsColumnsNamesWithPrefix.GiftWrapCost,
			OrderDetailsColumnsNamesWithPrefix.IsKitID,
			OrderDetailsColumnsNamesWithPrefix.KitID,
			OrderDetailsColumnsNamesWithPrefix.Locked,
			OrderDetailsColumnsNamesWithPrefix.LastModified,
			OrderDetailsColumnsNamesWithPrefix.OnOrder_Qty,
			OrderDetailsColumnsNamesWithPrefix.OptionID,
			OrderDetailsColumnsNamesWithPrefix.OptionIDs,
			OrderDetailsColumnsNamesWithPrefix.Package_Type,
			OrderDetailsColumnsNamesWithPrefix.Product_Keys_Shipped,
			OrderDetailsColumnsNamesWithPrefix.ProductCode,
			OrderDetailsColumnsNamesWithPrefix.ProductID,
			OrderDetailsColumnsNamesWithPrefix.ProductName,
			OrderDetailsColumnsNamesWithPrefix.ProductPrice,
			OrderDetailsColumnsNamesWithPrefix.QtyOnBackOrder,
			OrderDetailsColumnsNamesWithPrefix.QtyOnHold,
			OrderDetailsColumnsNamesWithPrefix.QtyOnPackingSlip,
			OrderDetailsColumnsNamesWithPrefix.QtyShipped,
			OrderDetailsColumnsNamesWithPrefix.Quantity,
			OrderDetailsColumnsNamesWithPrefix.Returned,
			OrderDetailsColumnsNamesWithPrefix.Returned_Date,
			OrderDetailsColumnsNamesWithPrefix.Reward_Points_Given_For_Purchase,
			OrderDetailsColumnsNamesWithPrefix.ShipDate,
			OrderDetailsColumnsNamesWithPrefix.Shipped,
			OrderDetailsColumnsNamesWithPrefix.Ships_By_Itself,
			OrderDetailsColumnsNamesWithPrefix.TaxableProduct,
			OrderDetailsColumnsNamesWithPrefix.TotalPrice,
			OrderDetailsColumnsNamesWithPrefix.Vendor_Price,
			OrderDetailsColumnsNamesWithPrefix.Warehouses,
		};

		private static readonly HashSet< string > _minimalColumnsSet = new HashSet< string >()
		{
			OrderDetailsColumnsNamesWithPrefix.OrderDetailID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.AutoDropShip,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.CategoryID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.CouponCode,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.CustomLineItem,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost_Outside_LocalRegion,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.FreeShippingItem,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.GiftTrakNumber,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.GiftWrapCost,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.IsKitID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.KitID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Locked,
			OrderDetailsColumnsNamesWithPrefix.LastModified,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.OnOrder_Qty,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.OptionID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.OptionIDs,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Package_Type,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Product_Keys_Shipped,
			OrderDetailsColumnsNamesWithPrefix.ProductCode,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.ProductID,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.ProductName,
			OrderDetailsColumnsNamesWithPrefix.ProductPrice,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.QtyOnBackOrder,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.QtyOnHold,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.QtyOnPackingSlip,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.QtyShipped,
			OrderDetailsColumnsNamesWithPrefix.Quantity,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Returned,
			OrderDetailsColumnsNamesWithPrefix.Returned_Date,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Reward_Points_Given_For_Purchase,
			OrderDetailsColumnsNamesWithPrefix.ShipDate,
			OrderDetailsColumnsNamesWithPrefix.Shipped,
			OrderDetailsColumnsNamesWithPrefix.Ships_By_Itself,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.TaxableProduct,
			OrderDetailsColumnsNamesWithPrefix.TotalPrice,
			OrderDetailsColumnsNamesWithPrefix.Vendor_Price,
			// we are sure it is not used OrderDetailsColumnsNamesWithPrefix.Warehouses
		};
	}
}
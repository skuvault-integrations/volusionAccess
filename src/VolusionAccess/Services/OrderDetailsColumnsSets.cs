using System.Collections.Generic;
using Netco.Extensions;

namespace VolusionAccess.Services
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
			//OrderDetailsColumnsNamesWithPrefix.AutoDropShip,
			//OrderDetailsColumnsNamesWithPrefix.CategoryID,
			//OrderDetailsColumnsNamesWithPrefix.CouponCode,
			//OrderDetailsColumnsNamesWithPrefix.CustomLineItem,
			//OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost,
			//OrderDetailsColumnsNamesWithPrefix.Fixed_ShippingCost_Outside_LocalRegion,
			//OrderDetailsColumnsNamesWithPrefix.FreeShippingItem,
			//OrderDetailsColumnsNamesWithPrefix.GiftTrakNumber,
			//OrderDetailsColumnsNamesWithPrefix.GiftWrapCost,
			//OrderDetailsColumnsNamesWithPrefix.IsKitID,
			//OrderDetailsColumnsNamesWithPrefix.KitID,
			//OrderDetailsColumnsNamesWithPrefix.Locked,
			OrderDetailsColumnsNamesWithPrefix.LastModified,
			//OrderDetailsColumnsNamesWithPrefix.OnOrder_Qty,
			//OrderDetailsColumnsNamesWithPrefix.OptionID,
			//OrderDetailsColumnsNamesWithPrefix.OptionIDs,
			//OrderDetailsColumnsNamesWithPrefix.Package_Type,
			//OrderDetailsColumnsNamesWithPrefix.Product_Keys_Shipped,
			OrderDetailsColumnsNamesWithPrefix.ProductCode,
			//OrderDetailsColumnsNamesWithPrefix.ProductID,
			//OrderDetailsColumnsNamesWithPrefix.ProductName,
			OrderDetailsColumnsNamesWithPrefix.ProductPrice,
			//OrderDetailsColumnsNamesWithPrefix.QtyOnBackOrder,
			//OrderDetailsColumnsNamesWithPrefix.QtyOnHold,
			//OrderDetailsColumnsNamesWithPrefix.QtyOnPackingSlip,
			//OrderDetailsColumnsNamesWithPrefix.QtyShipped,
			OrderDetailsColumnsNamesWithPrefix.Quantity,
			//OrderDetailsColumnsNamesWithPrefix.Returned,
			OrderDetailsColumnsNamesWithPrefix.Returned_Date,
			//OrderDetailsColumnsNamesWithPrefix.Reward_Points_Given_For_Purchase,
			OrderDetailsColumnsNamesWithPrefix.ShipDate,
			OrderDetailsColumnsNamesWithPrefix.Shipped,
			OrderDetailsColumnsNamesWithPrefix.Ships_By_Itself,
			//OrderDetailsColumnsNamesWithPrefix.TaxableProduct,
			OrderDetailsColumnsNamesWithPrefix.TotalPrice,
			OrderDetailsColumnsNamesWithPrefix.Vendor_Price,
			//OrderDetailsColumnsNamesWithPrefix.Warehouses
		};
	}
}
namespace VolusionAccess.Models.Command
{
	public class OrderColumns
	{
		public static readonly OrderColumns Unknown = new OrderColumns( string.Empty );
		public static readonly OrderColumns OrderId = new OrderColumns( "o.OrderID" );
		public static readonly OrderColumns OrderDate = new OrderColumns( "o.OrderDate" );
		public static readonly OrderColumns OrderDateUtc = new OrderColumns( "o.OrderDateUtc" );
		public static readonly OrderColumns IsAGift = new OrderColumns( "o.IsAGift" );
		public static readonly OrderColumns OrderStatus = new OrderColumns( "o.OrderStatus" );

		private OrderColumns( string name )
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}
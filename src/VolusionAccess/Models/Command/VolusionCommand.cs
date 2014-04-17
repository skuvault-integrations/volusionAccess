namespace VolusionAccess.Models.Command
{
	internal class VolusionCommand
	{
		public static readonly VolusionCommand Unknown = new VolusionCommand( string.Empty );
		public static readonly VolusionCommand GetProducts = new VolusionCommand( @"Generic\Products" );
		public static readonly VolusionCommand GetOrders = new VolusionCommand( @"Generic\Orders" );

		private VolusionCommand( string command )
		{
			this.Command = command;
		}

		public string Command { get; private set; }
	}
}
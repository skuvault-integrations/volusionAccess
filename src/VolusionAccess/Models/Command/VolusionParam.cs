﻿namespace VolusionAccess.Models.Command
{
	internal class VolusionParam
	{
		public static readonly VolusionParam Unknown = new VolusionParam( string.Empty );
		public static readonly VolusionParam Login = new VolusionParam( "Login" );
		public static readonly VolusionParam EncryptedPassword = new VolusionParam( "EncryptedPassword" );
		public static readonly VolusionParam ApiName = new VolusionParam( "EDI_Name" );
		public static readonly VolusionParam SelectColumns = new VolusionParam( "SELECT_Columns" );

		private VolusionParam( string name )
		{
			this.Name = name;
		}

		public string Name { get; private set; }
	}
}
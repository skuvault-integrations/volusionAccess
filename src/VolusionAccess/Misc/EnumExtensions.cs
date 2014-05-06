using System;

namespace VolusionAccess.Misc
{
	public static class EnumExtensions
	{
		public static T ToEnum< T >( this string value )
		{
			value = value.Replace( " ", "" );
			return ( T )Enum.Parse( typeof( T ), value, true );
		}

		public static T ToEnum< T >( this string value, T defaultValue )
		{
			if( string.IsNullOrWhiteSpace( value ) )
				return defaultValue;

			value = value.Replace( " ", "" );
			return ( T )Enum.Parse( typeof( T ), value, true );
		}
	}
}
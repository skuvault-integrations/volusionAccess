using CuttingEdge.Conditions;

namespace VolusionAccess.Models.Configuration
{
	public sealed class VolusionConfig
	{
		public string Host { get; private set; }
		public string ShopName { get; private set; }
		public string UserName { get; private set; }
		public string Password { get; private set; }
		public int DefaultTimeZone { get; private set; }

		public VolusionConfig( string shopName, string userName, string password, int defaultTimeZone = -12 )
		{
			Condition.Requires( shopName, "shopName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userName, "userName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( password, "password" ).IsNotNullOrWhiteSpace();
			Condition.Requires( defaultTimeZone, "defaultTimeZone" ).IsInRange( -12, 12 );

			shopName = shopName.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" );
			this.Host = string.Format( "http://{0}/net/WebService.aspx", shopName );
			this.ShopName = shopName;
			this.UserName = userName;
			this.Password = password;
			this.DefaultTimeZone = defaultTimeZone;
		}
	}
}
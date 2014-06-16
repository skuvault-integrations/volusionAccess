using CuttingEdge.Conditions;

namespace VolusionAccess.Models.Configuration
{
	public sealed class VolusionConfig
	{
		public string Host { get; private set; }
		public string ShopName { get; private set; }
		public string UserName { get; private set; }
		public string Password { get; private set; }

		public VolusionConfig( string shopName, string userName, string password )
		{
			Condition.Requires( shopName, "shopName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userName, "userName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( password, "password" ).IsNotNullOrWhiteSpace();

			shopName = shopName.ToLower().TrimEnd( '\\', '/' ).Replace( "https://", "" ).Replace( "http://", "" );
			this.Host = string.Format( "http://{0}/net/WebService.aspx", shopName );
			this.ShopName = shopName;
			this.UserName = userName;
			this.Password = password;
		}
	}
}
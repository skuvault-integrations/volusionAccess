using CuttingEdge.Conditions;
using VolusionAccess.Models.Command;

namespace VolusionAccess.Models.Configuration
{
	public sealed class VolusionConfig
	{
		public string Host { get; private set; }
		public string HostWithAuth { get; private set; }
		public string ShopName { get; private set; }
		public string UserName { get; private set; }
		public string Password { get; private set; }

		public VolusionConfig( string shopName, string userName, string password )
		{
			Condition.Requires( shopName, "shopName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userName, "userName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( password, "password" ).IsNotNullOrWhiteSpace();

			this.Host = string.Format( "{0}/net/WebService.aspx", shopName );
			this.HostWithAuth = string.Format( "{0}?{1}={2}&{3}={4}", this.Host, VolusionParam.Login, userName, VolusionParam.EncryptedPassword, password );
			this.ShopName = shopName;
			this.UserName = userName;
			this.Password = password;
		}
	}
}
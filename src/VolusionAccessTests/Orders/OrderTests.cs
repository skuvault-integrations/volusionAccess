using System.Linq;
using LINQtoCSV;
using NUnit.Framework;
using VolusionAccess;
using VolusionAccess.Models.Configuration;

namespace VolusionAccessTests.Orders
{
	public class OrderTests
	{
		private readonly IVolusionFactory BigCommerceFactory = new VolusionFactory();
		private VolusionConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\VolusionCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new VolusionConfig( testConfig.ShopName, testConfig.UserName, testConfig.Password );
		}
	}
}
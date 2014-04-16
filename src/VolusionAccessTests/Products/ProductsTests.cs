using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using VolusionAccess;
using VolusionAccess.Models.Configuration;

namespace VolusionAccessTests.Products
{
	[ TestFixture ]
	public class ProductsTests
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

		[ Test ]
		public void GetProducts()
		{
			var service = this.BigCommerceFactory.CreateProductsService( this.Config );
			var products = service.GetProducts();

			products.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this.BigCommerceFactory.CreateProductsService( this.Config );
			var products = await service.GetProductsAsync();

			products.Count().Should().BeGreaterThan( 0 );
		}
	}
}
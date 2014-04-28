using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using VolusionAccess;
using VolusionAccess.Models.Configuration;

namespace VolusionAccessTests.Orders
{
	public class OrderTests
	{
		private readonly IVolusionFactory VolusionFactory = new VolusionFactory();
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
		public void GetOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetOrders();

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetOrdersAsync();

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDate()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetOrders( DateTime.UtcNow.AddDays( -200 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetOrdersAsync( DateTime.UtcNow.AddDays( -200 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetFilteredOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetFilteredOrders( "IsAGift", "N" );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetFilteredOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetFilteredOrdersAsync( "IsAGift", "N" );

			orders.Count().Should().BeGreaterThan( 0 );
		}
	}
}
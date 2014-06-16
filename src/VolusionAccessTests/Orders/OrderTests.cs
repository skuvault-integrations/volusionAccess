using System;
using System.Collections.Generic;
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
		public void GetOrder()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var order = service.GetOrder( 1 );

			order.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetOrderAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var order = await service.GetOrderAsync( 1 );

			order.Should().NotBeNull();
		}

		[ Test ]
		public void GetNewOrUpdatedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNewOrUpdatedOrders();

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrUpdatedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNewOrUpdatedOrdersAsync();

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNewOrUpdatedOrdersByDate()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNewOrUpdatedOrders( DateTime.UtcNow.AddDays( -10 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNewOrUpdatedOrdersByDateAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNewOrUpdatedOrdersAsync( DateTime.UtcNow.AddDays( -10 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetNotFinishedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNotFinishedOrders( DateTime.UtcNow.AddDays( -60 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetNotFinishedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNotFinishedOrdersAsync( DateTime.UtcNow.AddDays( -60 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetFinishedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetFinishedOrders( new List< int > { 52, 55 } );

			orders.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetFinishedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetFinishedOrdersAsync( new List< int > { 52, 55 } );

			orders.Count().Should().BeGreaterThan( 0 );
		}
	}
}
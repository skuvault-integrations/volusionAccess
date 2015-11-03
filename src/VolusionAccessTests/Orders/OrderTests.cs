using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using Netco.Logging;
using NUnit.Framework;
using VolusionAccess;
using VolusionAccess.Models.Configuration;

namespace VolusionAccessTests.Orders
{
	public class OrderTests
	{
		private readonly IVolusionFactory VolusionFactory = new VolusionFactory();
		private VolusionConfig Config;
		private int TimeZone = -7;

		[ SetUp ]
		public void Init()
		{
			NetcoLogger.LoggerFactory = new ConsoleLoggerFactory();
			const string credentialsFilePath = @"..\..\Files\VolusionCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new VolusionConfig( testConfig.ShopName, testConfig.UserName, testConfig.Password, TimeZone );
		}

		[ Test ]
		public void GetOrder()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var order = service.GetOrder( 34109 );

			order.Should().NotBeNull();
			order.DefaultTimeZone.Should().Be( TimeZone );
		}

		[ Test ]
		public async Task GetOrderAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var order = await service.GetOrderAsync( 1 );

			order.Should().NotBeNull();
			order.DefaultTimeZone.Should().Be( TimeZone );
		}

		[ Test ]
		public void GetNewOrUpdatedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNewOrUpdatedOrders();

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public async Task GetNewOrUpdatedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNewOrUpdatedOrdersAsync();

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public void GetNewOrUpdatedOrdersByDate()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNewOrUpdatedOrders( DateTime.UtcNow.AddDays( -10 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public async Task GetNewOrUpdatedOrdersByDateAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNewOrUpdatedOrdersAsync( DateTime.UtcNow.AddDays( -10 ), DateTime.UtcNow );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public void GetNotFinishedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetNotFinishedOrders( new DateTime( 2015, 10, 30, 12, 30, 0 ), new DateTime( 2015, 11, 3, 13, 30, 0 ) );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public async Task GetNotFinishedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetNotFinishedOrdersAsync( DateTime.Now.AddDays( -20 ), DateTime.Now );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public void GetFinishedOrders()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = service.GetFinishedOrders( new List< int > { 71423, 55 } );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}

		[ Test ]
		public async Task GetFinishedOrdersAsync()
		{
			var service = this.VolusionFactory.CreateOrdersService( this.Config );
			var orders = await service.GetFinishedOrdersAsync( new List< int > { 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 777 } );

			orders.Count().Should().BeGreaterThan( 0 );
			foreach( var order in orders )
			{
				order.DefaultTimeZone.Should().Be( TimeZone );
			}
		}
	}
}
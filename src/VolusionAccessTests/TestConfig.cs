using LINQtoCSV;

namespace VolusionAccessTests
{
	internal class TestConfig
	{
		[ CsvColumn( Name = "ShopName", FieldIndex = 1 ) ]
		public string ShopName { get; set; }

		[ CsvColumn( Name = "UserName", FieldIndex = 2 ) ]
		public string UserName { get; set; }

		[ CsvColumn( Name = "Password", FieldIndex = 3 ) ]
		public string Password { get; set; }
	}
}
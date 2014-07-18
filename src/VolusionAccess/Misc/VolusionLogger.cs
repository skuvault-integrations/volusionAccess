using Netco.Logging;

namespace VolusionAccess.Misc
{
	public static class VolusionLogger
	{
		public static ILogger Log{ get; private set; }

		static VolusionLogger()
		{
			Log = NetcoLogger.GetLogger( "VolusionLogger" );
		}
	}
}
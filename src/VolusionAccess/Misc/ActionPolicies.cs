using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace VolusionAccess.Misc
{
	public static class ActionPolicies
	{
#if DEBUG
		private const int RetryCount = 1;
#else
		private const int RetryCount = 10;
#endif
		public static ActionPolicy Submit
		{
			get { return _volusionSumbitPolicy; }
		}

		private static readonly ActionPolicy _volusionSumbitPolicy = ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			VolusionLogger.Log.Trace( ex, "Retrying Volusion API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _volusionSumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionSumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			VolusionLogger.Log.Trace( ex, "Retrying Volusion API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _volusionGetPolicy; }
		}

		private static readonly ActionPolicy _volusionGetPolicy = ActionPolicy.Handle< Exception >().Retry( RetryCount, ( ex, i ) =>
		{
			VolusionLogger.Log.Trace( ex, "Retrying Volusion API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _volusionGetAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionGetAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( RetryCount, async ( ex, i ) =>
		{
			VolusionLogger.Log.Trace( ex, "Retrying Volusion API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}

using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Logging;
using Netco.Utils;

namespace VolusionAccess.Misc
{
	public static class ActionPolicies
	{
		public static ActionPolicy Submit
		{
			get { return _volusionSumbitPolicy; }
		}

		private static readonly ActionPolicy _volusionSumbitPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			typeof( ActionPolicies ).Log().Trace( ex, "Retrying Volusion API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _volusionSumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionSumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			typeof( ActionPolicies ).Log().Trace( ex, "Retrying Volusion API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _volusionGetPolicy; }
		}

		private static readonly ActionPolicy _volusionGetPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			typeof( ActionPolicies ).Log().Trace( ex, "Retrying Volusion API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _volusionGetAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionGetAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			typeof( ActionPolicies ).Log().Trace( ex, "Retrying Volusion API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}

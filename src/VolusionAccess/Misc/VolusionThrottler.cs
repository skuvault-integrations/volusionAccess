using Netco.ThrottlerServices;

namespace VolusionAccess.Misc
{
    public class VolusionThrottler : Throttler
    {
        /// <summary>
        /// Throttler constructor. See code section for details
        /// </summary>
        /// <code>
        /// // Maximum request quota: 20 requests. Restore rate: Five items every second
        /// var throttler = new Throttler( 20, 1, 5 )
        /// 
        /// // Maximum request quota of six and a restore rate of one request every minute
        /// var throttler = new Throttler( 6, 60, 1 )
        /// </code>
        /// <param name="maxQuota">Max quota</param>
        /// <param name="delayInSecondsBeforeRelease">Delay in seconds before release</param>
        /// <param name="itemsCountForRelease">Items count for release. Default is 1</param>
        /// <param name="maxRetryCount">Max Retry Count</param>
        public VolusionThrottler( int maxQuota = 1, int delayInSecondsBeforeRelease = 1, int itemsCountForRelease = 1, int maxRetryCount = 10 ):
            base( maxQuota, delayInSecondsBeforeRelease, itemsCountForRelease, maxRetryCount, "" )
        {
        }
    }
    
    public class VolusionThrottlerAsync: ThrottlerAsync
    {
        /// <summary>
        /// Throttler constructor. See code section for details
        /// </summary>
        /// <code>
        /// // Maximum request quota: 20 requests. Restore rate: Five items every second
        /// var throttler = new Throttler( 20, 1, 5 )
        /// 
        /// // Maximum request quota of six and a restore rate of one request every minute
        /// var throttler = new Throttler( 6, 60, 1 )
        /// </code>
        /// <param name="maxQuota">Max quota</param>
        /// <param name="delayInSecondsBeforeRelease">Delay in seconds before release</param>
        /// <param name="itemsCountForRelease">Items count for release. Default is 1</param>
        /// <param name="maxRetryCount">Max Retry Count</param>
        public VolusionThrottlerAsync( int maxQuota = 1, int delayInSecondsBeforeRelease = 1, int itemsCountForRelease = 1, int maxRetryCount = 10 ):
            base( maxQuota, delayInSecondsBeforeRelease, itemsCountForRelease, maxRetryCount, "" )
        {
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Methods
{
	public partial class LimitChecker
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly List<ClientCallType> ClientCalls = new List<ClientCallType>();

		/// <summary>
		/// 
		/// </summary>
		private bool StopChecker = false;

		/// <summary>
		/// The thread checker
		/// </summary>
		private readonly Thread ThreadChecker = null;

		/// <summary>
		/// Maximum life of client in seconds.
		/// </summary>
		public int MaxLiveClient { get; set; } = 3600;

		/// <summary>
		/// Maximum requests per timespan.
		/// </summary>
		public int MaxRequests { get; set; } = 2;

		/// <summary>
		/// Minimum time between calls in milliseconds.
		/// I put 10 minutes between calls for debug purposes.
		/// </summary>
		public int MinTimeLastCall { get; set; } = 1000 * 60 * 10;

		/// <summary>
		/// 
		/// </summary>
		public RuleTypes RuleTypes { get; set; } = RuleTypes.RequestsPerTimespan;

		/// <summary>
		/// Use 1-hour for debug purposes as the default.
		/// </summary>
		public int SpanRequestsSeconds { get; set; } = 60 * 60 * 60;
	}
}

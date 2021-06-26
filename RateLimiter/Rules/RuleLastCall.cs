using System;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Rules
{
	/// <summary>
	/// 
	/// </summary>
	public class RuleLastCall : RuleBase
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public int MinTime { get; set; } = 0;
		#endregion Properties

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		private RuleLastCall()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="minTime"></param>
		public RuleLastCall(int minTime)
		{
			// Initialize the properties.
			this.MinTime = minTime;
		}
		#endregion Constructors

		#region Virtual Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public override RuleResult CheckForDot(ClientCallType client)
		{
			try
			{
				// Check for the easy case.
				if (true == client.LastCall.Equals(DateTime.MinValue))
					return RuleResult.Compliant;

				// Compute the timespan since the last call.
				TimeSpan span = DateTime.Now.Subtract(client.LastCall);

				// Failout if less then the required time.
				if (span.TotalMilliseconds < this.MinTime)
					return RuleResult.Exceeded;

				return RuleResult.Compliant;
			}

			catch (Exception)
			{
			}

			return RuleResult.Error;
		}
		#endregion Virtual Methods

		#region Methods
		#endregion Methods
	}
}

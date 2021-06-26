using System;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Rules
{
	/// <summary>
	/// 
	/// </summary>
	public class RuleRequests : RuleBase
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public int Timespan { get; set; } = 0;

		/// <summary>
		/// 
		/// </summary>
		public int MaxRequests { get; set; } = 0;
		#endregion Properties

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		private RuleRequests()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="timespan"></param>
		/// <param name="maxrequests"></param>
		public RuleRequests(int timespan, int maxrequests)
		{
			// Set member variables.
			this.Timespan = timespan;
			this.MaxRequests = maxrequests;
		}
		#endregion Constructors

		#region Required Virtual Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public override RuleResult CheckForDot(ClientCallType client)
		{
			RuleResult result = RuleResult.Compliant;
			try
			{
				// Get the oldest allowed date.
				TimeSpan spanOldest = new TimeSpan(0, 0, this.Timespan);
				DateTime oldest = DateTime.Now.Subtract(spanOldest);

				// Age out old ones.
				for (int i = client.Requests.Count - 1; 0 <= i; i--)
				{
					if (0 > client.Requests[i].CompareTo(oldest))
						client.Requests.RemoveAt(i);
				}

				// Excessive?
				if (client.Requests.Count > this.MaxRequests)
					result = RuleResult.Exceeded;
			}

			catch (Exception)
			{
				// Should never come here.
				result = RuleResult.Error;
			}

			return result;
		}
		#endregion Required Virtual Methods

		#region Private Methods
		#endregion Private Methods
	}
}

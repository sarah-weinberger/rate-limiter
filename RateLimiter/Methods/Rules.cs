using System;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Methods
{
	/// <summary>
	/// 
	/// </summary>
	public class Rules
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="timespan">Timespan in seconds</param>
		/// <param name="client"></param>
		/// <param name="maxrequets"></param>
		/// <returns></returns>
		public static RuleResult RequestsPerTimespan(int timespan, int maxrequets, ClientCallType client)
		{
			RuleResult result = RuleResult.Compliant;
			try
			{
				// Get the oldest allowed date.
				TimeSpan spanOldest = new TimeSpan(0, 0, timespan);
				DateTime oldest = DateTime.Now.Subtract(spanOldest);

				// Age out old ones.
				for (int i = client.Requests.Count - 1; 0 <= i; i--)
				{
					if (0 > client.Requests[i].CompareTo(oldest))
						client.Requests.RemoveAt(i);
				}

				// Excessive?
				if (client.Requests.Count > maxrequets)
					result = RuleResult.Exceeded;
			}

			catch (Exception)
			{
				// Should never come here.
				result = RuleResult.Error;
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="minTime"></param>
		/// <param name="client"></param>
		/// <returns></returns>
		public static RuleResult TimespanSinceLastCall(int minTime, ClientCallType client)
		{
			try
			{
				// Check for the easy case.
				if (true == client.LastCall.Equals(DateTime.MinValue))
					return RuleResult.Compliant;

				// Compute the timespan since the last call.
				TimeSpan span = DateTime.Now.Subtract(client.LastCall);

				// Failout if less then the required time.
				if (span.TotalMilliseconds < minTime)
					return RuleResult.Exceeded;

				return RuleResult.Compliant;
			}

			catch (Exception)
			{
			}

			return RuleResult.Error;
		}
	}
}

using System;
using System.Threading;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Methods
{
	/// <summary>
	/// Create one instance of this Limit Checker class for each resource.
	/// </summary>
	public partial class LimitChecker : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		public LimitChecker()
		{
			// Create the thread checking each API request for DOS attacks.
			this.StopChecker = false;
			ThreadStart threadStart = new ThreadStart(this.ThreadTaskChecker);
			this.ThreadChecker = new Thread(threadStart);
			this.ThreadChecker.Start();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientCall"></param>
		/// <returns></returns>
		public RuleResult CheckRate(ClientCallType clientCall)
		{
			// Check Rule: Requests Per Timespan
			if (this.RuleTypes.HasFlag(RuleTypes.RequestsPerTimespan))
			{
				RuleResult result = Rules.RequestsPerTimespan(this.SpanRequestsSeconds, this.MaxRequests, clientCall);
				if (RuleResult.Compliant != result)
					return result;
			}

			// Check Rule: Timespan Since Last Call
			if (this.RuleTypes.HasFlag(RuleTypes.TimespanSinceLastCall))
			{
				RuleResult result = Rules.TimespanSinceLastCall(this.MinTimeLastCall, clientCall);
				if (RuleResult.Compliant != result)
					return result;
			}

			return RuleResult.Compliant;
		}

		/// <summary>
		/// 
		/// </summary>
		public void ThreadTaskChecker()
		{
			// Keep looping
			do
			{
				// Check each client for age. Need to implement atomic check, as main thread uses the data structure.
				//...
			} while (false == this.StopChecker);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientToken"></param>
		/// <param name="record"></param>
		/// <returns></returns>
		public bool LocateClientRecord(object clientToken, out ClientCallType record)
		{
			record = null;
			try
			{
				// Cycle through each record to find the token and if not there add it.
				foreach (var item in this.ClientCalls)
				{
					if ((long)item.ClientToken == (long)clientToken)
					{
						record = item;
						record.Requests.Add(DateTime.Now);
						record.Requests.Sort((x, y) => y.CompareTo(x));
						if (1 < record.Requests.Count)
							record.LastCall = record.Requests[1];
						return true;
					}
				}

				// Coming here means a new record.
				record = new ClientCallType()
				{
					ClientToken = clientToken,
				};
				record.Requests.Add(DateTime.Now);
				record.LastCall = DateTime.MinValue;
				this.ClientCalls.Add(record);

				return true;
			}

			catch (Exception)
			{
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			// Stop the thread.
			this.StopChecker = true;
		}
	}
}

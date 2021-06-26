using System;
using System.Collections.Generic;
using RateLimiter.Enums;
using RateLimiter.Types;
using RateLimiter.Methods;

namespace RateLimiter.ObjResources
{
	/// <summary>
	/// 
	/// </summary>
	public class Resource1
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly LimitChecker LimitChecker = new LimitChecker();

		/// <summary>
		/// 
		/// </summary>
		public Resource1()
		{
			// Pass on the rules type to the rate limiter.
			this.LimitChecker.RuleTypes = RuleTypes.RequestsPerTimespan;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ruleType"></param>
		public Resource1(RuleTypes ruleType)
		{
			// Pass on the rules type to the rate limiter.
			this.LimitChecker.RuleTypes = ruleType;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ruleType"></param>
		/// <param name="maxLiveClient"></param>
		/// <param name="maxRequests"></param>
		/// <param name="minTimeLastCall"></param>
		/// <param name="spanRequestsSeconds"></param>
		public Resource1(RuleTypes ruleType, int maxLiveClient, int maxRequests, int minTimeLastCall, int spanRequestsSeconds)
		{
			// Initialize the limit checker.
			this.LimitChecker.MaxLiveClient = maxLiveClient;
			this.LimitChecker.MaxRequests = maxRequests;
			this.LimitChecker.MinTimeLastCall = minTimeLastCall;
			this.LimitChecker.RuleTypes = ruleType;
			this.LimitChecker.SpanRequestsSeconds = spanRequestsSeconds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientToken"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public ApiResult Api1(object clientToken, out string text)
		{
			// Locate the client record.
			this.LimitChecker.LocateClientRecord(clientToken, out ClientCallType clientCall);

			// Do rate limit check.
			RuleResult result = this.LimitChecker.CheckRate(clientCall);
			if (RuleResult.Compliant != result)
			{
				text = string.Empty;
				return ApiResult.Dot;
			}

			// Method result.
			text = @"Result of Method1";

			return ApiResult.Success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientToken"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public ApiResult Api2(object clientToken, out string text)
		{
			// Locate the client record.
			this.LimitChecker.LocateClientRecord(clientToken, out ClientCallType clientCall);

			// Do rate limit check.
			RuleResult result = this.LimitChecker.CheckRate(clientCall);
			if (RuleResult.Compliant != result)
			{
				text = string.Empty;
				return ApiResult.Dot;
			}

			// Method result.
			text = @"Result of Method2";

			return ApiResult.Success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientToken"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public ApiResult DateOrderingCheck(object clientToken, out string text)
		{
			// Locate the client record.
			this.LimitChecker.LocateClientRecord(clientToken, out ClientCallType clientCall);

			// 
			List<DateTime> dates = new List<DateTime>
			{
				new DateTime(2020, 06, 25, 13, 30, 30),
				new DateTime(2021, 06, 25, 13, 30, 30),
				new DateTime(2021, 05, 25, 13, 30, 30)
			};
			dates.Sort((x, y) => y.CompareTo(x));
			text = $"Date ordering is: 0={dates[0]}, 1={dates[1]}, 2={dates[2]}";

			return ApiResult.Success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientToken"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public ApiResult DateCompare(object clientToken, out string text)
		{
			// Locate the client record.
			this.LimitChecker.LocateClientRecord(clientToken, out ClientCallType clientCall);

			// 
			List<DateTime> dates = new List<DateTime>
			{
				new DateTime(2020, 06, 25, 13, 30, 30),
				new DateTime(2021, 06, 25, 13, 30, 30),
				new DateTime(2021, 05, 25, 13, 30, 30)
			};
			dates.Sort((x, y) => y.CompareTo(x));

			// 
			DateTime oldest = new DateTime(2021, 1, 1, 0, 0, 0);
			if (0 > dates[2].CompareTo(oldest))
				dates.RemoveAt(2);
			if (2 < dates.Count)
				text = $"Date ordering is: 0={dates[0]}, 1={dates[1]}, 2={dates[2]}";
			else
				text = $"Date ordering is: 0={dates[0]}, 1={dates[1]}";

			return ApiResult.Success;
		}
	}
}

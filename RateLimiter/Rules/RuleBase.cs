using System;
using RateLimiter.Enums;
using RateLimiter.Types;

namespace RateLimiter.Rules
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class RuleBase
	{
		#region Required Virtual Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public abstract RuleResult CheckForDot(ClientCallType client);
		#endregion Required Virtual Methods
	}
}

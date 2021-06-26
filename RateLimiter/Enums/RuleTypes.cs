using System;

namespace RateLimiter.Enums
{
	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum RuleTypes : uint
	{
		/// <summary>
		/// 
		/// </summary>
		RequestsPerTimespan,

		/// <summary>
		/// 
		/// </summary>
		TimespanSinceLastCall,
	}
}

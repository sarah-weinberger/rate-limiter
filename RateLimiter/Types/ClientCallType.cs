using System;
using System.Collections.Generic;

namespace RateLimiter.Types
{
	/// <summary>
	/// 
	/// </summary>
	public class ClientCallType
	{
		/// <summary>
		/// 
		/// </summary>
		public object ClientToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public readonly List<DateTime> Requests = new List<DateTime>();

		/// <summary>
		/// 
		/// </summary>
		public DateTime LastCall { get; set; } = DateTime.MinValue;

		/// <summary>
		/// 
		/// </summary>
		public ClientCallType()
		{
		}
	}
}

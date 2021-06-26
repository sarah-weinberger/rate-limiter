using System;
using System.Security.Principal;
using NUnit.Framework;
using RateLimiter.Enums;

namespace RateLimiter.Tests
{
    [TestFixture]
    public class RateLimiterTest
    {
        /// <summary>
        /// Defines the test method Example.
        /// </summary>
        [Test]
        public void Example()
        {
            Console.Write("Example ran!");
            Assert.IsTrue(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleRequestsSucceed()
        {
            // Create a new resource.
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(RuleTypes.RequestsPerTimespan);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.Api1(accountToken, out string text);
            Console.WriteLine($"API Result is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleRequestsFail()
		{
            // Create a new resource.
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(RuleTypes.RequestsPerTimespan);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.Api1(accountToken, out string text);
            Console.WriteLine($"API Result for call 1 is {result} with text response: {text}");

            // Call the method.
            result = resource1.Api1(accountToken, out text);
            Console.WriteLine($"API Result for call 2 is {result} with text response: {text}");

            // Call the method.
            result = resource1.Api1(accountToken, out text);
            Console.WriteLine($"API Result for call 3 is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleLastCallSucceed()
		{
            // Create a new resource.
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(RuleTypes.TimespanSinceLastCall);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.Api1(accountToken, out string text);
            Console.WriteLine($"API Result is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleLastCallFail()
		{
            // Create a new resource.
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(RuleTypes.TimespanSinceLastCall);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.Api1(accountToken, out string text);
            Console.WriteLine($"API Result call 1 is {result} with text response: {text}");

            // Call the method.
            result = resource1.Api1(accountToken, out text);
            Console.WriteLine($"API Result call 2 is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleBothSucceed()
        {
            // Create a new resource.
            RuleTypes rules = RuleTypes.RequestsPerTimespan | RuleTypes.RequestsPerTimespan;
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(rules);

            // Call the method.
            IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
            ApiResult result = resource1.Api2(accountToken, out string text);
            Console.WriteLine($"API Result is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestRuleBothFail()
        {
            // Create a new resource.
            RuleTypes rules = RuleTypes.RequestsPerTimespan | RuleTypes.TimespanSinceLastCall;
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(rules);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.Api2(accountToken, out string text);
            Console.WriteLine($"API Result call 1 is {result} with text response: {text}");

            // Call the method.
            result = resource1.Api2(accountToken, out text);
            Console.WriteLine($"API Result call 2 is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestDateOrdering()
        {
            // Create a new resource.
            RuleTypes rules = RuleTypes.RequestsPerTimespan;
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(rules);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.DateOrderingCheck(accountToken, out string text);
            Console.WriteLine($"API Result is {result} with text response: {text}");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestDateCompare()
        {
            // Create a new resource.
            RuleTypes rules = RuleTypes.RequestsPerTimespan;
            RateLimiter.ObjResources.Resource1 resource1 = new ObjResources.Resource1(rules);

            // Call the method.
            long accountToken = 5;
            ApiResult result = resource1.DateCompare(accountToken, out string text);
            Console.WriteLine($"API Result is {result} with text response: {text}");
        }
    }
}

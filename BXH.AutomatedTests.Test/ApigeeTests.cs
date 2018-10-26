using System;
using System.Net.Mime;
using BXH.AutomatedTests.Api.Apigee;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test
{
    [TestFixture]
    public class ApigeeTests
    {
        readonly ApigeeProxyTests _apigeeTests = new ApigeeProxyTests();

        [Test]
        public void GetTokenReturnsOk()
        {
            var result = _apigeeTests.ApigeeToken();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}

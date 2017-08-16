using Altran.ILS.Data.Interfaces;
using Altran.ILS.Data.Repositories;
using Altran.ILS.Domain.Classes;
using Altran.ILS.Services;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;
using System;
using System.Collections.Generic;

namespace AltranTest.Tests.Repositories
{
    [TestFixture]
    public class PolicyServiceTest
    {
        Policies policies;
        IPolicyRepository policyRepository;
        PolicyService policyService;

        [SetUp]
        public void SetUp()
        {
            policyRepository = MockRepository.GenerateMock<IPolicyRepository>();

            policies = new Policies();
            policies.Results = new List<Policy>();
            ((List<Policy>)policies.Results).Add(new Policy()
            {
                Id = "64cceef9-3a01-49ae-a23b-3761b604800b",
                Email = "britneyblankenship@quotezart.com",
                AmountInsured = new decimal(1825.89),
                InceptionDate = new DateTimeOffset(2016, 6, 1, 3, 33, 32, TimeSpan.Zero),
                InstallmentPayment = true,
                ClientId = "e8fd159b-57c4-4d36-9bd7-a59ca13057bb"
            });
          
            ((List<Policy>)policies.Results).Add(new Policy()
            {
                Id = "7b624ed3-00d5-4c1b-9ab8-c265067ef58b",
                Email = "inesblankenship@quotezart.com",
                AmountInsured = new decimal(399.89),
                InceptionDate = new DateTimeOffset(2015, 7, 6, 6, 55, 49, TimeSpan.Zero),
                InstallmentPayment = true,
                ClientId = "a0ece5db-cd14-4f21-812f-966633e7be86"
            });

             ((List<Policy>)policies.Results).Add(new Policy()
            {
                Id = "56b415d6-53ee-4481-994f-4bffa47b5239",
                Email = "inesblankenship@quotezart.com",
                AmountInsured = new decimal(2301.98),
                InceptionDate = new DateTimeOffset(2014, 12, 1, 5, 53, 13, TimeSpan.Zero),
                InstallmentPayment = false,
                ClientId = "e8fd159b-57c4-4d36-9bd7-a59ca13057bb"
            });

            policyRepository.Stub(x => x.GetPolicies()).Return(policies);
            policyService = new PolicyService(policyRepository);
        }

        [TestCase("56b415d6-53ee-4481-994f-4bffa47b5239", "e8fd159b-57c4-4d36-9bd7-a59ca13057bb")]
        [TestCase("7b624ed3-00d5-4c1b-9ab8-c265067ef58b", "a0ece5db-cd14-4f21-812f-966633e7be86")]
        public void GetClientId_by_policy_number_returns_expectedClientId(string policyNumber, string expectedClientId)
        {
            var result = policyService.GetClientId(policyNumber);
            Assert.AreEqual(result, expectedClientId);
        }

        [TestCase("zzz-number", "not-existant")]
        public void GetClient_by_policy_number_returns_null(string policyNumber, string expectedClientId)
        {
            var result = policyService.GetClientId(policyNumber);
            Assert.IsNull(result); 
        }

        [TestCase("e8fd159b-57c4-4d36-9bd7-a59ca13057bb", 2)]
        [TestCase("a0ece5db-cd14-4f21-812f-966633e7be86", 1)]
        [TestCase("not-existant", 0)]
        public void GetPolicies_by_client_id(string clientId, int expectedCount)
        {
            var result = policyService.GetPolicies(clientId);
            Assert.AreEqual(((List<Policy>)result).Count, expectedCount);
        }
    }
}

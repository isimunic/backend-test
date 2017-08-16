using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Altran.ILS.WebApi;
using Altran.ILS.WebApi.Controllers;
using Altran.ILS.Services;
using Rhino.Mocks;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading;
using System.Security.Principal;
using Altran.ILS.Data.Interfaces;
using System.Web.Http.Hosting;

namespace Altran.ILS.Web.Tests.Controllers
{
    [TestFixture]
    public class PoliciesControllerTest
    {
        IPolicyService policyService;
        IClientService clientService;

        [SetUp]
        public void SetUp()
        {
            IPolicyRepository policyRepository = MockRepository.GenerateMock<IPolicyRepository>();
            IClientRepository clientRepository = MockRepository.GenerateMock<IClientRepository>();
            policyService = new FakePolicyService(policyRepository);
            clientService = new FakeClientService(clientRepository);
        }

        private PoliciesController GetPoliciesController(IPolicyService policyService, IClientService clientService)
        {
            return new PoliciesController(policyService, clientService);
        }

        [TestCase("Britney", "64cceef9-3a01-49ae-a23b-3761b604800b", HttpStatusCode.OK)]
        [TestCase("Britney", "non-existant", HttpStatusCode.NoContent)]
        [TestCase("Merrill", "64cceef9-3a01-49ae-a23b-3761b604800b", HttpStatusCode.Forbidden)]
        [TestCase("Ivana", "non-existant", HttpStatusCode.Forbidden)]
        public void PoliciesController_returns_client_by_policy_number(string expectedUserName, string policyNumber, HttpStatusCode statusExpected)
        {
            // Arrange
            PoliciesController controller = GetPoliciesController(policyService, clientService);
            controller.Request = new HttpRequestMessage();

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(expectedUserName), new string[0]);
            // Act
            HttpResponseMessage result = controller.GetClientByPolicyId(policyNumber);

            // Assert
            Assert.AreEqual(statusExpected, result.StatusCode);
        }

        [TestCase("Britney", "Merrill", HttpStatusCode.OK)]
        [TestCase("Merrill", "Britney", HttpStatusCode.OK)]
        [TestCase("Britney", "non-existant", HttpStatusCode.NoContent)]
        [TestCase("Ivana", "non-existant", HttpStatusCode.Forbidden)]
        public void PoliciesController_returns_policies_by_username(string expectedUserName, string userName, HttpStatusCode statusExpected)
        {
            // Arrange
            PoliciesController controller = GetPoliciesController(policyService, clientService);
            controller.Request = new HttpRequestMessage();

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(expectedUserName), new string[0]);
            // Act
            HttpResponseMessage result = controller.GetPoliciesByUsername(userName);

            // Assert
            Assert.AreEqual(statusExpected, result.StatusCode);
        }

    }
}

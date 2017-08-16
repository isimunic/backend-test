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
    public class ClientsControllerTest
    {
        IClientService clientService;

        [SetUp]
        public void SetUp()
        {
            IClientRepository clientRepository = MockRepository.GenerateMock<IClientRepository>();
            clientService = new FakeClientService(clientRepository);
        }

        private ClientsController GetClientsController(IClientService clientService)
        {
            return new ClientsController(clientService);
        }

        [TestCase("Britney", "a0ece5db-cd14-4f21-812f-966633e7be86", HttpStatusCode.OK)]
        [TestCase("Britney", "non-existant", HttpStatusCode.NoContent)]
        [TestCase("Ivana", "non-existant", HttpStatusCode.Forbidden)]
        public void ClientsController_returns_client_by_id(string expectedUserName, string clientId, HttpStatusCode statusExpected)
        {
            // Arrange
            ClientsController controller = GetClientsController(clientService);
            controller.Request = new HttpRequestMessage();

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(expectedUserName), new string[0]);
            // Act
            HttpResponseMessage result = controller.GetById(clientId);

            // Assert
            Assert.AreEqual(statusExpected, result.StatusCode);
        }

        [TestCase("Britney", "Merrill", HttpStatusCode.OK)]
        [TestCase("Britney", "non-existant", HttpStatusCode.NoContent)]
        [TestCase("Ivana", "non-existant", HttpStatusCode.Forbidden)]
        public void ClientsController_returns_client_by_username(string expectedUserName, string userName, HttpStatusCode statusExpected)
        {
            // Arrange
            ClientsController controller = GetClientsController(clientService);
            controller.Request = new HttpRequestMessage();

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(expectedUserName), new string[0]);
            // Act
            HttpResponseMessage result = controller.GetByUsername(userName);

            // Assert
            Assert.AreEqual(statusExpected, result.StatusCode);
        }
    }
}

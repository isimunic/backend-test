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
    public class ClientServiceTest
    {
        Clients clients;
        IClientRepository clientRepository;
        ClientService clientService;

        [SetUp]
        public void SetUp()
        {
            clientRepository = MockRepository.GenerateMock<IClientRepository>();

            clients = new Clients();
            clients.Results = new List<Client>();
            ((List<Client>)clients.Results).Add(new Client()
            {
                Id = "a0ece5db-cd14-4f21-812f-966633e7be86",
                Email = "britneyblankenship@quotezart.com",
                Name = "Britney",
                Role = "admin"
            });

            ((List<Client>)clients.Results).Add(new Client()
            {
                Id = "44e44268-dce8-4902-b662-1b34d2c10b8e",
                Email = "merrillblankenship@quotezart.com",
                Name = "Merrill",
                Role = "user"
            }); 

            clientRepository.Stub( x => x.GetClients()).Return(clients);
            clientService = new ClientService(clientRepository);
        }

        [TestCase("Britney", "a0ece5db-cd14-4f21-812f-966633e7be86")]
        [TestCase("Merrill", "44e44268-dce8-4902-b662-1b34d2c10b8e")]
        public void GetClient_by_username_returns_expectedClientId(string username, string expectedClientId)
        {
            var result = clientService.GetClient(username);
            Assert.AreEqual(result.Id, expectedClientId);
        }

        [TestCase("Ivana", "non-existant")]
        public void GetClient_by_username_returns_null(string username, string expectedClientId)
        {
            var result = clientService.GetClient(username);
            Assert.IsNull(result); 
        }

        [TestCase("Britney", true)]
        [TestCase("Merrill", false)]
        public void IsClientAdmin_by_username(string username, bool expected)
        {
            var result = clientService.IsClientAdmin(username);
            Assert.AreEqual(result, expected);
        }

        [TestCase("Britney", false)]
        [TestCase("Merrill", true)]
        public void IsClientUser_by_username(string username, bool expected)
        {
            var result = clientService.IsClientUser(username);
            Assert.AreEqual(result, expected);
        }

        [TestCase("Britney", "admin")]
        [TestCase("Merrill", "user")]
        public void GetClientRole_by_username_returns_expectedRole(string username, string expectedRole)
        {
            var result = clientService.GetClientRole(username);
            Assert.AreEqual(result, expectedRole);
        }

        [TestCase("Ivana", "non-existant")]
        public void GetClientRole_by_username_returns_null(string username, string expectedRole)
        {
            var result = clientService.GetClientRole(username);
            Assert.IsNull(result);
        }
    }
}

using Altran.ILS.Data.Interfaces;
using Altran.ILS.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altran.ILS.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public Client GetClient(string username)
        {
            return this.clientRepository.GetClients().Results.Where(x => x.Name == username).Select(x => x).FirstOrDefault();
        }

        public Client GetClientById(string clientId)
        {
            return this.clientRepository.GetClients().Results.Where(x => x.Id == clientId).Select(x => x).FirstOrDefault();
        }

        public string GetClientRole(string username)
        {
            return this.clientRepository.GetClients().Results.Where(x => x.Name == username).Select(x => x.Role).FirstOrDefault();
        }

        public bool IsClientAdmin(string username)
        {
            var role = this.clientRepository.GetClients().Results.Where(x => x.Name == username).Select(x => x.Role).FirstOrDefault();
            return (role == "admin");
        }

        public bool IsClientUser(string username)
        {
            var role = this.clientRepository.GetClients().Results.Where(x => x.Name == username).Select(x => x.Role).FirstOrDefault();
            return (role == "user");
        }
    }
}

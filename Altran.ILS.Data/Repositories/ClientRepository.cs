using Altran.ILS.Data.Interfaces;
using Altran.ILS.Domain.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Altran.ILS.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private Clients clients;

        public ClientRepository()
        {
            var httpClient = new HttpClient();
            var task = httpClient.GetAsync("http://www.mocky.io/v2/5808862710000087232b75ac").ContinueWith((t) =>
            {
                var response = t.Result;
                var readTask = response.Content.ReadAsAsync<Clients>();
                readTask.Wait();
                this.clients = readTask.Result;
            });

            task.Wait();
        }

        public Clients GetClients()
        {
            return this.clients;
        }
    }
}

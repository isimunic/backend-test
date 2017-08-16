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
    public class PolicyRepository : IPolicyRepository
    {
        private Policies policies;

        public PolicyRepository()
        {
            var httpClient = new HttpClient();
            var task = httpClient.GetAsync("http://www.mocky.io/v2/580891a4100000e8242b75c5").ContinueWith((t) =>
            {
                var response = t.Result;
                var readTask = response.Content.ReadAsAsync<Policies>();
                readTask.Wait();
                this.policies = readTask.Result;
            });

            task.Wait();
        }

        public Policies GetPolicies()
        {
            return this.policies;
        }
    }
}

using Altran.ILS.Data.Interfaces;
using Altran.ILS.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altran.ILS.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository policyRepository;

        public PolicyService(IPolicyRepository policyRepository)
        {
            this.policyRepository = policyRepository;
        }
 
        public IEnumerable<Policy> GetPolicies(string clientId)
        {
            return this.policyRepository.GetPolicies().Results.Where(x => x.ClientId == clientId).Select(x => x).ToList();
        }

        public string GetClientId(string policyNumber)
        {
            return this.policyRepository.GetPolicies().Results.Where(x => x.Id == policyNumber).Select(x => x.ClientId).FirstOrDefault();
        }
    }
}

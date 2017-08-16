using Altran.ILS.Data.Interfaces;
using Altran.ILS.Domain.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altran.ILS.Services
{
    public class FakePolicyService  : IPolicyService
    {
        public FakePolicyService(IPolicyRepository policyRepository)
        {
        }
 
        public IEnumerable<Policy> GetPolicies(string clientId)
        {
            return GetPolicies().Results.Where(x => x.ClientId == clientId).Select(x => x).ToList();
        }
        
        public string GetClientId(string policyNumber)
        {
            return GetPolicies().Results.Where(x => x.Id == policyNumber).Select(x => x.ClientId).FirstOrDefault();
        }

        private Policies GetPolicies()
        {
            #region - Test data -

            var jsonString = @"{  
                   'policies':[  
                      {  
                         'id':'64cceef9-3a01-49ae-a23b-3761b604800b',
                         'amountInsured':1825.89,
                         'email':'inesblankenship@quotezart.com',
                         'inceptionDate':'2016-06-01T03:33:32Z',
                         'installmentPayment':true,
                         'clientId':'e8fd159b-57c4-4d36-9bd7-a59ca13057bb'
                      },
                      {  
                         'id':'7b624ed3-00d5-4c1b-9ab8-c265067ef58b',
                         'amountInsured':399.89,
                         'email':'inesblankenship@quotezart.com',
                         'inceptionDate':'2015-07-06T06:55:49Z',
                         'installmentPayment':true,
                         'clientId':'a0ece5db-cd14-4f21-812f-966633e7be86'
                      },
                      {  
                         'id':'56b415d6-53ee-4481-994f-4bffa47b5239',
                         'amountInsured':2301.98,
                         'email':'inesblankenship@quotezart.com',
                         'inceptionDate':'2014-12-01T05:53:13Z',
                         'installmentPayment':false,
                         'clientId':'44e44268-dce8-4902-b662-1b34d2c10b8e'
                      }
                   ]
                }";

            #endregion - Test data -

            return JsonConvert.DeserializeObject<Policies>(jsonString);        
        }
    }
}

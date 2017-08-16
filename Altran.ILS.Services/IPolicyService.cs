using Altran.ILS.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altran.ILS.Services
{
    public interface IPolicyService
    {
        // * Get the list of policies linked to a user name -> Can be accessed by users with role "admin"
        IEnumerable<Policy> GetPolicies(string username);
        // * Get the user linked to a policy number -> Can be accessed by users with role "admin"
        string GetClientId(string policyNumber);
    }
}

using Altran.ILS.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altran.ILS.Services
{
    public interface IClientService
    {
        //* Get user data filtered by user id -> Can be accessed by users with role "users" and "admin"
        Client GetClientById(string clientId); 

        //* Get user data filterd by user name -> Can be accessed by users with role "users" and "admin"
        Client GetClient(string userName);

        string GetClientRole(string username);
        bool IsClientAdmin(string username);
        bool IsClientUser(string username);
    }
}

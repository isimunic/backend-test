using Altran.ILS.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altran.ILS.Data.Interfaces
{
    public interface IClientRepository
    {
        Clients GetClients();
    }
}

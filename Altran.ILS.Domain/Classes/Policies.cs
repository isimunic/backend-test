using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altran.ILS.Domain.Classes
{
    public class Policies
    {
        [JsonProperty("policies")]
        public IEnumerable<Policy> Results;
    }
}

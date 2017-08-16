using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altran.ILS.Domain.Classes
{
    public class Policy
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("amountInsured")]
        public decimal AmountInsured { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("inceptionDate")]
        public DateTimeOffset InceptionDate { get; set; }

        [JsonProperty("installmentPayment")]
        public bool InstallmentPayment { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }
    }
}

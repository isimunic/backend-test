using Altran.ILS.Domain.Classes;
using Altran.ILS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace Altran.ILS.WebApi.Controllers
{

    //* Get the list of policies linked to a user name -> Can be accessed by
    //users with role "admin"
    //* Get the user linked to a policy number -> Can be accessed by users with
    //role "admin"

    public class PoliciesController : ApiController
    {
        private readonly IPolicyService policyService;
        private readonly IClientService clientService;

        public PoliciesController(IPolicyService policyService)
        {
            this.policyService = policyService;
        }

        public PoliciesController(IPolicyService policyService, IClientService clientService)
        {
            this.policyService = policyService;
            this.clientService = clientService;
        }

        // GET api/policies
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            return response;
        }

        /// <summary>
        /// GET api/policies/64cceef9-3a01-49ae-a23b-3761b604800b  Get the user linked to a policy number. 
        /// Only allow execution for users with role 'admin' 
        /// </summary>
        /// <param name="id">64cceef9-3a01-49ae-a23b-3761b604800b</param>
        /// <returns>Client in Json format if exists. Otherwise returns No Content status server.</returns>
        [Authorize]
        public HttpResponseMessage GetClientByPolicyId(string id)
        {
            try
            {
                var requestingUser = Thread.CurrentPrincipal.Identity.Name;

                if (clientService.IsClientAdmin(requestingUser))
                {
                    var clientId = policyService.GetClientId(id);
                    var client = clientService.GetClientById(clientId);
                    if (client == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                    return response;
                }
            }
            catch (Exception)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }

        /// <summary>
        /// GET api/policies?username=Manning Gets the list of policies linked to a user name. 
        /// Only allow execution for users with role 'admin' and 'user'
        /// </summary>
        /// <param name="username">Merrill</param>
        /// <returns>Policies in Json format if exists. Otherwise returns empty list.</returns>
        [Authorize]
        public HttpResponseMessage GetPoliciesByUsername(string username)
        {
            try
            {
                var requestingUser = Thread.CurrentPrincipal.Identity.Name;

                if (clientService.IsClientAdmin(requestingUser) || clientService.IsClientUser(requestingUser))
                {
                    var client = clientService.GetClient(username);
                    if (client == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    } 
                
                    Policies policies = new Policies();
                    policies.Results = policyService.GetPolicies(client.Id);
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(policies), Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Forbidden);
                    return response;
                }
            }
            catch (Exception)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
        }

        /// <summary>
        /// POST api/policies. Not implemented. Returns Forbidden status code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]string value)
        {
            var response = Request.CreateResponse(HttpStatusCode.Forbidden);
            return response;
        }

        /// <summary>
        /// PUT api/policies/5. Not implemented. Returns Forbidden status code.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            var response = Request.CreateResponse(HttpStatusCode.Forbidden);
            return response;
        }

        /// <summary>
        /// DELETE api/policies/5. Not implemented. Returns Forbidden status code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.Forbidden);
            return response;
        }
    }
}

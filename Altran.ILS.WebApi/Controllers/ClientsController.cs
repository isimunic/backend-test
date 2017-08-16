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
    public class ClientsController : ApiController
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        /// <summary>
        /// GET api/clients. There is no page for this GET. 
        /// </summary>
        /// <returns>Page not Found</returns>
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.NotFound);
            return response;
        }

        /// <summary>
        /// GET api/clients/id Gets a client matching id value from parameter. 
        /// Only allow execution for users with role 'admin' and 'user'
        /// </summary>
        /// <param name="id">44e44268-dce8-4902-b662-1b34d2c10b8e</param>
        /// <returns>Client in Json format if exists. Else returns No Content status server.</returns>
        [Authorize]
        public HttpResponseMessage GetById(string id)
        {
            try
            {
                var requestingUser = Thread.CurrentPrincipal.Identity.Name;

                if (clientService.IsClientAdmin(requestingUser) || clientService.IsClientUser(requestingUser))
                {
                    Client client = clientService.GetClientById(id);
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
        /// GET api/clients?username=Merrill Gets a client matching username value from parameter. 
        /// Only allow execution for users with role 'admin' and 'user'
        /// </summary>
        /// <param name="username">Merrill</param>
        /// <returns>Client in Json format if exists. Otherwise returns No Content status server.</returns>
        [Authorize] 
        public HttpResponseMessage GetByUsername(string username)
        {
            try
            {
                var requestingUser = Thread.CurrentPrincipal.Identity.Name;

                if (clientService.IsClientAdmin(requestingUser) || clientService.IsClientUser(requestingUser))
                {
                    Client client = clientService.GetClient(username);
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
        /// POST api/clients. Not implemented. Returns Forbidden status code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]string value)
        {
            var response = Request.CreateResponse(HttpStatusCode.Forbidden);
            return response;
        }

        /// <summary>
        /// PUT api/clients/5. Not implemented. Returns Forbidden status code.
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
       /// DELETE api/clients/5. Not implemented. Returns Forbidden status code.
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
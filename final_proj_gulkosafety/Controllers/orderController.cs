using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class orderController : ApiController
    {
        public List<order> Get()
        {
            try
            {
                order o = new order();
                List<order> oList = o.ReadOrder();
                return oList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public HttpResponseMessage Post([FromBody] order o)
        {
            try
            {
                {
                    o.Insert();

                }

                return Request.CreateResponse(HttpStatusCode.Created, o);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
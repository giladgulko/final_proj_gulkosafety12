using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class items_in_orderController : ApiController
    {
        //POST api/<controller>
        public HttpResponseMessage Post([FromBody] items_in_order i)
        {
            try
            {
                {
                    i.InsertItemInOrder();

                }

                return Request.CreateResponse(HttpStatusCode.Created, i);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        public List<items_in_order> Get(int order_num)
        {
            try
            {
                items_in_order i = new items_in_order();
                List<items_in_order> iList = i.Read(order_num);
                return iList;
            }
            catch (Exception ex)
            {
                throw (ex);
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
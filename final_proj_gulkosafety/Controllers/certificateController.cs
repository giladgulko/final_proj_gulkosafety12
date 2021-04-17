using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class certificateController : ApiController
    {

        public List<certificate> Get()
        {
            try
            {
                certificate c = new certificate();
                List<certificate> cList = c.ReadCertificate();
                return cList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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
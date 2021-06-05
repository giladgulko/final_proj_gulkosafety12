using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class certificate_typeController : ApiController
    {

        public List<certificate_type> Get()
        {
            try
            {
                certificate_type c = new certificate_type();
                List<certificate_type> cList = c.ReadCertificate_type();
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
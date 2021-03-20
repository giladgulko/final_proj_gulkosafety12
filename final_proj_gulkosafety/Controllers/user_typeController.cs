using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class user_typeController : ApiController
    {
        // GET all types api/<controller>
        public List<user_type> Get()
        {
            user_type ut = new user_type();
            List<user_type> utList = ut.Read();
            return utList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST new type api/<controller>
        public void Post([FromBody] user_type ut)
        {
            ut.Insert();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
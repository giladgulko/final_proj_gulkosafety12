using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class instruction_typeController : ApiController
    {
        public List<instruction_type> Get()
        {
            try
            {
                instruction_type i = new instruction_type();
                List<instruction_type> iList = i.ReadInstruction_type();
                return iList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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
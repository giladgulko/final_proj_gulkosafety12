using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defectController : ApiController
    {
        public List<defect> Get()
        {
            defect _defect = new defect();
            return _defect.ReadDefect();
        }

        // POST api/<controller>
        public void Post([FromBody] defect _defect)
        {
            _defect.InsertDefect();
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
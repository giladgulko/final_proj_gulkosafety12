using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defect_typeController : ApiController
    {
        public List<defect_type> Get()
        {
            defect_type _defect_type = new defect_type();
            List<defect_type> defectTypeList = _defect_type.ReadDefectType();
            return defectTypeList;
        }

        // POST api/<controller>
        // public void Post([FromBody] defect_type _defect_type)
        // {
        //    _defect_type.InsertDefectType();
        //    }

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
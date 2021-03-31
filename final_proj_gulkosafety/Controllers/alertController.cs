using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class alertController : ApiController
    {
        public List<alert> Get(string user_email)
        {
            alert a = new alert();
            List<alert> aList = a.Read(user_email);
            return aList;
        }
        public List<alert> Get(int proj_num)
        {
            alert a = new alert();
            List<alert> alertList = a.Read(proj_num);
            return alertList;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        //public void Put( [FromBody] alert a)
        //{
        //    a.UpdateAlert();
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
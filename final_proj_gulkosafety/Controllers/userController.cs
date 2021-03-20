using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class userController : ApiController
    {
        // GET all users api/<controller>
        public List<user> Get()
        {
            user u = new user();
            List<user> uList = u.Read();
            return uList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST one project api/<controller>
        public void Post([FromBody] user u)
        {
            u.InsertUser();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

     
        public List<user> Get(string manager_email, string foreman_email)
        {
            user p = new user();
            return p.Read_user_in_project(manager_email, foreman_email);
        }

    }
}
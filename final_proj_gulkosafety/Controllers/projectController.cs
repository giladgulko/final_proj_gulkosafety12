using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class projectController : ApiController
    {
        public List<project> Get()
        {
            project p = new project();
            List<project> pList = p.Read();
            return pList;
        }

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody] project p)
        {
            p.Insert();
        }

        //PUT api/<controller>/5
        public void PutProjectDetails([FromBody] project p)
        {
            p.UpdateProjectDetails();
        }

        //public void PutProjectStatus([FromBody] project p)
        //{
        //    p.UpdateProjectStatus(p.Project_num, p.Status);
        //}

        public void PutProjectUsers(int project_num, [FromBody] project p)
        {
            p.UpdateProjectUser();
        }

        // DELETE api/<controller>/5
        public void DeleteProject([FromBody] project p)
        {
            p.DeleteProject(p.Project_num);
        }

    }
}
using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class reportController : ApiController
    {
        //get all project's reports
        //[HttpGet]
        //[Route("api/report/")]
        public List<report> Get(int proj_num)
        { 
            report r = new report();
            List<report> reprotList = r.ReadReport(proj_num);
            return reprotList;
        }

        public report GetLastReport([FromBody] project p)
        {
            report r = new report();
            r.readLastReport(p.Project_num);
            return r ;
        }

        public void Post([FromBody] report r)
        {
            r.InsertReport();
        }

        public void DeleteReport(int report_num)
        {
            report r = new report();
            r.DeleteReport(report_num);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
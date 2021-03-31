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

        [Route("api/report/{project_num}/lastReport")]
        public report GetLastReport(int project_num)
        {
            report r = new report();
            return r.readLastReport(project_num);
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
        public void Put([FromBody] report r)
        {
            r.UpdateReport();
        }

        public void PUTReportGrage(int report_num,double grade)
        {
            report r = new report();
            r.UpdateReportGrade(report_num, grade);
        }
    }
}
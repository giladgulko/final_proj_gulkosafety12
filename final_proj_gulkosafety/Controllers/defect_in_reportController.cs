using final_proj_gulkosafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace final_proj_gulkosafety.Controllers
{
    public class defect_in_reportController : ApiController
    {
        public List<defect_in_report> Get(int report_num)
        {
            defect_in_report defectsInReport = new defect_in_report();
            List<defect_in_report> defect_in_reportList = defectsInReport.ReadDefectsInReport(report_num);
            return defect_in_reportList;
        }

        public void DeleteDefectInReport([FromBody] defect_in_report dr)
        {
            dr.DeleteDefectInReport(dr.Report_num,dr.Defect_num);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}
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
        //מחזירה את כל הליקוים בדוח שלפני הדוח הנוכחי. בשביל השוואה בין הדוחות, לאלגוריתם החכם
        public List<defect_in_report> GetLastReportDefects(int proj_num,DateTime reportDate)
        {
            defect_in_report LastReportDefect = new defect_in_report();
            List<defect_in_report> LastReportDefects = LastReportDefect.readLastReportDefect(proj_num, reportDate);
            return LastReportDefects;
        }
        public void DeleteDefectInReport([FromBody] defect_in_report dr)
        {
            dr.DeleteDefectInReport(dr.Report_num,dr.Defect_num);
        }


        // POST api/<controller>
      

        public void Post([FromBody] defect_in_report p)
        {
            p.InsertDefectInReport();

        }

        //update defect in report without 
        public void Put([FromBody] defect_in_report defectInReport)
        {
            defectInReport.UpdateDefectInReport();
        }

    }
}
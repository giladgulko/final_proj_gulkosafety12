using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class defect_in_report
    {
        int report_num;
        int defect_num;
        DateTime fix_date;
        DateTime fix_time;
        string picture_link;
        int fix_status;
        string description;


        public int Report_num { get => report_num; set => report_num = value; }
        public int Defect_num { get => defect_num; set => defect_num = value; }
        public DateTime Fix_date { get => fix_date; set => fix_date = value; }
        public DateTime Fix_time { get => fix_time; set => fix_time = value; }
        public string Picture_link { get => picture_link; set => picture_link = value; }
        public int Fix_status { get => fix_status; set => fix_status = value; }
        public string Description { get => description; set => description = value; }

        public defect_in_report(int report_num, int defect_num, DateTime fix_date, DateTime fix_time, string picture_link, int fix_status, string description)
        {
            Report_num = report_num;
            Defect_num = defect_num;
            Fix_date = fix_date;
            Fix_time = fix_time;
            Picture_link = picture_link;
            Fix_status = fix_status;
            Description = description;
        }
        public defect_in_report() { }


        public List<defect_in_report> ReadDefectsInReport(int report_num)
        {
            DBServices dbs = new DBServices();
            List<defect_in_report> defectsInReportList = dbs.ReadDefectsInReport(report_num);
            return defectsInReportList;
        }
        public void DeleteDefectInReport(int report_num, int defect_num)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteDefectInReport(report_num, defect_num);

        }
        public void UpdateDefectInReport(int defect_num, DateTime fix_date, DateTime fix_time, string pic_link, int fix_status, string desc)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateDefectInReport(defect_num, fix_date, fix_time, pic_link, fix_status, desc);

        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace final_proj_gulkosafety.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        //insert a new user
        public int InsertUser(user _user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(user _user)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}')", _user.Email, _user.Name, _user.Phone, _user.Password, _user.User_type_num);
            String prefix = "INSERT INTO user " + "(email,name,phone,password,user_type_num)";
            command = prefix + sb.ToString();

            return command;

        }
        //insert a new defect
        public int InsertDefect(defect def)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(def);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(defect _defect)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}')", _defect.Name, _defect.Grade, _defect.Defect_type_num);
            String prefix = "INSERT INTO defect " + "(name, grade,defect_type_num)";
            command = prefix + sb.ToString();

            return command;

        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }


        // get all defect
        public List<defect> ReadDefect()
        {
            SqlConnection con = null;
            List<defect> defectList = new List<defect>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM defect";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    defect _defect = new defect();
                    _defect.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defect.Name = (string)dr["name"];
                    _defect.Grade = Convert.ToDouble(dr["grade"]);
                    _defect.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);

                    defectList.Add(_defect);
                }

                return defectList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //update project user
        public int UpdateProjectUser(project p)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateContactsCommand(p);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateContactsCommand(project p)
        {
            String command;
            command = "UPDATE project SET manager_email='" + p.Manager_email + "', foreman_email='" + p.Foreman_email + "' WHERE project_num =" + p.Project_num;

            return command;
        }



        //update project detail
        public int UpdateProjectDetails(project p)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateCommand(p);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateCommand(project p)
        {
            String command;
            command = "UPDATE project SET name='" + p.Name + "', company='" + p.Company + "', address='" + p.Address + "', start_date='" + p.Start_date.ToString("yyyy-MM-dd") + "', end_date='" + p.End_date.ToString("yyyy-MM-dd") + "', status=" + p.Status + ", description='" + p.Description + "', project_type_num=" + p.Project_type_num + ", manager_email='" + p.Manager_email + "', foreman_email='" + p.Foreman_email + "' WHERE project_num =" + p.Project_num;

            return command;
        }

        //update project safety level
        public int UpdateProjectSafetyLvl(int project_num, double safety_lvl)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildupdateSafetyLvlCommand(project_num, safety_lvl);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildupdateSafetyLvlCommand(int project_num, double safety_lvl)
        {
            String command;
            command = "UPDATE project SET safety_lvl=" + safety_lvl + " WHERE project_num =" + project_num;
            return command;
        }

        //update project status
        public int UpdateProjectStatus(int proj_num, int status)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateCommand(proj_num, status);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateCommand(int proj_num, int status)
        {
            String command;
            command = "UPDATE project SET status=" + status + "WHERE project_num =" + proj_num;

            return command;
        }

        //returns all projects
        public List<project> ReadProjects()
        {
            SqlConnection con = null;
            List<project> projectList = new List<project>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from project";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    project p = new project();

                    p.Project_num = Convert.ToInt32(dr["project_num"]);
                    p.Name = (string)dr["name"];
                    p.Company = (string)dr["company"];
                    p.Address = (string)dr["address"];
                    p.Start_date = Convert.ToDateTime(dr["start_date"]);
                    p.End_date = Convert.ToDateTime(dr["end_date"]);
                    p.Status = Convert.ToInt32(dr["status"]);
                    p.Description = (string)dr["description"];
                    p.Safety_lvl = Convert.ToDouble(dr["safety_lvl"]);
                    p.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    p.Manager_email = (string)dr["manager_email"];
                    p.Foreman_email = (string)dr["foreman_email"];

                    projectList.Add(p);


                }

                return projectList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //return all users in project
        public List<user> Read_user_in_project(string manager_email, string foreman_email)
        {
            SqlConnection con = null;
            List<user> userList = new List<user>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "  select * from [user]  WHERE email='" + manager_email + "' or email='" + foreman_email + "'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    user user_proj = new user();

                    user_proj.Email = (string)dr["email"];
                    user_proj.Name = (string)dr["name"];
                    user_proj.Phone = (string)dr["phone"];
                    user_proj.Password = (string)dr["password"];
                    user_proj.User_type_num = Convert.ToInt32(dr["user_type_num"]);
                    userList.Add(user_proj);

                }

                return userList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //read projects' report
        public List<report> ReadReport(int proj_num)
        {
            SqlConnection con = null;
            List<report> reportList = new List<report>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM report WHERE project_num=" + proj_num;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    report _report = new report();
                    _report.Report_num = Convert.ToInt32(dr["report_num"]);
                    _report.Date = Convert.ToDateTime(dr["date"]);
                    _report.Time = Convert.ToDateTime(dr["time"]);
                    _report.Comment = (string)dr["comment"];
                    _report.Grade = Convert.ToDouble(dr["grade"]);
                    _report.Project_num = Convert.ToInt32(dr["project_num"]);
                    _report.User_mail = (string)dr["user_email"];
                    reportList.Add(_report);
                }

                return reportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //delete project 
        public int DeleteProject(int proj_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommand(proj_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteCommand(int proj_num)
        {
            String command;
            command = "delete from project where project_num=" + proj_num;
            return command;
        }

        //delete report 
        public int DeleteReport(int report_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteReportCommand(report_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildDeleteReportCommand(int report_num)
        {
            String command;
            command = "delete from report where report_num=" + report_num;
            return command;
        }

        //insert a new report
        public int InsertReport(report _report)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_report);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(report _report)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}','{1}','{2}','{3}','{4}')", _report.Date.ToString("yyyy-MM-dd"), _report.Time.ToString("HH:mm"), _report.Grade, _report.Comment, _report.Project_num);
            String prefix = "INSERT INTO report " + "(date,time,grade,comment,project_num)";
            command = prefix + sb.ToString();

            return command;

        }

        //insert a whole new project
        public int InsertProject(project _project)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_project);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertCommand(project _project)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')", _project.Name, _project.Company, _project.Address, _project.Start_date.ToString("yyyy-MM-dd"), _project.End_date.ToString("yyyy-MM-dd"), _project.Status, _project.Description, _project.Safety_lvl, _project.Project_type_num, _project.Manager_email, _project.Foreman_email);
            String prefix = "INSERT INTO project " + "(name,company,address,start_date,end_date,status,description,safety_lvl,project_type_num,manager_email,foreman_email)";
            command = prefix + sb.ToString();

            return command;

        }

        //read all defects in report
        public List<defect_in_report> ReadDefectsInReport(int report_num)
        {
            SqlConnection con = null;
            List<defect_in_report> defectsInReportList = new List<defect_in_report>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select di.*,d.name,dt.type_name,dt.defect_type_num from defect_in_report di inner join defect d on di.defect_num=d.defect_num inner join defect_type dt on d.defect_type_num=dt.defect_type_num where di.report_num=" + report_num;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    defect_in_report _defectInReport = new defect_in_report();

                    _defectInReport.Report_num = Convert.ToInt32(dr["report_num"]);
                    _defectInReport.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defectInReport.Fix_date = Convert.ToDateTime(dr["fix_date"]);
                    _defectInReport.Fix_time = Convert.ToDateTime("12:30");
                    _defectInReport.Picture_link = (string)dr["picture_link"];
                    _defectInReport.Fix_status = Convert.ToInt32(dr["fix_status"]);
                    _defectInReport.Description = (string)dr["description"];
                    _defectInReport.Defect_name = (string)dr["name"];
                    _defectInReport.Defect_type_name = (string)dr["type_name"];
                    defectsInReportList.Add(_defectInReport);
                }

                return defectsInReportList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
       
        // delete defect in report
        public int DeleteDefectInReport(int report_num, int defect_num)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildDeleteCommand(report_num, defect_num);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildDeleteCommand(int report_num, int defect_num)
        {
            String command;
            command = "DELETE FROM defect_in_report WHERE report_num=" + report_num + "and defect_num=" + defect_num;
            return command;
        }

        //update defect in report- all fields- date, time, fix status, description
        public int UpdateDefectInReport(defect_in_report defectInReport)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateCommand(defectInReport);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateCommand(defect_in_report defectInReport)
        {
            String command;
            command = "UPDATE defect_in_report SET fix_date='" + defectInReport.Fix_date.ToString("yyyy-MM-dd") + "', fix_time='" + defectInReport.Fix_time.ToString("HH:ss") + "', fix_status=" + defectInReport.Fix_status + " , description='"+ defectInReport.Description+ "' WHERE defect_num =" + defectInReport.Defect_num+" and report_num="+ defectInReport.Report_num;

            return command;
        }

        //update defect in report- only image link
        public int UpdateDefectInReportImg(defect_in_report defectInReport)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            String cStr = BuildupdateImgCommand(defectInReport);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildupdateImgCommand(defect_in_report defectInReport)
        {
            String command;
            command = "UPDATE defect_in_report SET picture_link='" + defectInReport.Picture_link + "' WHERE defect_num =" + defectInReport.Defect_num + " and report_num=" + defectInReport.Report_num;

            return command;
        }

        //update report
        public int UpdateReport(report r)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildUpdateReportCommand(r);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        private String BuildUpdateReportCommand(report r)
        {
            String command;
            command = "UPDATE report SET date='" + r.Date.ToString("yyyy-MM-dd") + "', time='" + r.Time.ToString("HH:ss") + "', comment='" + r.Comment + "' WHERE report_num =" + r.Report_num;

            return command;
        }

        //read all defect type
        public List<defect_type> ReadDefectType()
        {
            SqlConnection con = null;
            List<defect_type> defectTypeList = new List<defect_type>();

            try
            {
                con = connect("DBConnectionString");
                String selectSTR = "";

                selectSTR = "select * from defect_type";



                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    defect_type dt = new defect_type();

                    dt.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);
                    dt.Type_name = (string)dr["type_name"];

                    defectTypeList.Add(dt);
                }
                return defectTypeList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<project_type> ReadProjectTypes()
        {

            SqlConnection con = null;
            List<project_type> ptlist = new List<project_type>();

            try
            {
                con = connect("DBConnectionString"); 

                String selectSTR = "SELECT * FROM [project_type]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 

                while (dr.Read())
                {
                    project_type pt = new project_type();
                    pt.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    pt.Project_type_name = (string)dr["project_type_name"];
                    ptlist.Add(pt);
                }

                return ptlist;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //insert a whole new project type- with weights
        public void InsertProjectType(project_type pt)
        {

        }
       
        public List<user> ReadUsers()
        {
            SqlConnection con = null;
            List<user> uList = new List<user>();

            try
            {
                con = connect("DBConnectionString"); 

                String selectSTR = "SELECT * FROM [user]";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 

                while (dr.Read())
                {
                    user u = new user();
                    u.Email = (string)dr["email"];
                    u.Name = (string)dr["name"];
                    u.Phone = (string)dr["phone"];
                    u.Password = (string)dr["password"];
                    u.User_type_num = Convert.ToInt32(dr["user_type_num"]);
                    uList.Add(u);
                }

                return uList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //insert new defect in report
        public int InsertDefectInReport(defect_in_report _defect_in_report)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); 
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertCommand(_defect_in_report); 

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); 
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertCommand(defect_in_report _defect_in_report)
        {
            String command;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values('{0}', '{1}', '{2}','{3}', '{4}','{5}','{6}')", _defect_in_report.Report_num, _defect_in_report.Defect_num, _defect_in_report.Fix_date.ToString("yyyy-MM-dd"), _defect_in_report.Fix_time.ToString("HH:mm"), _defect_in_report.Picture_link, _defect_in_report.Fix_status, _defect_in_report.Description); ;
            String prefix = "INSERT INTO defect_in_report " + "(report_num,defect_num,fix_date,fix_time,picture_link,fix_status,description)";
            command = prefix + sb.ToString();

            return command;

        }


        //returns all user types
        public List<user_type> ReadUserTypes()
        {
            List<user_type> utlist = new List<user_type>();
            return utlist;
        }
        //insert a new user type
        public void InsertUserType(user_type ut)
        {

        }
        //get last report
        public report ReadLastReport(int proj_num)
        {
            SqlConnection con = null;
            report lastreport = new report();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select top 1* from report where project_num="+ proj_num + " order by report_num desc" ;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                // Read till the end of the data into a row
                while (dr.Read())
                {
                    lastreport.Report_num = Convert.ToInt32(dr["report_num"]);
                    lastreport.Date = Convert.ToDateTime(dr["date"]);
                    lastreport.Time = Convert.ToDateTime(dr["time"]);
                    lastreport.Comment = (string)dr["comment"];
                    lastreport.Grade = Convert.ToDouble(dr["grade"]);
                    lastreport.Project_num = Convert.ToInt32(dr["project_num"]);
                    lastreport.User_mail = (string)dr["user_email"];
                }

                return lastreport;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        //get all project type weights
        public List<proj_type_weight> ReadProj_type_weight(int proj_type)
        {
            SqlConnection con = null;
            List<proj_type_weight> ptwList = new List<proj_type_weight>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select* from weight_type_defects where project_type_num="+ proj_type;
                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    proj_type_weight ptw = new proj_type_weight();
                    ptw.Project_type_num = Convert.ToInt32(dr["project_type_num"]);
                    ptw.Defect_type_num = Convert.ToInt32(dr["defect_type_num"]);
                    ptw.Weight = Convert.ToDouble(dr["weight"]);
                    
                    ptwList.Add(ptw);
                }

                return ptwList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //get all last report defects
        public List<defect_in_report> readLastReportDefects(int proj_num, DateTime date)
        {
            SqlConnection con = null;
            List<defect_in_report> lastDinR = new List<defect_in_report>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "select di.*,d.name,dt.type_name,dt.defect_type_num from defect_in_report di inner join defect d on di.defect_num=d.defect_num inner join defect_type dt on d.defect_type_num=dt.defect_type_num where di.report_num=(select top 1 report_num from report where project_num=" + proj_num + " and date <'"+date.ToString("yyyy-MM-dd") + "' order by report_num desc)";

                SqlCommand cmd = new SqlCommand(selectSTR, con);


                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    defect_in_report _defectInReport = new defect_in_report();

                    _defectInReport.Report_num = Convert.ToInt32(dr["report_num"]);
                    _defectInReport.Defect_num = Convert.ToInt32(dr["defect_num"]);
                    _defectInReport.Fix_date = Convert.ToDateTime(dr["fix_date"]);
                    _defectInReport.Fix_time = Convert.ToDateTime("12:30");
                    _defectInReport.Picture_link = (string)dr["picture_link"];
                    _defectInReport.Fix_status = Convert.ToInt32(dr["fix_status"]);
                    _defectInReport.Description = (string)dr["description"];
                    _defectInReport.Defect_name = (string)dr["name"];
                    _defectInReport.Defect_type_name = (string)dr["type_name"];
                    lastDinR.Add(_defectInReport);
                }

                return lastDinR;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        //read alerts user email
        public List<alert> ReadAlerts(string user_email)
        {
            SqlConnection con = null;
            List<alert> alertsList = new List<alert>();

            try
            {
                con = connect("DBConnectionString"); 

                String selectSTR = "SELECT * FROM alert WHERE user_email='" + user_email + "'and status=0 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {  

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    alertsList.Add(_alert);
                }

                return alertsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //read alerts by project number
        public List<alert> ReadAlerts(int proj_num)
        {
            SqlConnection con = null;
            List<alert> alertsList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM alert WHERE proj_num='" + proj_num + "'and status=0 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    alertsList.Add(_alert);
                }

                return alertsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int UpdateReportGrade(int report_num, double grade)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception)
            {
                throw new Exception("The connection to sever is not good");
            }

            String cStr = BuildUpdateReportCommand(report_num,grade);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //update alert
        public int UpdateAlert(alert a)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildUpdateAlertCommand(a);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception)
            {
                throw new Exception("The update of status alert failed");
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildUpdateAlertCommand(alert a)
        {
            String command;
            command = "UPDATE alert SET content='" + a.Content + "', alert_type_num=" + a.Alert_type_num + ", date='" + a.Date.ToString("yyyy-MM-dd HH:mm") + "', User_email='" + a.User_email + "', status=" + a.Status + ", proj_num=" + a.Proj_num+ " Where alert_num="+a.Alert_num;

            return command;
        }


        public int InsertAlert(alert a)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String cStr = BuildInsertAlertCommand(a);

            cmd = CreateCommand(cStr, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("The update of status alert failed");
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        private String BuildInsertAlertCommand(alert a)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", a.Content, a.Alert_type_num, a.User_email, a.Status, a.Date.ToString("yyyy-MM-dd HH:mm:ss"), a.Proj_num);
            String prefix = "INSERT INTO alert " + "(content, alert_type_num,user_email,status,date,proj_num) ";
            command = prefix + sb.ToString();

            return command;
        }

        private String BuildUpdateReportCommand(int report_num, double grade)
        {
            String command;
            command = "UPDATE report SET grade=" + grade +  " WHERE report_num =" + report_num;

            return command;
        }
        //read Alert Archive by user_email
        public List<alert> ReadAlertArchive(string user_email)
        {
            SqlConnection con = null;
            List<alert> alertsArchiveList = new List<alert>();

            try
            {
                con = connect("DBConnectionString");

                String selectSTR = "SELECT * FROM alert WHERE user_email='" + user_email + "'and status=1 order by [date] desc";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {

                    alert _alert = new alert();
                    _alert.Alert_num = Convert.ToInt32(dr["alert_num"]);
                    _alert.Content = (string)dr["content"];
                    _alert.Date = Convert.ToDateTime(dr["date"]);
                    _alert.Alert_type_num = Convert.ToInt32(dr["alert_type_num"]);
                    _alert.User_email = (string)dr["user_email"];
                    _alert.Status = Convert.ToInt32(dr["status"]);
                    _alert.Proj_num = Convert.ToInt32(dr["proj_num"]);
                    alertsArchiveList.Add(_alert);
                }

                return alertsArchiveList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

    }
}
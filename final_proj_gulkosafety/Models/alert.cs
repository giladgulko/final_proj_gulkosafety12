﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class alert
    {
        int alert_num;
        string content;
        DateTime date;
        DateTime time;
        int alert_type_num;
        string user_email;

        public alert(int alert_num, string content,DateTime date,DateTime time,int alert_type_num, string user_email)
        {
            Alert_num = alert_num;
            Content = content;
            Date = date;
            Time = time;
            Alert_type_num = alert_type_num;
            User_email = user_email;
        }

        public alert() { }

        public int Alert_num { get => alert_num; set => alert_num = value; }
        public string Content { get => content; set => content = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public int Alert_type_num { get => alert_type_num; set => alert_type_num = value; }
        public string User_email { get => user_email; set => user_email = value; }
    }
}
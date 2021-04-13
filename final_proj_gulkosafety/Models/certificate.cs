using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class certificate
    {
        int certificate_num;
        string company;
        DateTime date;
        string address;
        string discription;
        int certificate_statues;
        int pay_statues;
        string bill_num;
        int certificate_type_num;
        string user_email;
        string contact_id;

        public certificate(int certificate_num, string company, DateTime date, string address, string discription, int certificate_statues, int pay_statues, string bill_num, int certificate_type_num, string user_email, string contact_id)
        {
            Certificate_num = certificate_num;
            Company = company;
            Date = date;
            Address = address;
            Discription = discription;
            Certificate_statues = certificate_statues;
            Pay_statues = pay_statues;
            Bill_num = bill_num;
            Certificate_type_num = certificate_type_num;
            User_email = user_email;
            Contact_id = contact_id;
        }
        public certificate() { }

        public int Certificate_num { get => certificate_num; set => certificate_num = value; }
        public string Company { get => company; set => company = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Address { get => address; set => address = value; }
        public string Discription { get => discription; set => discription = value; }
        public int Certificate_statues { get => certificate_statues; set => certificate_statues = value; }
        public int Pay_statues { get => pay_statues; set => pay_statues = value; }
        public string Bill_num { get => bill_num; set => bill_num = value; }
        public int Certificate_type_num { get => certificate_type_num; set => certificate_type_num = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public string Contact_id { get => contact_id; set => contact_id = value; }
    }
}
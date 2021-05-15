﻿using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class contact
    {
        int id;
        string full_name;
        int phone;
        string mail;



        public int Id { get => id; set => id = value; }
        public string Full_name { get => full_name; set => full_name = value; }
        public int Phone { get => phone; set => phone = value; }
        public string Mail { get => mail; set => mail = value; }

        public contact(int id, string full_name, int phone, string mail)
        {
            Id = id;
            Full_name = full_name;
            Phone = phone;
            Mail = mail;
        }
        public contact(){ }

        public void InsertContact()
        {
            DBServices dbs = new DBServices();
            dbs.InsertContact(this);
        }
    }
}
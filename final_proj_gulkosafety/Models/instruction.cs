using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class instruction
    {
        int Instruction_num;
        string company;
        DateTime date;
        DateTime time;
        string address;
        int participants_num;
        int pay_ststus;
        int total_price;
        int bill_num;
        string instructor_email;
        int instruction_type_num;

        public instruction(int instruction_num1, string company, DateTime date, DateTime time, string address, int participants_num, int pay_ststus, int total_price, int bill_num, string instructor_email, int instruction_type_num)
        {
            Instruction_num1 = instruction_num1;
            Company = company;
            Date = date;
            Time = time;
            Address = address;
            Participants_num = participants_num;
            Pay_ststus = pay_ststus;
            Total_price = total_price;
            Bill_num = bill_num;
            Instructor_email = instructor_email;
            Instruction_type_num = instruction_type_num;
        }
        public instruction() { }

        public int Instruction_num1 { get => Instruction_num; set => Instruction_num = value; }
        public string Company { get => company; set => company = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Address { get => address; set => address = value; }
        public int Participants_num { get => participants_num; set => participants_num = value; }
        public int Pay_ststus { get => pay_ststus; set => pay_ststus = value; }
        public int Total_price { get => total_price; set => total_price = value; }
        public int Bill_num { get => bill_num; set => bill_num = value; }
        public string Instructor_email { get => instructor_email; set => instructor_email = value; }
        public int Instruction_type_num { get => instruction_type_num; set => instruction_type_num = value; }
    }
}
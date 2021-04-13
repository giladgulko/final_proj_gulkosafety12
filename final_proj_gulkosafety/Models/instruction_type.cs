﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class instruction_type
    {
        int instruction_type_num;
        string type_name;
        int expiration;
        double price;

        public instruction_type(int instruction_type_num, string type_name, int expiration, double price)
        {
            Instruction_type_num = instruction_type_num;
            Type_name = type_name;
            Expiration = expiration;
            Price = price;
        }

        public instruction_type() { }

        public int Instruction_type_num { get => instruction_type_num; set => instruction_type_num = value; }
        public string Type_name { get => type_name; set => type_name = value; }
        public int Expiration { get => expiration; set => expiration = value; }
        public double Price { get => price; set => price = value; }
    }
}
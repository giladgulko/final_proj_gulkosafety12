using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class order
    {
        int order_num;
        int bill_num;
        DateTime date;
        double total_price;
        int contact_id;



        public int Order_num { get => order_num; set => order_num = value; }
        public int Bill_num { get => bill_num; set => bill_num = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Total_price { get => total_price; set => total_price = value; }
        public int Contact_id { get => contact_id; set => contact_id = value; }

        public order(int order_num, int bill_num, DateTime date, double total_price, int contact_id)
        {
            Order_num = order_num;
            Bill_num = bill_num;
            Date = date;
            Total_price = total_price;
            Contact_id = contact_id;
        }

        public order()
        {

        }
    }
}
using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class order
    {
        int order_num;
        string bill_num;
        DateTime date;
        double total_price;
        string contact_id;
        int item_num;
        int quantity;


        public int Order_num { get => order_num; set => order_num = value; }
        public string Bill_num { get => bill_num; set => bill_num = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Total_price { get => total_price; set => total_price = value; }
        public string Contact_id { get => contact_id; set => contact_id = value; }
        public int Item_num { get => item_num; set => item_num = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public order(int order_num, string bill_num, DateTime date, double total_price, string contact_id, int item_num, int quantity)
        {
            Order_num = order_num;
            Bill_num = bill_num;
            Date = date;
            Total_price = total_price;
            Contact_id = contact_id;
            Item_num = item_num;
            Quantity = quantity;
        }

        public order(){}

        public List<order> ReadOrder()
        {
            DBServices dbs = new DBServices();
            return dbs.ReadOrder();
        }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.InsertOrder(this);
        }
    }

}
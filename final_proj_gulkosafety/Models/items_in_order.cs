using final_proj_gulkosafety.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class items_in_order
    {
        int order_num;
        int item_num;
        int quantity;

        public int Order_num { get => order_num; set => order_num = value; }
        public int Item_num { get => item_num; set => item_num = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public items_in_order(int order_num, int item_num, int quantity)
        {
            Order_num = order_num;
            Item_num = item_num;
            Quantity = quantity;
        }

        public items_in_order() { }

        public void InsertItemInOrder()
        {
            DBServices dbs = new DBServices();
            dbs.InsertItemInOrder(this);
        }


    }
}
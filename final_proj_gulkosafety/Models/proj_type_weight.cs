using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_proj_gulkosafety.Models
{
    public class proj_type_weight
    {
        int defect_type_num;
        float weight;

        public proj_type_weight(int defect_type_num, float weight)
        {
            Defect_type_num = defect_type_num;
            Weight = weight;
        }

        public int Defect_type_num { get => defect_type_num; set => defect_type_num = value; }
        public float Weight { get => weight; set => weight = value; }

        public proj_type_weight() { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection.Emit;

namespace Lab3
{
    struct DataItem
    {
        public double x{ get; set; }
        public double[] field { get; set; }
        public DataItem(double x, double f1, double f2)
        {
            this.x = x;
            field = new double[2];
            field[0] = f1;
            field[1] = f2;
        }

        public string ToLongString(string format)
        {
            return $"x = {x.ToString(format)} f1 = {field[0].ToString(format)} f2 = {field[1].ToString(format)}";
            //return string.Format(format, );
        }

        public override string ToString()
        {
            return $"x = {x} f1 = {field[0]} f2 = {field[1]}";
        }
    }
}

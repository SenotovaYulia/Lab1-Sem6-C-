using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class RawDataItem
    {
        public double coordinate { get; set; }
        public double value { get; set; }
        public RawDataItem(double init_x, double init_value)
        {
            coordinate = init_x;
            value = init_value;
        }
        public string ToString(string format)
        {
            return $"x = {coordinate.ToString(format)}\nf(x) = {value.ToString(format)}\n";
        }
        override public string ToString()
        {
            return $"x = {coordinate}\nf(x) = {value}\n";
        }
    }
}

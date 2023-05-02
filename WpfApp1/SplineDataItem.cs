using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public struct SplineDataItem
    {
        public double coordinate { get; set; }
        public double value { get; set; }
        public double first_der { get; set; }
        public double second_der { get; set; }
        public SplineDataItem(double init_x, double init_value, double init_first_der, double init_second_der)
        {
            coordinate = init_x;
            value = init_value;
            first_der = init_first_der;
            second_der = init_second_der;
        }
        public string ToString(string format)
        {
            return $"x = {coordinate.ToString(format)}\nf(x) = {value.ToString(format)}\nf'(x) = {first_der.ToString(format)}\n f''(x) = {second_der.ToString(format)}";
        }
        override public string ToString()
        {
            return $"x = {coordinate}\nf(x) = {value}\nf'(x) = {first_der}\n f''(x) = {second_der}";
        }
    }
}

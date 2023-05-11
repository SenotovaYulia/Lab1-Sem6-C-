using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static internal class FloatFunc
    {
        public static double[] init1(double x)
        {
            double[] result = new double[2];
            result[0] = x / 2;
            result[1] = x * x;
            return result;
        }
        public static double[] init2(double x)
        {
            double[] result = new double[2];
            result[0] = 0;
            result[1] = x * x * x - 2;
            return result;
        }
        public static double[] init3(double x)
        {
            double[] result = new double[2];
            result[0] = Math.Sin(x);
            result[1] = Math.Cos(x);
            return result;
        }
    }
}

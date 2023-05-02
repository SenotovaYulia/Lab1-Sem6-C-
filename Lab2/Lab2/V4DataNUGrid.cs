using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class V4DataNUGrid: V4Data
    {
        public double[] notes{ get; set;  }
        public double[] measures_1 { get; set; }
        public double[] measures_2 { get; set; }
        public V4DataNUGrid(string str_data, DateTime date_data) : base(str_data, date_data)
        {
            notes = new double[0];
            measures_1 = new double[0];
            measures_2 = new double[0];
        }
        public V4DataNUGrid(string str_data, DateTime date_data, double[] init, F2Float F) : base(str_data, date_data)
        {
            int length = init.Length;
            notes = new double[length];
            measures_1 = new double[length];
            measures_2 = new double[length];
            for (int i = 0; i < length; ++i)
            {
                notes[i] = init[i];
                double[] res = F(notes[i]);
                measures_1[i] = res[0];
                measures_2[i] = res[1];
            }
        }
        public override double MaxAbsValue
        {
            get
            {
                double max = 0.0;
                for (int i = 0; i < notes.Length; ++i)
                {
                    if (max < Math.Abs(measures_1[i]))
                    {
                        max = Math.Abs(measures_1[i]);
                    }
                    if (max < Math.Abs(measures_2[i]))
                    {
                        max = Math.Abs(measures_2[i]);
                    }
                }
                return max;
            }
        }
        public override string ToString()
        {
            string str = "object_type = V4DataNUGrid\n";
            str += base.ToString();
            return str;
        }
        public override string ToLongString(string format)
        {
            string str = this.ToString();
            for (int i = 0; i < notes.Length; ++i)
            {
                str += $"x = {notes[i].ToString(format)} f1 = {measures_1[i].ToString(format)} f2 = {measures_2[i].ToString(format)}\n";
            }
            return str;
        }
        public override IEnumerator<DataItem> GetEnumerator()
        {
            int i = 0;
            for (; i < notes.Length; ++i)
            {
                DataItem dt = new DataItem(notes[i], measures_1[i], measures_2[i]);
                yield return dt;
            }
        }
    }
}

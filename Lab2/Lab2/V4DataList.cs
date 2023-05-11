using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class V4DataList: V4Data
    {
        public List<DataItem> DataList { get; set; }
        public V4DataList(string str_data, DateTime date_data): base(str_data, date_data)
        {
            DataList = new List<DataItem>();//capacity емкость начальная задается
        }
        public bool Add(double x, double v1, double v2)
        {
            if(DataList.Exists(y => y.x == x))
            {
                return false;
            }
            DataList.Add(new DataItem(x, v1, v2));
            return true;
        }
        public int AddDefaults(int nItems, F2Float F)
        {
            for(int i = 0; i < nItems; i++)
            {
                double x = (double)i * (double)i / 17.0 + (double)i / 5.5 + 2;
                double[] y = F(x);
                DataList.Add(new DataItem(x, y[0], y[1]));
            }
            return 1;
        }
        public override double MaxAbsValue
        {
            get
            {
                double max = 0.0;
                for(int i = 0; i < DataList.Count; ++i)
                {
                    if(max < Math.Abs(DataList[i].field[0]))
                    {
                        max = Math.Abs(DataList[i].field[0]);
                    }
                    if (max < Math.Abs(DataList[i].field[1]))
                    {
                        max = Math.Abs(DataList[i].field[1]);
                    }
                }
                return max;
            }
        }
        public override string ToString()
        {
            string str = "object_type = V4DataList\n";
            str += base.ToString();
            str += $"count = {DataList.Count}\n";
            return str;
        }
        public override string ToLongString(string format)
        {
            string str = this.ToString();
            for(int i = 0; i < DataList.Count; ++i)
            {
                str += $"{i} x = {DataList[i].x.ToString(format)} f1 = {DataList[i].field[0].ToString(format)} f2 = {DataList[i].field[1].ToString(format)}\n";
            }
            return str;
        }
        //IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        //{
            //return DataList.GetEnumerator();
        //}
        public override IEnumerator<DataItem> GetEnumerator()
        {
            return DataList.GetEnumerator();
        }
    }
}

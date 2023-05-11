using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace Lab3
{
    internal class V4DataCollection: System.Collections.ObjectModel.ObservableCollection<V4Data>
    {
        public new V4Data this[int index]
        {
            get => base[index];
        }
        public int? Count_zero {
            get{
                if (base.Count == 0){
                    return null;
                }
                var array_zeros = from item in this from dt in item  where (dt.field[0] == 0.0 || dt.field[1] == 0.0) select dt;
                int len = array_zeros.Count();
                if(len == -1)
                {
                    return -1;
                }
                else
                {
                    return len;
                }
            }
        }
        public IEnumerable<DataItem> request_ordered
        {
            get{
                if (base.Count == 0) { return null; }
                IEnumerable<DataItem> temp = from item in this from dt in item select dt;
                IEnumerable<DataItem> res = from item in temp orderby item.x descending select item;
                return res;
            }
        }
        public IEnumerable<double> request_in_datalist
        {
            get
            {
                if(base.Count == 0) { return null; }
                IEnumerable<V4Data> temp_datalist = from item in this where item is V4DataList select item;
                IEnumerable<V4Data> temp_datanugrid = from item in this where item is V4DataNUGrid select item;
                IEnumerable<DataItem> dt_datalist = from item in temp_datalist from dt in item select dt;
                IEnumerable<DataItem> dt_datanugrid = from item in temp_datanugrid from dt in item select dt;
                var unique_datalist = dt_datalist.GroupBy(item => item.x).Select(y=>y.First().x);
                var unique_datanugrid = dt_datanugrid.GroupBy(item => item.x).Select(y=>y.First().x);
                var res = from item1 in unique_datalist where (!unique_datanugrid.Contains(item1)) select item1;
                return res;
            }
        }
        public bool Contains(string ID)
        {
            for (int i = 0; i < base.Count; ++i)
            {
                if (String.Equals(base[i].str_data, ID))
                {
                    return true;
                }

            }
            return false;
        }
        public bool Remove(string ID)
        {
            for (int i = 0; i < base.Count; ++i)
            {
                if (String.Equals(base[i].str_data, ID))
                {
                    base.Remove(base[i]);
                    return true;
                }
            }
            return false;
        }
        public new bool Add(V4Data v4Data)
        {
            if (this.Contains(v4Data.str_data))
            {
                return false;
            }
            base.Add(v4Data);
            return true;
        }
        public void AddDefaults()
        {
            F2Float funct1 = new F2Float(FloatFunc.init1);
            F2Float funct2 = new F2Float(FloatFunc.init2);
            F2Float funct3 = new F2Float(FloatFunc.init3);
            V4DataList initlist_1 = new V4DataList("first", DateTime.Now);
            V4DataList initlist_2 = new V4DataList("second345", DateTime.Now);
            V4DataList initlist_3 = new V4DataList("lalala", DateTime.Now);
            initlist_1.AddDefaults(5, funct1);
            //initlist_2.AddDefaults(5, funct2);
            //initlist_3.AddDefaults(5, funct3);
            base.Add(initlist_1);
            base.Add(initlist_2);
            //base.Add(initlist_3);
            double[] arr = new double[4] { 2.0, 2.4, 5.7, 13.0};
            V4DataNUGrid initlist_4 = new V4DataNUGrid("forth", DateTime.Now, arr, funct2);
            V4DataNUGrid initlist_5 = new V4DataNUGrid("fifth_345", DateTime.Now);
            //V4DataNUGrid initlist_6 = new V4DataNUGrid("530000", DateTime.Now, arr, funct3);
            base.Add(initlist_4);
            base.Add(initlist_5);
            //base.Add(initlist_6);
            UniformGrid u = new UniformGrid(0, 4, 3);
            V4DataUGrid initlist_7 = new V4DataUGrid("seveth", DateTime.Now, u, funct1);
            V4DataUGrid initlist_8 = new V4DataUGrid("eightth_345", DateTime.Now);
            //V4DataUGrid initlist_9 = new V4DataUGrid("autmn", DateTime.Now, u, funct3);
            base.Add(initlist_7);
            base.Add(initlist_8);
            //base.Add(initlist_9);
        }

        public string ToLongString(string format)
        {
            string str = "";
            for(int i = 0; i < base.Count; ++i)
            {
                str += base[i].ToLongString(format) + '\n';
            }
            return str;
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < base.Count; ++i)
            {
                str += base[i].ToString() + '\n';
            }
            return str;
        }
    }
}

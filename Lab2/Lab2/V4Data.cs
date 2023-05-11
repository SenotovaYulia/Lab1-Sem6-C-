using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab3
{
    abstract class V4Data: IEnumerable<DataItem>
    {
        public string str_data { get; set;  }
        public DateTime date_data { get; set;  }
        public V4Data(string str_data, DateTime date_data){
            this.str_data = str_data;
            this.date_data = date_data;
        }
        abstract public double MaxAbsValue{ get; }
        abstract public string ToLongString(string format);
        public override string ToString()
        {
            return $"str_data = {str_data}\ndate_data = {date_data}\n";
        }

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

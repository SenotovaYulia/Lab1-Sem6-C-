using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.IO.BinaryWriter;
namespace Lab3
{
    class V4DataUGrid : V4Data
    {
        public UniformGrid parameters{ get; set; }
        public double[] measures_1 { get; set; }
        public double[] measures_2 { get; set; }
        public V4DataUGrid(string str_data, DateTime date_data) : base(str_data, date_data)
        {
            measures_1 = new double[0];
            measures_2 = new double[0];
        }
        public V4DataUGrid(string str_data, DateTime date_data, UniformGrid init, F2Float F) : base(str_data, date_data)
        {
            measures_1 = new double[init.note_number];
            measures_2 = new double[init.note_number];
            parameters = init;
            for(int i = 0; i < parameters.note_number; ++i)
            {
                double[] res = F(i * parameters.Step);
                measures_1[i] = res[0];
                measures_2[i] = res[1];
            }
        }
        public override double MaxAbsValue
        {
            get
            {
                double max = 0.0;
                for (int i = 0; i < parameters.note_number; ++i)
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
        public static explicit operator V4DataNUGrid(V4DataUGrid source)
        {
            V4DataNUGrid result = new V4DataNUGrid(source.str_data, source.date_data);
            result.notes = new double[source.parameters.note_number];
            result.measures_1 = new double[source.parameters.note_number];
            result.measures_2 = new double[source.parameters.note_number];
            for (int i = 0; i < source.parameters.note_number; ++i)
            {
                result.notes[i] = i * source.parameters.Step;
                result.measures_1[i] = source.measures_1[i];
                result.measures_2[i] = source.measures_2[i];
            }
            return result;
        }
        public override string ToString()
        {
            string str = "object_type = V4DataUGrid\n";
            str += base.ToString();
            str += parameters;
            return str;
        }
        public override string ToLongString(string format)
        {
            string str = "object_type = V4DataUGrid\n";
            str += base.ToString();
            str += parameters.ToLongString(format);
            for (int i = 0; i < parameters.note_number; ++i)
            {
                str += $"x = {(i * parameters.Step).ToString(format)} f1 = {measures_1[i].ToString(format)} f2 = {measures_2[i].ToString(format)}\n";
            }
            return str;
        }
        public override IEnumerator<DataItem> GetEnumerator()
        {
            int i = 0;
            for(; i < parameters.note_number; ++i)
            {
                DataItem dt = new DataItem(i * parameters.Step + parameters.coordinate_begin, measures_1[i], measures_2[i]);
                yield return dt;
            }
        }
        public bool Save(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter binaryWriter = new BinaryWriter(fs);

                binaryWriter.Write(base.str_data);
                binaryWriter.Write(base.date_data.Year);
                binaryWriter.Write(base.date_data.Month);
                binaryWriter.Write(base.date_data.Day);
                binaryWriter.Write(base.date_data.Hour);
                binaryWriter.Write(base.date_data.Minute);
                binaryWriter.Write(base.date_data.Second);

                binaryWriter.Write(parameters.coordinate_begin);
                binaryWriter.Write(parameters.coordinate_end);
                binaryWriter.Write(parameters.note_number);
                binaryWriter.Write(parameters.Step);
                for (int i = 0; i < parameters.note_number; ++i)
                {
                    //binaryWriter.Write((double)(i * parameters.Step));
                    //Console.WriteLine("a" + (double)(i * parameters.Step));
                    binaryWriter.Write(measures_1[i]);
                    //Console.WriteLine("b" + (double)measures_1[i]);
                    binaryWriter.Write((measures_2[i]));
                    //Console.WriteLine("v" + (double)measures_2[i]);
                }
                binaryWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        static public bool Load(string filename, ref V4DataUGrid V4)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                BinaryReader binaryReader = new BinaryReader(fs);
                V4.str_data = binaryReader.ReadString();
                V4.date_data = new DateTime(binaryReader.ReadInt32(), binaryReader.ReadInt32(), binaryReader.ReadInt32(),
                                            binaryReader.ReadInt32(), binaryReader.ReadInt32(), binaryReader.ReadInt32());
                double coord_begin = binaryReader.ReadDouble();
                double coord_end = binaryReader.ReadDouble();
                int note_number = binaryReader.ReadInt32();
                double step = binaryReader.ReadDouble();

                double[] measure1 = new double[note_number];
                double[] measure2 = new double[note_number];
                V4.parameters = new UniformGrid(coord_begin, coord_end, note_number);
                //Console.WriteLine("loadded:");
                for (int i = 0; i < note_number; ++i)
                {
                    //double note = binaryReader.ReadDouble();
                    measure1[i] = binaryReader.ReadDouble();
                    measure2[i] = binaryReader.ReadDouble();
                    //Console.WriteLine("note:" + note);
                    //Console.WriteLine("1:" + measure1[i]);
                    //Console.WriteLine("2:" + measure2[i]);
                }
                V4.measures_1 = measure1;
                V4.measures_2 = measure2;
                foreach (double item in measure1) Console.WriteLine(item);
                foreach (double item in measure2) Console.WriteLine(item);
                binaryReader.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }
}

//using Lab3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace ClassLibrary1
{
    public delegate double FRaw(double x);
    public enum FRawEnum { Linear, Cubic, Random };
    public class RawData
    {
        public double a { get; set; }
        public double b { get; set; }
        public int notes_number { get; set; }
        public bool is_uniform { get; set; }
        public FRaw function;
        public FRawEnum function_name;
        public double[] grid_notes { get; set; }
        public double[] grid_values { get; set; }
        public List<RawDataItem> rawdata_collection { get; set; }
        public RawData(double init_a, double init_b, int init_note_number, bool init_is_uni, FRawEnum init_function)
        {
            a = init_a;
            b = init_b;
            notes_number = init_note_number;
            is_uniform = init_is_uni;
            function_name = init_function;
            if(function_name == FRawEnum.Linear)
            {
                function = linear;
            }else if(function_name == FRawEnum.Cubic)
            {
                function = cubic;
            }else if(function_name == FRawEnum.Random)
            {
                function = random_function;
            }
            grid_notes = new double[notes_number];
            grid_values = new double[notes_number];
            if (is_uniform)
            {
                double step = (b - a) / (notes_number - 1);
                for (int i = 0; i < notes_number; i++)
                {
                    grid_notes[i] = step * i + a;
                    grid_values[i] = function(grid_notes[i]);
                    //Console.Write("In rawdata constructor:");
                    //Console.Write(grid_notes[i]);
                    //Console.WriteLine(grid_values[i]);
                }
            }
            else
            {
                double step = 1.0 / (notes_number - 1);
                Console.WriteLine($"step: {step}");
                for (int i = 0; i < notes_number; i++)
                {
                    double x = i * step;
                    Console.WriteLine($"x: {x}");
                    grid_notes[i] = a + (b - a) * x * x;
                    grid_values[i] = function(grid_notes[i]);
                }
                /*double x = i * step;
                        grid_notes[i] = x * x * (b - a) + a;
                        grid_values[i] = function(grid_notes[i]);
                        RawDataItem rawdataitem = new RawDataItem(grid_notes[i], grid_values[i]);
                        rawdata_collection.Add(rawdataitem);*/
            }
            rawdata_collection = new List<RawDataItem>();
            for (int i = 0; i < notes_number; ++i)
            {
                RawDataItem rawdataitem = new RawDataItem(grid_notes[i], grid_values[i]);
                rawdata_collection.Add(rawdataitem);
            }
        }
        public RawData(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                BinaryReader binaryReader = new BinaryReader(fs);
                a = binaryReader.ReadDouble();
                b = binaryReader.ReadDouble();
                notes_number = binaryReader.ReadInt32();
                is_uniform = binaryReader.ReadBoolean();
                int temp = binaryReader.ReadInt32();
                function_name = (FRawEnum)temp;
                if (function_name == FRawEnum.Linear)
                {
                    function = linear;
                }
                else if (function_name == FRawEnum.Cubic)
                {
                    function = cubic;
                }
                else if (function_name == FRawEnum.Random)
                {
                    function = random_function;
                }
                binaryReader.Close();
                rawdata_collection = new List<RawDataItem>();
                /*for (int i = 0; i < notes_number; ++i)
                {
                    RawDataItem rawdataitem = new RawDataItem(grid_notes[i], grid_values[i]);
                    rawdata_collection.Add(rawdataitem);
                }*/
                if (is_uniform)
                {
                    double step = (b - a) / (notes_number - 1);
                    for (int i = 0; i < notes_number; ++i)
                    {
                        grid_notes[i] = step * i + a;
                        grid_values[i] = function(grid_notes[i]);
                        RawDataItem rawdataitem = new RawDataItem(grid_notes[i], grid_values[i]);
                        rawdata_collection.Add(rawdataitem);
                    }
                }
                else
                {
                    double step = 1 / (notes_number - 1);
                    for (int i = 0; i < notes_number; ++i)
                    {
                        double x = i * step;
                        grid_notes[i] = x * x * (b - a) + a;
                        grid_values[i] = function(grid_notes[i]);
                        RawDataItem rawdataitem = new RawDataItem(grid_notes[i], grid_values[i]);
                        rawdata_collection.Add(rawdataitem);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        public static double linear(double x)
        {
            //return 5 * x - 7.5;
            return x;
        }
        public static double cubic(double x)
        {
            //return x * x * x + 2 * x * x - 3 * x;
            return x * x * (x - 1);
        }
        public static double random_function(double x)
        {
            var rand = new Random();
            return rand.NextDouble();
        }
        public void Save(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter binaryWriter = new BinaryWriter(fs);
                binaryWriter.Write(a);
                binaryWriter.Write(b);
                binaryWriter.Write(notes_number);
                binaryWriter.Write(is_uniform);
                int temp = (int)function_name;
                binaryWriter.Write(temp);
                for(int i = 0; i < notes_number; ++i)
                {
                    binaryWriter.Write(grid_notes[i]);
                    binaryWriter.Write(grid_values[i]);
                }
                binaryWriter.Close();
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
        public static void Load(string filename, ref RawData rawData)
        {
            FileStream fs = null;
            //rawData.a = 1;
            //rawData = new RawData(0.0, 1.0, 5, true, FRawEnum.Linear);
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                BinaryReader binaryReader = new BinaryReader(fs);
                rawData.a = binaryReader.ReadDouble();
                rawData.b = binaryReader.ReadDouble();
                rawData.notes_number = binaryReader.ReadInt32();
                rawData.is_uniform = binaryReader.ReadBoolean();
                int init_function_name = binaryReader.ReadInt32();
                rawData.grid_notes = new double[rawData.notes_number];
                rawData.grid_values = new double[rawData.notes_number];
                rawData.rawdata_collection = new List<RawDataItem>();
                for (int i = 0; i < rawData.notes_number; ++i)
                {
                    rawData.grid_notes[i] = binaryReader.ReadDouble();
                    rawData.grid_values[i] = binaryReader.ReadDouble();
                    RawDataItem rawdataitem = new RawDataItem(rawData.grid_notes[i], rawData.grid_values[i]);
                    rawData.rawdata_collection.Add(rawdataitem);
                }
                binaryReader.Close();
                rawData.function_name = (FRawEnum)init_function_name;
                //FRaw linear_function = linear;
                //rawData = new RawData(init_a, init_b, init_note_number, init_is_uniform, frawenum);
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            finally
            {
                if (fs != null) fs.Close();
            }
        } 
    }
}
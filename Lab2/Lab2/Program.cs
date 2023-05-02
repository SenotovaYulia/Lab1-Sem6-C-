using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using ClassLibrary1;
namespace Lab3
{
    delegate double[] F2Float(double x);
    class Program
    {
        static void Main(string[] args)
        {
            /*
            try
            {
                double raw_a = 1.8;
                double raw_b = 6.0;
                int raw_notes_number = 7;
                bool raw_is_uniform = true;
                ClassLibrary1.FRawEnum raw_selected_function = ClassLibrary1.FRawEnum.Linear;
                int spline_note_number = 10;
                double spline_left_der = 5.0;
                double spline_right_der = 5.0;
                ClassLibrary1.RawData rawdata = new ClassLibrary1.RawData(raw_a, raw_b, raw_notes_number, raw_is_uniform, raw_selected_function);
                ClassLibrary1.SplineData splinedata = new ClassLibrary1.SplineData(rawdata, spline_left_der, spline_right_der, spline_note_number);
                splinedata.Execute_Spline();
                Console.WriteLine("Значения RawData");
                for (int i = 0; i < raw_notes_number; ++i)
                {
                    Console.Write(rawdata.grid_notes[i]);
                    Console.Write(" ");
                    Console.WriteLine(rawdata.grid_values[i]);
                }
                Console.WriteLine("Значения");
                
                int length = splinedata.spline_collection.Count;
                for (int i = 0; i < length; ++i)
                {
                    double coord = splinedata.spline_collection[i].coordinate;
                    double diff1 = splinedata.spline_collection[i].value;
                    //double diff2 = Spline2.new_measures2[i];
                    Console.Write(coord);// + ' ' + diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff1);
                }
                Console.WriteLine("\nПервые производные");
                for (int i = 0; i < length; ++i)
                {
                    double diff1 = splinedata.spline_collection[i].first_der;
                    //double diff2 = splinedata.new_derivatives2[i];
                    Console.WriteLine(diff1);
                    //Console.Write(" ");
                    //Console.WriteLine(diff2);
                }
                Console.WriteLine("\nИнтеграл");
                double int3 = splinedata.integral;
                //double int4 = Spline2.integral2;
                Console.WriteLine(int3);
                //Console.Write(" ");
                //Console.WriteLine(int4);
                
                /*Console.WriteLine("\nРазница значений");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_measures1[i] - Spline2.new_measures1[i];
                    double diff2 = Spline1.new_measures2[i] - Spline2.new_measures2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nРазница первых производных");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_derivatives1[i] - Spline2.new_derivatives1[i];
                    double diff2 = Spline1.new_derivatives2[i] - Spline2.new_derivatives2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nРазница интегралов");
                double diff_int1 = Spline1.integral1 - Spline2.integral1;
                double diff_int2 = Spline1.integral2 - Spline2.integral2;
                Console.Write(diff_int1);
                Console.Write(" ");
                Console.WriteLine(diff_int2); */
                
                /*F2Float funct2 = new F2Float(FloatFunc.init2);
                double[] arr = new double[4] { 2.0, 2.4, 5.7, 13.0 };
                V4DataNUGrid nugrid = new V4DataNUGrid("zero", DateTime.Now, arr, funct2);
                double[] new_arr = new double[6] { 2.0, 2.4, 3.0, 5.7, 8.0, 13.0 };
                V4DataNUGridSpline Spline1 = new V4DataNUGridSpline(ref nugrid, 0.0, 507.0, new_arr, 1.0, 5.0);
                Spline1.spline_function();
                bool is_saved = Spline1.Save("testing.txt", "F3");
                /////////////////////////////////////////////////// 
                
                V4DataNUGridSpline Spline2 = new V4DataNUGridSpline(ref nugrid, 0.0, 200.0, new_arr, 1.0, 5.0);
                Spline2.spline_function();
                Console.WriteLine("ПЕРВЫЙ:");
                Console.WriteLine("Значения");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_measures1[i];
                    double diff2 = Spline1.new_measures2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nПервые производные");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_derivatives1[i];
                    double diff2 = Spline1.new_derivatives2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nИнтеграл");
                double int1 = Spline1.integral1;
                double int2 = Spline1.integral2;
                Console.Write(int1);
                Console.Write(" ");
                Console.WriteLine(int2);

                Console.WriteLine("\nВТОРОЙ:");
                Console.WriteLine("Значения");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline2.new_measures1[i];
                    double diff2 = Spline2.new_measures2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nПервые производные");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline2.new_derivatives1[i];
                    double diff2 = Spline2.new_derivatives2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nИнтеграл");
                double int3 = Spline2.integral1;
                double int4 = Spline2.integral2;
                Console.Write(int3);
                Console.Write(" ");
                Console.WriteLine(int4);
                
                Console.WriteLine("\nРазница значений");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_measures1[i] - Spline2.new_measures1[i];
                    double diff2 = Spline1.new_measures2[i] - Spline2.new_measures2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nРазница первых производных");
                for (int i = 0; i < new_arr.Length; ++i)
                {
                    double diff1 = Spline1.new_derivatives1[i] - Spline2.new_derivatives1[i];
                    double diff2 = Spline1.new_derivatives2[i] - Spline2.new_derivatives2[i];
                    Console.Write(diff1);
                    Console.Write(" ");
                    Console.WriteLine(diff2);
                }
                Console.WriteLine("\nРазница интегралов");
                double diff_int1 = Spline1.integral1 - Spline2.integral1;
                double diff_int2 = Spline1.integral2 - Spline2.integral2;
                Console.Write(diff_int1);
                Console.Write(" ");
                Console.WriteLine(diff_int2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }
        /*[DllImport("C:\\Users\\user\\Documents\\C#_projects\\Lab1 Sem2 copy Lab3\\x64\\Debug\\Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GlobalFunction(ref int ret, int note_number, double[] notes, double[] measures, double[] derivatives,
int new_note_number, double[] new_grid, double[] new_values, double[] left_integ, double[] right_integ, double[] integrals,
double[] spline_coeff);*/
    }
}

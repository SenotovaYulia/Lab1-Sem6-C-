using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SplineData
    {
        public RawData rawdata { get; set; }
        public int uniform_grid_number { get; set; }
        public List<SplineDataItem> spline_collection { get; set; }
        public double left_der { get; set; }
        public double right_der { get; set; }
        public SplineData(RawData init_raw_data, double left_second_der, double right_second_der, int init_uniform_notes_number)
        {
            rawdata = init_raw_data;
            uniform_grid_number = init_uniform_notes_number;
            left_der = left_second_der;
            right_der = right_second_der;
        }
        public void Execute_Spline()
        {
            /*int ret = 1;
            int note_number = rawdata.notes_number;
            double[] measures = new double[note_number];
            for (int i = 0; i < note_number; ++i)
            {
                measures[i] = rawdata.grid_values[i];
            }
            double[] derivatives = new double[2] { left_der, right_der };
            double[] new_values = new double[3 * uniform_grid_number];
            double[] arr_integral = new double[1];
            arr_integral[0] = 0.0;
            double[] spline_coeff = new double[4 * (note_number - 1)];
            double[] new_grid = new double[uniform_grid_number];
            for(int i = 0; i < uniform_grid_number; ++i)
            {
                new_grid[i] = i * (rawdata.b - rawdata.a) / uniform_grid_number;
            }
            GlobalFunction(ref ret, note_number, rawdata.grid_notes, rawdata.grid_values, derivatives, uniform_grid_number,
                new_grid, new_values, new double[1] { left_der }, new double[1] { right_der }, arr_integral, spline_coeff);
            if (ret == 0)
            {
                //this.new_measures1 = new double[new_grid.Length];
                //this.new_measures2 = new double[new_grid.Length];
                //this.new_derivatives1 = new double[new_grid.Length];
                //this.new_derivatives2 = new double[new_grid.Length];
                spline_collection = new List<SplineDataItem>();
                for (int i = 0; i < uniform_grid_number; i++)
                {
                    double temp_note = new_grid[i];
                    double temp_value = new_values[3 * i];
                    double temp_first_der = new_values[3 * i + 1];
                    double temp_second_der = new_values[3 * i + 2];
                    Console.WriteLine($"{i}:" + temp_value + " " + temp_first_der + " " + temp_second_der + "\n");
                    spline_collection.Add(new SplineDataItem(temp_note, temp_value, temp_first_der, temp_second_der));
                }
                integral = arr_integral[0];
            }*/

            int ret = 1;
            int note_number = rawdata.notes_number;
            double[] measures = new double[2 * note_number];
            for (int i = 0; i < note_number; ++i)
            {
                measures[i] = rawdata.grid_values[i];
                measures[i + note_number] = 0.0;
            }
            double[] derivatives = new double[2] { left_der, right_der };
            double[] new_values = new double[3 * 2 * uniform_grid_number];
            double[] integrals = new double[2];

            double[] new_grid = new double[uniform_grid_number];
            for (int i = 0; i < uniform_grid_number; ++i)
            {
                new_grid[i] = i * (rawdata.b - rawdata.a) / (uniform_grid_number - 1) + rawdata.a;
            }
            double[] spline_coeff = new double[4 * 2 * (note_number - 1)];
            GlobalFunction(ref ret, note_number, rawdata.grid_notes, measures, derivatives, uniform_grid_number, new_grid, new_values,
                new double[1] { rawdata.a }, new double[1] { rawdata.b }, integrals, spline_coeff);
            if (ret == 0)
            {
                spline_collection = new List<SplineDataItem>();
                for (int i = 0; i < new_grid.Length; i++)
                {
                    double new_measures1 = new_values[3 * i];
                    double new_measures2 = new_values[3 * i + 3 * new_grid.Length];
                    double new_derivatives1 = new_values[3 * i + 1];
                    double new_derivatives2 = new_values[3 * i + 3 * new_grid.Length + 1];
                    double new_second_der1 = new_values[3 * i + 2];
                    double new_second_der2 = new_values[3 * i + new_grid.Length + 2];
                    spline_collection.Add(new SplineDataItem(new_grid[i], new_measures1, new_derivatives1, new_second_der1));
                }
                integral = integrals[0];
                Console.WriteLine("Integrals:");
                Console.WriteLine(integrals[0].ToString(), ' ', integrals[1]);//this.integral2 = integrals[1];
            }
            /*
             derivatives, this.new_grid.Length, this.new_grid, new_values, new double[1] { left_border }, new double[1] { right_border }, integrals, spline_coeff);
            if (ret == 0)
            {
                this.new_measures1 = new double[new_grid.Length];
                this.new_measures2 = new double[new_grid.Length];
                this.new_derivatives1 = new double[new_grid.Length];
                this.new_derivatives2 = new double[new_grid.Length];
                for (int i = 0; i < new_grid.Length; i++)
                {
                    this.new_measures1[i] = new_values[3 * i];
                    this.new_measures2[i] = new_values[3 * i + 3 * new_grid.Length];
                    this.new_derivatives1[i] = new_values[3 * i + 1];
                    this.new_derivatives2[i] = new_values[3 * i + 3 * new_grid.Length + 1];
                }
                this.integral1 = integrals[0];
                this.integral2 = integrals[1];
            }*/
        }
        public double integral { get; set; }
        [DllImport("C:\\Users\\user\\Documents\\C#_projects\\Solution1\\x64\\Debug\\Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GlobalFunction(ref int ret, int note_number, double[] notes, double[] measures, double[] derivatives,
    int new_note_number, double[] new_grid, double[] new_values, double[] left_integ, double[] right_integ, double[] integrals,
    double[] spline_coeff);
    }
}

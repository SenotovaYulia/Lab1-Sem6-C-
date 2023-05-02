using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class V4DataNUGridSpline
    {
        public V4DataNUGrid grid { get; private set; }
        public double right_end_deriv { get; private set; }
        public double left_end_deriv { get; private set; }
        public double[] new_grid { get; private set; }
        public double[] new_measures1 { get; private set; }
        public double[] new_measures2 { get; private set; }
        public double[] new_derivatives1 { get; private set; }
        public double[] new_derivatives2 { get; private set; }
        public double left_border { get; private set; }
        public double right_border { get; private set; }
        public double integral1 { get; private set; }
        public double integral2 { get; private set; }
        public V4DataNUGridSpline(ref V4DataNUGrid init_grid, double init_left_der, double init_right_der, double[] init_new_grid, double init_l_border, double init_r_border)
        {
            grid = init_grid;
            right_end_deriv = init_right_der;
            left_end_deriv = init_left_der;
            int length = init_new_grid.Length;
            new_grid = new double[length];
            for (int i = 0; i < length; ++i)
            {
                new_grid[i] = init_new_grid[i];
            }
            left_border = init_l_border;
            right_border = init_r_border;
        }
        public void spline_function()
        {
            int ret = 1;
            int note_number = grid.notes.Length;
            double[] measures = new double[2 * note_number];
            for(int i = 0; i < note_number; ++i)
            {
                measures[i] = grid.measures_1[i];
                measures[i + note_number] = grid.measures_2[i];
            }
            double[] derivatives = new double[2] { left_end_deriv, right_end_deriv };
            double[] new_values = new double[3 * 2 * new_grid.Length];
            double[] integrals = new double[2];
            double[] spline_coeff = new double[4 * 2 * (note_number - 1)];
            GlobalFunction(ref ret, note_number, this.grid.notes, measures, derivatives, this.new_grid.Length, this.new_grid, new_values, new double[1] {left_border}, new double[1] { right_border}, integrals, spline_coeff);
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
            }
        }
        public bool Save(string filename, string format)
        {
            string str = this.ToLongString(format);
            StreamWriter fs = null;
            try {
                fs = new StreamWriter(filename);
                fs.WriteLine(str);
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }
        public string ToLongString(string format)
        {
            string result = grid.ToLongString(format) + '\n';
            result += "Первая производная в концах:" + left_end_deriv + ' ' + right_end_deriv + '\n';
            result += "Границы для вычисления интеграла:" + left_border + ' ' + right_border + '\n';
            result += "Интеграл:" + integral1 + " +  i * " + integral2 + '\n';
            result += "Сетка значений:\n";
            for (int i = 0; i < new_grid.Length; ++i)
            {
                result += new_grid[i] + " " + new_measures1[i] + " " + new_measures2[i] + " ";
                result += new_derivatives1[i] + " " + new_derivatives2[i] + "\n";
            }
            return result;
        }
        [DllImport("C:\\Users\\user\\Documents\\C#_projects\\Solution1\\x64\\Debug\\Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GlobalFunction(ref int ret, int note_number, double[] notes, double[] measures, double[] derivatives,
int new_note_number, double[] new_grid, double[] new_values, double[] left_integ, double[] right_integ, double[] integrals,
double[] spline_coeff);
    }
}

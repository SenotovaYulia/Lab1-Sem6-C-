using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ClassLibrary1;

namespace WpfApp1
{
    internal class ViewData: INotifyPropertyChanged, IDataErrorInfo
    {
        public double raw_a { get; set; }
        public double raw_b { get; set; }
        public int raw_notes_number { get; set; }
        public bool raw_is_uniform { get; set; }
        public ClassLibrary1.FRawEnum raw_selected_function { get; set; }
        public ClassLibrary1.FRaw? fraw { get; set; }
        public int spline_note_number { get; set; }
        public double spline_left_der { get; set; }
        public double spline_right_der { get; set; }
        private RawData rawData;// { get; set; }
        public RawData rawData_event
        {
            get
            {
                return rawData;
            }
            set
            {
                rawData = value;
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RawData.rawdata_collection"));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private SplineData splineData { get; set; }
        public SplineData splineData_event
        {
            get
            {
                return splineData;
            }
            set
            {
                splineData = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SplineData.splinedata_collection"));
                }
            }
        }
        public MyOxyPlotModel oxyPlotModel { get; set; }
        public void Save(string filename)
        {
            try
            {
                rawData.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Load(string filename)//, ref RawData new_rawData)
        {
            try
            {
                RawData.Load(filename, ref rawData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ViewData()
        {
            raw_a = 1.8;
            raw_b = 6.0;
            raw_notes_number = 7;
            raw_is_uniform = true;
            raw_selected_function = ClassLibrary1.FRawEnum.Linear;
            spline_note_number = 10;
            spline_left_der = 0.0;
            spline_right_der = 0.0;
            //InitRawData();
            //InitSplineData();
        }
        public void InitRawData()
        {
            if (raw_selected_function == FRawEnum.Linear)
                fraw = RawData.linear;
            else if (raw_selected_function == FRawEnum.Cubic)
                fraw = RawData.cubic;
            else
                fraw = RawData.random_function;
            rawData = new RawData(raw_a, raw_b, raw_notes_number, raw_is_uniform, raw_selected_function);
        }
        public void InitSplineData()
        {
            splineData = new SplineData(rawData, spline_left_der, spline_right_der, spline_note_number);
        }
        public void CreateSpline()
        {
            try
            {
                splineData.Execute_Spline();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "raw_notes_number":
                        if (raw_notes_number < 2)
                        {
                            error = "Количество узлов, в которых задана функция, должно быть не меньше 2";
                        }
                        break;
                    case "spline_note_number":
                        if(spline_note_number < 2)
                        {
                            error = "Количество узлов, в которых вычисляется сплайн, должно быть не меньше 2";
                        }
                        //Обработка ошибок для свойства Name
                        break;
                    case "raw_b":
                        if (raw_a >= raw_b)
                        {
                            error = "Левый конец должен юыть меньше правого конца";
                        }

                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class DoubleArrConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string? first_der = value[0].ToString();
                string? second_der = value[1].ToString();
                if (first_der == null || second_der == null)
                {
                    throw new Exception();
                }
                return first_der + ";" + second_der;
            }
            catch (Exception) { return ";"; }
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string? str = value as string;
                if (str == null)
                {
                    throw new Exception();
                }
                string[] s = str.Split(";", StringSplitOptions.RemoveEmptyEntries);
                return new object[] { System.Convert.ToDouble(s[0]), System.Convert.ToDouble(s[1]) };
            }
            catch (Exception) { return new object[2]; }
        }
    }
}

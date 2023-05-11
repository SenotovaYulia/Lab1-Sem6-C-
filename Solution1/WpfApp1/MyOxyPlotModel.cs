using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Legends;
using System.Windows;
//using System.Windows.Forms.DataVisualization.Charting.Chart; 
namespace WpfApp1
{
    internal class MyOxyPlotModel
    {
        SplineData data;
        public PlotModel plotModel { get; private set; }
        public MyOxyPlotModel(SplineData data)
        {
            this.data = data;
            plotModel = new PlotModel { Title = "Cubic Spline" };
            OxyColor color = OxyColors.BlueViolet;
            LineSeries lineSeries = new LineSeries();
            Func<double, double> Fn1 = (x) => x;
            Func<double, double> Fn2 = (x) => x * x * (x - 1);
            Func<double, double>[] Fns = new Func<double, double>[data.uniform_grid_number - 1];
            //for (int i = 0; i < data.rawdata.notes_number; i++)
            //lineSeries.Points.Add(new DataPoint(data.rawdata.grid_notes[0], data.rawdata.grid_values[0]));
            lineSeries.MarkerType = MarkerType.Cross;
            lineSeries.Color = OxyColors.Green;
            lineSeries.MarkerSize = 5;
            lineSeries.MarkerStroke = color;
            lineSeries.MarkerFill = color;
            lineSeries.Title = "RawData grid";

            Legend legend = new Legend();
            plotModel.Legends.Add(legend);
            if (data.rawdata.function_name == ClassLibrary1.FRawEnum.Linear)
            {
                plotModel.Series.Add(new FunctionSeries(Fn1, data.rawdata.grid_notes[0], data.rawdata.grid_notes[data.rawdata.notes_number - 1],
                    0.0001));

            }
            if (data.rawdata.function_name == ClassLibrary1.FRawEnum.Cubic)
            {
                List<DataPoint> datapoint = new List<DataPoint>();
                for (int i = 0; i < data.rawdata.notes_number - 1; ++i)
                {
                    datapoint.Add(new DataPoint(data.rawdata.grid_notes[i], data.rawdata.grid_values[i]));
                }
                FunctionSeries series = new FunctionSeries(Fn2, data.rawdata.grid_notes[0], data.rawdata.grid_notes[data.rawdata.notes_number - 1],
                    0.0001);
                series.Color = OxyColors.Green;
                plotModel.Series.Add(series);
            }
            else
            {
                for (int i = 0; i < data.rawdata.notes_number; i++)
                    lineSeries.Points.Add(new DataPoint(data.rawdata.grid_notes[i], data.rawdata.grid_values[i]));
                //plotModel.Legends.Add(legend);
                this.plotModel.Series.Add(lineSeries);
            }
            LineSeries lineSeries2 = new LineSeries();
            for (int j = 0; j < data.uniform_grid_number; j++)
            {
                lineSeries2.Points.Add(new DataPoint(data.spline_collection[j].coordinate, data.spline_collection[j].value));
                //MessageBox.Show(data.coeff.Length.ToString());
                //int j = 0;
                /*double x1 = data.spline_collection[j].coordinate;
                double d = data.coeff[4 * j + 3] / 6;
                double c = data.spline_collection[j].second_der / 2 - 3 * d * x1;
                double b = data.spline_collection[j].first_der - 3 * d * x1 * x1 - 2 * c * x1;
                double a = data.spline_collection[j].value - d * x1 * x1 * x1 - c * x1 * x1 - b * x1;
                Fns[j] = (x) => d * x * x * x + c * x * x + b * x + a;
                MessageBox.Show(d.ToString() + ' ' + c.ToString() + ' ' + b.ToString() + ' ' + a.ToString());
                FunctionSeries spline_series = new FunctionSeries(Fns[j], data.rawdata.rawdata_collection[j].coordinate,
                    data.rawdata.rawdata_collection[j + 1].coordinate, 0.0001);
                spline_series.Color = OxyColors.Blue;
                plotModel.Series.Add(spline_series);*/
            }
            lineSeries2.Color = OxyColors.Blue;

            lineSeries2.MarkerType = MarkerType.Circle;
            lineSeries2.MarkerSize = 4;
            lineSeries2.MarkerStroke = OxyColors.Red;
            lineSeries2.MarkerFill = OxyColors.Red;
            lineSeries2.Title = "Spline function";

            legend = new Legend();
            plotModel.Legends.Add(legend);
            plotModel.Series.Add(lineSeries2);
        }
    }
}

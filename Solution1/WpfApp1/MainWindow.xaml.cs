using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;
using Microsoft.Win32;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewData viewData = new ViewData();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewData;
            function_list.ItemsSource = Enum.GetValues(typeof(ClassLibrary1.FRawEnum));
        }
        private void From_Controls_Click(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                viewData.InitRawData();
                viewData.InitSplineData();
                viewData.CreateSpline();
                viewData.oxyPlotModel = new MyOxyPlotModel(viewData.splineData_event);
                //this.DataContext = oxyPlotModel;

                //button_draw.IsEnabled = false;
                DataContext = null;
                DataContext = viewData;
                //for (int i = 0; i < view )
                //MessageBox.Show(viewData.splineData_event.spline_collection[0].ToString() + ' ' +
                    /*viewData.splineData_event.spline_collection[0].first_der.ToString() + ' ' +
                    viewData.splineData_event.spline_collection[0].second_der.ToString() + ' ' +
                    viewData.splineData_event.spline_collection[1].ToString() + ' ' +*/
                    //viewData.splineData_event.spline_collection[1].ToString() + ' '
                    /*viewData.splineData_event.coeff[5].ToString() + ' ' + 
                    viewData.splineData_event.coeff[6].ToString() + ' ' + viewData.splineData_event.coeff[7]);*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Correct_From_File(object sender, CanExecuteRoutedEventArgs e)
        {
            //viewData.InitSplineData();
            //viewData.CreateSpline();
            try
            {
                /*string checking;
                bool can_execute = true;

                checking = viewData["raw_notes_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["raw_b"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["spline_note_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                if (can_execute)*/
                    e.CanExecute = true;
                /*else
                    e.CanExecute = false;*/
            }
            catch (Exception ex)
            {
                e.CanExecute = false;
            }
        }

        private void From_File_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                viewData.Load(openFileDialog.FileName);
            }
            string checking;
            bool can_execute = true;

            checking = viewData["raw_notes_number"];
            can_execute = checking.Length == 0 ? can_execute : false;

            checking = viewData["raw_b"];
            can_execute = checking.Length == 0 ? can_execute : false;

            checking = viewData["spline_note_number"];
            can_execute = checking.Length == 0 ? can_execute : false;

            if (viewData.rawData_event == null)
            {
                can_execute = false;
            }
            if (can_execute){
                viewData.InitSplineData();
                viewData.CreateSpline();
                viewData.oxyPlotModel = new MyOxyPlotModel(viewData.splineData_event);
                DataContext = null;
                DataContext = viewData;
            }
        }

        private void Save_Click(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); 
            if (saveFileDialog.ShowDialog() == false)
                return;
            string filename = saveFileDialog.FileName;
            viewData.Save(filename);
            MessageBox.Show("Файл сохранен");
        }
        
        private void Correct_RawData(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                string checking;
                bool can_execute = true;

                checking = viewData["raw_notes_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["raw_b"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["spline_note_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                if (viewData.rawData_event == null)
                {
                    can_execute = false;
                }

                if (can_execute)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            catch (Exception ex)
            {
                e.CanExecute = false;
            }
        }

        public void Correct_SplineData(object sender, CanExecuteRoutedEventArgs e)
        {
            try
            {
                string checking;
                bool can_execute = true;

                checking = viewData["raw_notes_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["raw_b"];
                can_execute = checking.Length == 0 ? can_execute : false;

                checking = viewData["spline_note_number"];
                can_execute = checking.Length == 0 ? can_execute : false;

                /*if (viewData.rawData_event == null)
                {
                    can_execute = false;
                }*/

                if (can_execute)
                    e.CanExecute = true;
                else
                    e.CanExecute = false;
            }
            catch (Exception ex)
            {
                e.CanExecute = false;
            }
        }
    }
}
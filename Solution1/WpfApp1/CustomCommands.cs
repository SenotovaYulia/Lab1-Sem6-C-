using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public class CustomCommands
    {
        public static RoutedCommand FromControlsCommand =
                new RoutedCommand("RawData from Controls", typeof(CustomCommands));
        public static RoutedCommand FromFileCommand =
                new RoutedCommand("RawData from File", typeof(CustomCommands));
        public static RoutedCommand SaveCommand =
                new RoutedCommand("RawData from File", typeof(CustomCommands));
    }
}

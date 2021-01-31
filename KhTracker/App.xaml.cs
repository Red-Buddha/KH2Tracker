using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Log logger;

        App()
        {
            logger = new Log("test.txt");
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            logger.Close();
        }
    }
}

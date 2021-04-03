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
            try
            {
                logger = new Log(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\KhTracker\\log.txt");
            }
            catch
            { };
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            if (App.logger != null)
                logger.Close();
        }
    }
}

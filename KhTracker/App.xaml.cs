using System;
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
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
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

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            (MainWindow as MainWindow).Save("KhTrackerAutoSaves\\" + "Tracker-CrashBackup_" + DateTime.Now.ToString("yy-MM-dd_H-m") + ".txt");
        }
    }
}

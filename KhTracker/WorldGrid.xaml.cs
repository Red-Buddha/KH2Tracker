using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for WorldGrid.xaml
    /// </summary>
    public partial class WorldGrid : UniformGrid
    {
        public WorldGrid()
        {
            InitializeComponent();
        }

        public void HandleWorldGrid(Item button, bool add)
        {
            if (add)
                Children.Add(button);
            else
                Children.Remove(button);

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);

            Rows = gridnum;
            Height = Rows * 40;
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            UniformGrid grid = sender as UniformGrid;

            Item button = e.Data.GetData(typeof(Item)) as Item;

            bool isreport = false;

            // item is a report
            if (data.hintsLoaded && (int)button.GetValue(Grid.RowProperty) == 0)
            {
                int index = (int)button.GetValue(Grid.ColumnProperty);

                // out of report attempts
                if (data.reportAttempts[index] == 0)
                    return;

                // check for correct report location
                if (data.reportLocations[index].Replace(" ", "") == grid.Name.Substring(0, grid.Name.Length - 4))
                {
                    // hint text and resetting fail icons
                    window.SetHintText(data.reportInformation[index].Item1 + " has " + data.reportInformation[index].Item2 + " important checks.");
                    data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
                    data.reportAttempts[index] = 3;
                    isreport = true;

                    // auto update world immportant check number
                    for (int i = 0; i < data.Hints.Count; ++i)
                    {
                        if (data.Worlds[i].Name == data.reportInformation[index].Item1.Replace(" ", ""))
                            window.SetReportValue(data.Hints[i], data.reportInformation[index].Item2 + 1);
                    }
                }
                else
                {
                    // update fail icons when location is report location is wrong
                    data.reportAttempts[index]--;
                    if (data.reportAttempts[index] == 0)
                        data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail3");
                    else if (data.reportAttempts[index] == 1)
                        data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail2");
                    else if (data.reportAttempts[index] == 2)
                        data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail1");

                    return;
                }
            }

            // remove hint text when tracking a non-report
            if (isreport == false)
                window.SetHintText("");

            // move item to world
            window.ItemPool.Children.Remove(button);
            grid.Children.Add(button);

            // update collection count
            window.IncrementCollected();

            // update mouse actions
            button.MouseDoubleClick -= button.Item_Click;
            button.MouseDown += button.Item_Return;
            button.MouseMove -= button.Item_MouseMove;

            if (isreport)
            {
                button.MouseEnter += button.Report_Hover;
            }
        }
    }
}

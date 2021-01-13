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

        public void Handle_WorldGrid(Item button, bool add)
        {
            if (add)
            {
                try
                {
                    Children.Add(button);
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
                Children.Remove(button);

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);

            Rows = gridnum;

            // default 1, add .5 for every row
            double length = 1 + ((Children.Count - 1) / 5) / 2.0;
            ((Parent as Grid).Parent as Grid).RowDefinitions[(int)Parent.GetValue(Grid.RowProperty)].Height = new GridLength(length, GridUnitType.Star);
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            if (e.Data.GetDataPresent(typeof(Item)))
            {

                Item item = e.Data.GetData(typeof(Item)) as Item;

                if (Handle_Report(item, window, data))
                    Add_Item(item, window);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                window.LoadHints(files[0]);
            }
        }

        public void Add_Item(Item item, MainWindow window)
        {
            // move item to world
            window.ItemPool.Children.Remove(item);
            Handle_WorldGrid(item, true);

            // update collection count
            window.IncrementCollected();

            // update mouse actions
            if (MainWindow.data.dragDrop)
            {
                item.MouseDoubleClick -= item.Item_Click;
                item.MouseMove -= item.Item_MouseMove;
            }
            else
            {
                item.MouseDown -= item.Item_MouseDown;
                item.MouseUp -= item.Item_MouseUp;
            }
            item.MouseDown -= item.Item_Return;
            item.MouseDown += item.Item_Return;

            item.DragDropEventFire(item.Name, Name.Remove(Name.Length - 4, 4), true);
        }

        public bool Handle_Report(Item item, MainWindow window, Data data)
        {
            bool isreport = false;

            // item is a report
            if (data.hintsLoaded && (int)item.GetValue(Grid.RowProperty) == 0)
            {
                int index = (int)item.GetValue(Grid.ColumnProperty);

                // out of report attempts
                if (data.reportAttempts[index] == 0)
                    return false;

                // check for correct report location
                if (data.reportLocations[index].Replace(" ", "") == Name.Substring(0, Name.Length - 4))
                {
                    // hint text and resetting fail icons
                    window.SetHintText(data.reportInformation[index].Item1 + " has " + data.reportInformation[index].Item2 + " important checks");
                    data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
                    data.reportAttempts[index] = 3;
                    isreport = true;
                    item.DragDropEventFire(data.reportInformation[index].Item1.Replace(" ", String.Empty), data.reportInformation[index].Item2);

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

                    return false;
                }
            }

            if (isreport == false)
            {
                // remove hint text when tracking a non-report
                window.SetHintText("");
            }
            else
            {
                item.MouseEnter -= item.Report_Hover;
                item.MouseEnter += item.Report_Hover;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for Draggable.xaml
    /// </summary>
    public partial class Item : ContentControl
    {
        public Item()
        {
            InitializeComponent();
        }
        
        //Adorner subclass specific to this control
        private class ItemAdorner : Adorner
        {
            Rect renderRect;
            ImageSource imageSource;
            public Point CenterOffset;
            public ItemAdorner(Item adornedElement) : base(adornedElement)
            {
                renderRect = new Rect(adornedElement.RenderSize);
                this.IsHitTestVisible = false;
                imageSource = ((adornedElement).Content as Image).Source;
                CenterOffset = new Point(-renderRect.Width / 2, -renderRect.Height / 2);
            }
            protected override void OnRender(DrawingContext drawingContext)
            {
                drawingContext.DrawImage(imageSource, renderRect);
            }
        }

        //Struct to use in the GetCursorPos function
        private struct PInPoint
        {
            public int X;
            public int Y;
            public PInPoint(int x, int y)
            {
                X = x; Y = y;
            }
            public PInPoint(double x, double y)
            {
                X = (int)x; Y = (int)y;
            }
            public Point GetPoint(double xOffset = 0, double yOffet = 0)
            {
                return new Point(X + xOffset, Y + yOffet);
            }
            public Point GetPoint(Point offset)
            {
                return new Point(X + offset.X, Y + offset.Y);
            }
        }

        [DllImport("user32.dll")]
        static extern void GetCursorPos(ref PInPoint p);

        private ItemAdorner myAdornment;
        private PInPoint pointRef = new PInPoint();

        public void Item_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var adLayer = AdornerLayer.GetAdornerLayer(this);
                myAdornment = new ItemAdorner(this);
                adLayer.Add(myAdornment);
                DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
                adLayer.Remove(myAdornment);
            }
        }

        public void Item_Click(object sender, RoutedEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);

            if (data.selected != null)
            {
                bool isreport = false;

                // item is a report
                if (data.hintsLoaded && (int)GetValue(Grid.RowProperty) == 0)
                {
                    int index = (int)GetValue(Grid.ColumnProperty);

                    if (data.reportAttempts[index] == 0)
                        return;

                    if (data.reportLocations[index].Replace(" ", "") == data.selected.Name)
                    {
                        window.SetHintText(data.reportInformation[index].Item1 + " has " + data.reportInformation[index].Item2 + " important checks.");
                        data.ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail0");
                        data.reportAttempts[index] = 3;
                        isreport = true;

                        for (int i = 0; i < data.Hints.Count; ++i)
                        {
                            if (data.Worlds[i].Name == data.reportInformation[index].Item1.Replace(" ", ""))
                                ((MainWindow)Application.Current.MainWindow).SetReportValue(data.Hints[i], data.reportInformation[index].Item2 + 1);
                        }
                    }
                    else
                    {
                        data.reportAttempts[index]--;
                        if (data.reportAttempts[index] == 0)
                            data.ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail3");
                        else if (data.reportAttempts[index] == 1)
                            data.ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail2");
                        else if (data.reportAttempts[index] == 2)
                            data.ReportAttemptVisual[index].SetResourceReference(ContentProperty, "Fail1");

                        return;
                    }
                }

                if (isreport == false)
                    window.SetHintText("");

                ((MainWindow)Application.Current.MainWindow).ItemPool.Children.Remove(this);

                for (int i = 0; i < data.Worlds.Count; ++i)
                {
                    if (data.selected == data.Worlds[i])
                    {
                        data.Grids[i].HandleWorldGrid(this, true);
                        break;
                    }
                }

                ((MainWindow)Application.Current.MainWindow).IncrementCollected();

                MouseDoubleClick -= Item_Click;
                MouseDown += Item_Return;

                MouseMove -= Item_MouseMove;

                if (isreport)
                {
                    MouseEnter += Report_Hover;
                }
            }
        }

        public void Report_Hover(object sender, RoutedEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            int index = (int)GetValue(Grid.ColumnProperty);

            window.SetHintText(data.reportInformation[index].Item1 + " has " + data.reportInformation[index].Item2 + " important checks.");
        }

        public void Item_Return(object sender, RoutedEventArgs e)
        {
            HandleItemReturn();
        }

        public void HandleItemReturn()
        {
            Data data = MainWindow.data;
            if (Parent != ((MainWindow)Application.Current.MainWindow).ItemPool)
            {
                ((WorldGrid)Parent).HandleWorldGrid(this, false);

                ((MainWindow)Application.Current.MainWindow).ItemPool.Children.Add(this);

                ((MainWindow)Application.Current.MainWindow).DecrementCollected();

                MouseDown -= Item_Return;

                MouseDoubleClick += Item_Click;
                MouseMove += Item_MouseMove;

                MouseEnter -= Report_Hover;
            }
        }

        private void Item_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            GetCursorPos(ref pointRef);
            Point relPos = this.PointFromScreen(pointRef.GetPoint(myAdornment.CenterOffset));
            myAdornment.Arrange(new Rect(relPos, myAdornment.DesiredSize));

            Mouse.SetCursor(Cursors.None);
            e.Handled = true;
        }
    }
}
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
        bool selected = false;

        public delegate void TotalHandler(string world, int checks);
        public delegate void FoundHandler(string item, string world, bool add);

        public event TotalHandler UpdateTotal;
        public event FoundHandler UpdateFound;
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
                renderRect = new Rect(adornedElement.DesiredSize);
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
                if (data.mode == Mode.DAHints)
                {
                    if (data.WorldsData[data.selected.Name].worldGrid.Handle_PointReport(this, window, data))
                    {
                        data.WorldsData[data.selected.Name].worldGrid.Add_Item(this, window);
                    }
                }
                else
                {
                    if (data.WorldsData[data.selected.Name].worldGrid.Handle_Report(this, window, data))
                    {
                        data.WorldsData[data.selected.Name].worldGrid.Add_Item(this, window);
                    }
                }
            }
        }

        public void Report_Hover(object sender, RoutedEventArgs e)
        {

            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            int index = (int)GetValue(Grid.ColumnProperty);

            if (data.mode == Mode.DAHints)
                window.SetHintText(Codes.GetHintTextName(data.pointreportInformation[index].Item1) + " has " + data.pointreportInformation[index].Item2);
            else
                window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + data.reportInformation[index].Item2 + " important checks");
        }

        public void DragDropEventFire(string item, string world, bool add)
        {
            UpdateFound(item, world, add);
        }

        public void DragDropEventFire(string world, int value)
        {
            UpdateTotal(world, value);
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
                WorldGrid parent = this.Parent as WorldGrid;

                ((WorldGrid)Parent).Handle_WorldGrid(this, false);

                ((MainWindow)Application.Current.MainWindow).ItemPool.Children.Add(this);

                ((MainWindow)Application.Current.MainWindow).DecrementCollected();

                MouseDown -= Item_Return;

                if (data.dragDrop)
                {
                    MouseDoubleClick -= Item_Click;
                    MouseDoubleClick += Item_Click;
                    MouseMove -= Item_MouseMove;
                    MouseMove += Item_MouseMove;
                }
                else
                {
                    MouseDown -= Item_MouseDown;
                    MouseDown += Item_MouseDown;
                    MouseUp -= Item_MouseUp;
                    MouseUp += Item_MouseUp;
                }

                MouseEnter -= Report_Hover;

                UpdateFound(this.Name, parent.Name.Remove(parent.Name.Length - 4, 4), false);
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

        public void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selected = true;
        }

        public void Item_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (selected)
                Item_Click(sender, e);
        }
    }
}
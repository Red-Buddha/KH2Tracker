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
        MainWindow MainW = (MainWindow)App.Current.MainWindow;

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

        public void Item_Click(object sender, RoutedEventArgs e)
        {
            Data data = MainWindow.data;
            if (data.selected != null && data.WorldsData[data.selected.Name].worldGrid.ReportHandler(this))
            {
                data.WorldsData[data.selected.Name].worldGrid.Add_Item(this);
            }
        }

        public void Item_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (selected)
                Item_Click(sender, e);
        }

        public void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selected = true;
        }

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
 
        public void Report_Hover(object sender, RoutedEventArgs e)
        {
            Data data = MainWindow.data;
            int index = (int)GetValue(Grid.ColumnProperty);
            var repStr1 = data.reportInformation[index].Item1;
            var repStr2 = data.reportInformation[index].Item2;
            var repInt = data.reportInformation[index].Item3;

            switch (data.mode)
            {
                case Mode.PointsHints:
                    MainW.SetHintText(Codes.GetHintTextName(repStr1), "has", Codes.FindShortName(Codes.GetHintTextName(repStr2)), true, false, true);
                    break;
                case Mode.PathHints:
                    if (data.UsingProgressionHints)
                        return;
                    MainW.SetHintText(Codes.GetHintTextName(repStr1));
                    break;
                case Mode.SpoilerHints:
                    if (repStr1 == "Empty")
                    {
                        if (data.UsingProgressionHints)
                            return;
                        MainW.SetHintText("This report looks too faded to read...");
                    }
                    else
                    {
                        if (repInt == -1)
                        {
                            if (data.UsingProgressionHints)
                                return;
                            MainW.SetHintText(Codes.GetHintTextName(repStr1), "has no Important Checks", "", true, false, false);
                        }
                        else if (repInt == -12345)
                        {
                            MainW.SetHintText(data.reportInformation[index].Item1, "", "", false, false, false);
                        }
                        else
                        {
                            if (data.UsingProgressionHints)
                                return;
                            MainW.SetHintText(Codes.GetHintTextName(repStr1), "has been revealed!", "", true, false, false);
                        }
                    }
                    break;
                default:
                    if (repInt == -99)
                    {
                        //MainW.SetJokeText(repStr1);
                    }
                    else
                    {
                        if (data.UsingProgressionHints)
                            return;
                        MainW.SetHintText(Codes.GetHintTextName(repStr2), "has", repInt + " important checks", true, false, true);
                    }
                    break;
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

        public void Item_Return(object sender, RoutedEventArgs e)
        {
            HandleItemReturn();
        }

        public void HandleItemReturn()
        {
            Data data = MainWindow.data;

            if (this.Name.StartsWith("Ghost_"))
            {
                Grid GhostRow = VisualTreeHelper.GetChild(MainW.ItemPool, 4) as Grid; //ghost grid always at this position
                if (Parent != GhostRow)
                {
                    WorldGrid parent = this.Parent as WorldGrid;
                    ((WorldGrid)Parent).Handle_WorldGrid(this, false);

                    GhostRow.Children.Add(this);
                    parent.Children.Remove(this);
                }
                return;
            }

            //int index = data.Items.IndexOf(this);
            //Grid ItemRow = data.ItemsGrid[index];
            Grid ItemRow = data.Items[this.Name].Item2;

            if (Parent != ItemRow)
            {
                WorldGrid parent = this.Parent as WorldGrid;

                ((WorldGrid)Parent).Handle_WorldGrid(this, false);

                ItemRow.Children.Add(this);

                MainW.SetCollected(false);

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
            }
        }
    }
}
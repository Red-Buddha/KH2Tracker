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
            if (data.selected != null && data.WorldsData[data.selected.Name].worldGrid.ReportHandler(this))
            {
                data.WorldsData[data.selected.Name].worldGrid.Add_Item(this);
            }
        }

        public void Report_Hover(object sender, RoutedEventArgs e)
        {
            Data data = MainWindow.data;
            int index = (int)GetValue(Grid.ColumnProperty);

            if (data.mode == Mode.DAHints)
            {
                if (shortenNames.ContainsKey(data.reportInformation[index].Item2))
                {
                    MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + shortenNames[data.reportInformation[index].Item2]);
                }
                else
                    MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + data.reportInformation[index].Item2);
            }
            else if (data.mode == Mode.PathHints)
            {
                MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1));
            }
            else if (data.mode == Mode.SpoilerHints)
            {
                if (data.reportInformation[index].Item1 == "Empty")
                {
                    MainW.SetHintText("This report looks too faded to read...");
                }
                else
                {
                    if (data.reportInformation[index].Item3 == -1)
                    {
                        MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has no Important Checks");
                    }
                    else
                    {
                        MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has been revealed!");
                    }
                }
            }
            else if(data.reportInformation[index].Item3 == -99)
            {
                //MainW.SetJokeText(data.reportInformation[index].Item1);
            }
            else
            {
                MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + data.reportInformation[index].Item2 + " important checks");
            }
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

                if (!this.Name.StartsWith("Ghost_"))
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


        ///TODO: remove these and use ones from Codes.cs
        private Dictionary<string, string> shortenNames = new Dictionary<string, string>()
        {
            {"Baseball Charm (Chicken Little)", "Baseball Charm"},
            {"Lamp Charm (Genie)", "Lamp Charm"},
            {"Ukulele Charm (Stitch)", "Ukulele Charm"},
            {"Feather Charm (Peter Pan)", "Feather Charm"},
            {"PromiseCharm", "Promise Charm"},
            {"Battlefields of War (Auron)", "Battlefields of War"},
            {"Sword of the Ancestor (Mulan)", "Sword of the Ancestor"},
            {"Beast's Claw (Beast)", "Beast's Claw"},
            {"Bone Fist (Jack Skellington)", "Bone Fist"},
            {"Proud Fang (Simba)", "Proud Fang"},
            {"Skill and Crossbones (Jack Sparrow)", "Skill and Crossbones"},
            {"Scimitar (Aladdin)", "Scimitar"},
            {"Identity Disk (Tron)", "Identity Disk"}
        };
    }
}
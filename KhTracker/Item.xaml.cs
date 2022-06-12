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

        public MainWindow MainW = (MainWindow)App.Current.MainWindow;

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
                else if (data.mode == Mode.PathHints)
                {
                    if (data.WorldsData[data.selected.Name].worldGrid.Handle_PathReport(this, window, data))
                    {
                        data.WorldsData[data.selected.Name].worldGrid.Add_Item(this, window);
                    }
                }
                else if (data.mode == Mode.SpoilerHints)
                {
                    if (data.WorldsData[data.selected.Name].worldGrid.Handle_SpoilerReport(this, window, data))
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
            int index = (int)GetValue(Grid.ColumnProperty);

            if (data.mode == Mode.DAHints)
            {
                if (shortenNames.ContainsKey(data.pointreportInformation[index].Item2))
                {
                    MainW.SetHintText(Codes.GetHintTextName(data.pointreportInformation[index].Item1) + " has " + shortenNames[data.pointreportInformation[index].Item2]);
                }
                else
                    MainW.SetHintText(Codes.GetHintTextName(data.pointreportInformation[index].Item1) + " has " + data.pointreportInformation[index].Item2);
            }
            else if (data.mode == Mode.PathHints)
            {
                MainW.SetHintText(Codes.GetHintTextName(data.pathreportInformation[index].Item1));
            }
            else if (data.mode == Mode.SpoilerHints)
            {
                if (data.reportInformation[index].Item1 == "Empty")
                {
                    MainW.SetHintText("This report looks too faded to read...");
                }
                else
                {
                    if (data.reportInformation[index].Item2 == -1)
                    {
                        MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has no Important Checks");
                    }
                    else
                    {
                        MainW.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has been revealed!");
                    }
                }
            }
            else if(data.reportInformation[index].Item2 == -99)
            {
                MainW.SetJokeText(data.reportInformation[index].Item1);
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
            Grid ItemRow = VisualTreeHelper.GetChild(MainW.ItemPool, GetItemPool[this.Name]) as Grid;

            if (this.Name.StartsWith("Ghost_"))
            {
                if (Parent != ItemRow)
                {
                    WorldGrid parent = this.Parent as WorldGrid;
                    ((WorldGrid)Parent).Handle_WorldGrid(this, false);

                    ItemRow.Children.Add(this);
                    parent.Children.Remove(this);
                }
                return;
            }

            if (Parent != ItemRow)
            {
                WorldGrid parent = this.Parent as WorldGrid;

                ((WorldGrid)Parent).Handle_WorldGrid(this, false);

                ItemRow.Children.Add(this);

                MainW.DecrementCollected();

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

        private Dictionary<string, int> GetItemPool = new Dictionary<string, int>()
        {
            {"Report1", 0},
            {"Report2", 0},
            {"Report3", 0},
            {"Report4", 0},
            {"Report5", 0},
            {"Report6", 0},
            {"Report7", 0},
            {"Report8", 0},
            {"Report9", 0},
            {"Report10", 0},
            {"Report11", 0},
            {"Report12", 0},
            {"Report13", 0},
            {"Fire1", 1},
            {"Fire2", 1},
            {"Fire3", 1},
            {"Blizzard1", 1},
            {"Blizzard2", 1},
            {"Blizzard3", 1},
            {"Thunder1", 1},
            {"Thunder2", 1},
            {"Thunder3", 1},
            {"Cure1", 1},
            {"Cure2", 1},
            {"Cure3", 1},
            {"HadesCup", 1},
            {"OlympusStone", 1},
            {"Reflect1", 2},
            {"Reflect2", 2},
            {"Reflect3", 2},
            {"Magnet1", 2},
            {"Magnet2", 2},
            {"Magnet3", 2},
            {"Valor", 2},
            {"Wisdom", 2},
            {"Limit", 2},
            {"Master", 2},
            {"Final", 2},
            {"Anti", 2},
            {"OnceMore", 2},
            {"SecondChance", 2},
            {"UnknownDisk", 3},
            {"TornPage1", 3},
            {"TornPage2", 3},
            {"TornPage3", 3},
            {"TornPage4", 3},
            {"TornPage5", 3},
            {"Baseball", 3},
            {"Lamp", 3},
            {"Ukulele", 3},
            {"Feather", 3},
            {"Connection", 3},
            {"Nonexistence", 3},
            {"Peace", 3},
            {"PromiseCharm", 3},
            {"BeastWep", 4},
            {"JackWep", 4},
            {"SimbaWep", 4},
            {"AuronWep", 4},
            {"MulanWep", 4},
            {"SparrowWep", 4},
            {"AladdinWep", 4},
            {"TronWep", 4},
            {"MembershipCard", 4},
            {"Picture", 4},
            {"IceCream", 4},
            {"Ghost_Report1", 5},
            {"Ghost_Report2", 5},
            {"Ghost_Report3", 5},
            {"Ghost_Report4", 5},
            {"Ghost_Report5", 5},
            {"Ghost_Report6", 5},
            {"Ghost_Report7", 5},
            {"Ghost_Report8", 5},
            {"Ghost_Report9", 5},
            {"Ghost_Report10", 5},
            {"Ghost_Report11", 5},
            {"Ghost_Report12", 5},
            {"Ghost_Report13", 5},
            {"Ghost_Fire1", 6},
            {"Ghost_Fire2", 6},
            {"Ghost_Fire3", 6},
            {"Ghost_Blizzard1", 6},
            {"Ghost_Blizzard2", 6},
            {"Ghost_Blizzard3", 6},
            {"Ghost_Thunder1", 6},
            {"Ghost_Thunder2", 6},
            {"Ghost_Thunder3", 6},
            {"Ghost_Cure1", 6},
            {"Ghost_Cure2", 6},
            {"Ghost_Cure3", 6},
            {"Ghost_HadesCup", 6},
            {"Ghost_OlympusStone", 6},
            {"Ghost_Reflect1", 7},
            {"Ghost_Reflect2", 7},
            {"Ghost_Reflect3", 7},
            {"Ghost_Magnet1", 7},
            {"Ghost_Magnet2", 7},
            {"Ghost_Magnet3", 7},
            {"Ghost_Valor", 7},
            {"Ghost_Wisdom", 7},
            {"Ghost_Limit", 7},
            {"Ghost_Master", 7},
            {"Ghost_Final", 7},
            {"Ghost_Anti", 7},
            {"Ghost_OnceMore", 7},
            {"Ghost_SecondChance", 7},
            {"Ghost_UnknownDisk", 8},
            {"Ghost_TornPage1", 8},
            {"Ghost_TornPage2", 8},
            {"Ghost_TornPage3", 8},
            {"Ghost_TornPage4", 8},
            {"Ghost_TornPage5", 8},
            {"Ghost_Baseball", 8},
            {"Ghost_Lamp", 8},
            {"Ghost_Ukulele", 8},
            {"Ghost_Feather", 8},
            {"Ghost_Connection", 8},
            {"Ghost_Nonexistence", 8},
            {"Ghost_Peace", 8},
            {"Ghost_PromiseCharm", 8},
            {"Ghost_BeastWep", 9},
            {"Ghost_JackWep", 9},
            {"Ghost_SimbaWep", 9},
            {"Ghost_AuronWep", 9},
            {"Ghost_MulanWep", 9},
            {"Ghost_SparrowWep", 9},
            {"Ghost_AladdinWep", 9},
            {"Ghost_TronWep", 9},
            {"Ghost_MembershipCard", 9},
            {"Ghost_Picture", 9},
            {"Ghost_IceCream", 9}
        };

    }
}
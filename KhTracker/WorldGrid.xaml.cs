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
        //public static int localLevelCount = 0;
        public static int Ghost_Fire = 0;
        public static int Ghost_Blizzard = 0;
        public static int Ghost_Thunder = 0;
        public static int Ghost_Cure = 0;
        public static int Ghost_Reflect = 0;
        public static int Ghost_Magnet = 0;
        public static int Ghost_Pages = 0;

        //amount of obtained ghost magic/pages
        public static int Ghost_Fire_obtained = 0;
        public static int Ghost_Blizzard_obtained = 0;
        public static int Ghost_Thunder_obtained = 0;
        public static int Ghost_Cure_obtained = 0;
        public static int Ghost_Reflect_obtained = 0;
        public static int Ghost_Magnet_obtained = 0;
        public static int Ghost_Pages_obtained = 0;

        //A single spot to have referenced for the opacity of the ghost checks idk where to put this
        public static double universalOpacity = 0.5;

        public WorldGrid()
        {
            InitializeComponent();
        }

        public void Handle_WorldGrid(Item button, bool add)
        {
            int addRemove = 1;

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
            {
                //Console.WriteLine("Removing " + button.Name);
                Children.Remove(button);
                addRemove = -1;
            }

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);

            Rows = gridnum;

            // default 1, add .5 for every row
            double length = 1 + ((Children.Count - 1) / 5) / 2.0;
            var outerGrid = ((Parent as Grid).Parent as Grid);
            int row = (int)Parent.GetValue(Grid.RowProperty);
            outerGrid.RowDefinitions[row].Height = new GridLength(length, GridUnitType.Star);

            if (MainWindow.data.mode == Mode.AltHints || MainWindow.data.mode == Mode.OpenKHAltHints)
            {
                WorldComplete();

                string worldName = Name.Substring(0, Name.Length - 4);
                if (MainWindow.data.WorldsData[worldName].hint != null)
                {
                    Image hint = MainWindow.data.WorldsData[worldName].hint;
                    ((MainWindow)App.Current.MainWindow).SetReportValue(hint, Children.Count + 1);
                }
            }

            if (MainWindow.data.mode == Mode.DAHints)
            {
                if (button.Name.StartsWith("Ghost_"))
                {
                    //we use this modified code to set world point values
                    string worldName = Name.Substring(0, Name.Length - 4);
                    CheckWorldGhost(worldName);

                    Console.WriteLine(worldName + " added/removed " + (TableReturn(button.Name) * addRemove));

                    Image hint = MainWindow.data.WorldsData[worldName].hint;
                    ((MainWindow)App.Current.MainWindow).SetPoints(worldName, ((MainWindow)App.Current.MainWindow).GetPoints(worldName) - (TableReturn(button.Name) * addRemove));
                    ((MainWindow)App.Current.MainWindow).SetReportValue(hint, ((MainWindow)App.Current.MainWindow).GetPoints(worldName) + 1);

                }
                else
                {
                    string worldName = Name.Substring(0, Name.Length - 4);
                    WorldPointsComplete();

                    Console.WriteLine("real " + worldName + " added/removed " + (TableReturn(button.Name) * addRemove));

                    Image hint = MainWindow.data.WorldsData[worldName].hint;

                    ((MainWindow)App.Current.MainWindow).SetPoints(worldName, ((MainWindow)App.Current.MainWindow).GetPoints(worldName) - (TableReturn(button.Name) * addRemove));
                    ((MainWindow)App.Current.MainWindow).SetReportValue(hint, ((MainWindow)App.Current.MainWindow).GetPoints(worldName) + 1);

                    //this creates a dictionary of items each world has tracked.
                    if (worldName != "GoA")
                    {
                        var templist = new List<string>();
                        templist.Add(button.Name);
                        if (add)
                        {
                            if (!Data.WorldItems.Keys.Contains(worldName))
                            {
                                Data.WorldItems.Add(worldName, templist);
                            }
                            else
                            {
                                Data.WorldItems[worldName].Add(button.Name);
                            }

                            Remove_Ghost(worldName, button);
                        }
                        else
                        {
                            if (Data.WorldItems[worldName].Contains(button.Name))
                                Data.WorldItems[worldName].Remove(button.Name);
                            else
                                Console.WriteLine("something went wrong removing item from list");
                        }
                    }

                    if (worldName == "GoA")
                        return;
                    else
                        ((MainWindow)App.Current.MainWindow).UpdatePointScore(TableReturn(button.Name) * addRemove);
                }
            }
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            if (e.Data.GetDataPresent(typeof(Item)))
            {
                if (data.mode == Mode.DAHints)
                {
                    Item item = e.Data.GetData(typeof(Item)) as Item;

                    if (Handle_PointReport(item, window, data))
                        Add_Item(item, window);

                }
                else
                {
                    Item item = e.Data.GetData(typeof(Item)) as Item;

                    if (Handle_Report(item, window, data))
                        Add_Item(item, window);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.Path.GetExtension(files[0]).ToUpper() == ".TXT")
                    window.LoadHints(files[0]);
                else if (System.IO.Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                    window.ParseSeed(files[0]);
            }
        }

        public void Add_Item(Item item, MainWindow window)
        {
            // move item to world
            window.ItemPool.Children.Remove(item);
            Handle_WorldGrid(item, true);

            //Reset any obtained item to be normal transparency
            item.Opacity = 1.0;

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

            SetItemPoolGhosts(window);
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
                if (data.reportLocations[index] == Name.Substring(0, Name.Length - 4))
                {
                    // hint text and resetting fail icons
                    window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + data.reportInformation[index].Item2 + " important checks");
                    data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
                    data.reportAttempts[index] = 3;
                    isreport = true;
                    item.DragDropEventFire(data.reportInformation[index].Item1, data.reportInformation[index].Item2);

                    // set world report hints to as hinted then checks if the report location was hinted to set if its a hinted hint
                    data.WorldsData[data.reportInformation[index].Item1].hinted = true;
                    if (data.WorldsData[data.reportLocations[index]].hinted == true)
                    {
                        data.WorldsData[data.reportInformation[index].Item1].hintedHint = true;
                    }

                    // loop through hinted world for reports to set their info as hinted hints
                    for (int i = 0; i < data.WorldsData[data.reportInformation[index].Item1].worldGrid.Children.Count; ++i)
                    {
                        Item gridItem = data.WorldsData[data.reportInformation[index].Item1].worldGrid.Children[i] as Item;
                        if (gridItem.Name.Contains("Report"))
                        {
                            int reportIndex = int.Parse(gridItem.Name.Substring(6)) - 1;
                            data.WorldsData[data.reportInformation[reportIndex].Item1].hintedHint = true;
                            window.SetReportValue(data.WorldsData[data.reportInformation[reportIndex].Item1].hint, data.reportInformation[reportIndex].Item2 + 1);
                        }
                    }

                    // auto update world important check number
                    window.SetReportValue(data.WorldsData[data.reportInformation[index].Item1].hint, data.reportInformation[index].Item2 + 1);
                }
                else
                {
                    // update fail icons when location is report location is wrong
                    AddFailIcon(index);
                    return false;
                }
            }

            if (isreport)
            {
                item.MouseEnter -= item.Report_Hover;
                item.MouseEnter += item.Report_Hover;
            }

            return true;
        }

        private void AddFailIcon(int index)
        {
            Data data = MainWindow.data;

            data.reportAttempts[index]--;
            if (data.reportAttempts[index] == 0)
                data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail3");
            else if (data.reportAttempts[index] == 1)
                data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail2");
            else if (data.reportAttempts[index] == 2)
                data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail1");
        }

        public void WorldComplete()
        {
            string worldName = Name.Substring(0, Name.Length - 4);
            if (worldName == "GoA" || MainWindow.data.WorldsData[worldName].complete == true)
                return;

            List<string> items = new List<string>();
            items.AddRange(MainWindow.data.WorldsData[Name.Substring(0, Name.Length - 4)].checkCount);

            foreach (var child in Children)
            {
                Item item = child as Item;
                char[] numbers = { '1', '2', '3', '4', '5' };
                if (items.Contains(item.Name.TrimEnd(numbers)))
                {
                    items.Remove(item.Name.TrimEnd(numbers));
                }
            }

            if (items.Count == 0)
            {
                MainWindow.data.WorldsData[Name.Substring(0, Name.Length - 4)].complete = true;
            }
        }

        public int TableReturn(string nameButton)
        {
            if (nameButton.StartsWith("Ghost_"))
            {
                if (((MainWindow)App.Current.MainWindow).GhostMathOption.IsChecked)
                {
                    if (GetItemType.Keys.Contains(nameButton))
                    {
                        if (MainWindow.data.PointsDatanew.Keys.Contains(GetItemType[nameButton]))
                            return MainWindow.data.PointsDatanew[GetItemType[nameButton]];
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                if (GetItemType.Keys.Contains(nameButton))
                {
                    if (MainWindow.data.PointsDatanew.Keys.Contains(GetItemType[nameButton]))
                        return MainWindow.data.PointsDatanew[GetItemType[nameButton]];
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        //points hints stuff
        public bool Handle_PointReport(Item item, MainWindow window, Data data)
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
                if (data.reportLocations[index] == Name.Substring(0, Name.Length - 4))
                {
                    // hint text and resetting fail icons
                    window.SetHintText(Codes.GetHintTextName(data.pointreportInformation[index].Item1) + " has " + data.pointreportInformation[index].Item2);
                    CheckGhost(data.pointreportInformation[index].Item1, data.pointreportInformation[index].Item2, window, data, "Report" + index);
                    data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
                    data.reportAttempts[index] = 3;
                    isreport = true;
                }
                else
                {
                    // update fail icons when location is report location is wrong
                    AddFailIcon(index);
                    return false;
                }
            }

            if (isreport)
            {
                item.MouseEnter -= item.Report_Hover;
                item.MouseEnter += item.Report_Hover;
            }

            return true;
        }

        public void WorldPointsComplete()
        {
            string worldName = Name.Substring(0, Name.Length - 4);
            if (worldName == "GoA" || MainWindow.data.WorldsData[worldName].complete == true)
            {
                return;
            }

            List<string> items = new List<string>();
            items.AddRange(MainWindow.data.WorldsData[Name.Substring(0, Name.Length - 4)].checkCount);

            foreach (var child in Children)
            {
                Item item = child as Item;
                char[] numbers = { '1', '2', '3', '4', '5' };
                Console.WriteLine(item.Name);

                if (item.Name.Contains("Report") || item.Name.StartsWith("Ghost_"))
                    items.Remove(item.Name);
                else if (items.Contains(item.Name.TrimEnd(numbers)))
                {
                    items.Remove(item.Name.TrimEnd(numbers));
                }
            }

            if (items.Count == 0)
            {
                MainWindow.data.WorldsData[Name.Substring(0, Name.Length - 4)].complete = true;
            }
        }

        public void CheckGhost(string world, string itemname, MainWindow window, Data data, string report)
        {
            //don't bother checking if ghost tracking is off
            if (((MainWindow)Application.Current.MainWindow).GhostItemOption.IsChecked == false)
                return;

            //check if report was tracked before in this session to avoid tracking multiple ghosts for removing and placing the same report back
            if (data.TrackedReports.Contains(report))
                return;
            else
            {
                data.TrackedReports.Add(report);
            }

            bool wasmulti = false;
            string NewName;
            string GhostName;
            List<string> Multi = new List<string>();

            //gotta be a better way
            //if an elemt or tapge then add variations to a list
            //we do this to check to see if any of the 3 levels of magic or any of
            //the torn pages were previously tracked to the hinted world
            if (itemname.Contains("Element"))
            {
                string[] itemnames;
                itemnames = itemname.Split(' ');
                Multi.Add(itemnames[0] + "1");
                Multi.Add(itemnames[0] + "2");
                Multi.Add(itemnames[0] + "3");
                NewName = itemnames[0];
                wasmulti = true;
            }
            else if (itemname.Contains("Torn"))
            {
                Multi.Add("TornPage1");
                Multi.Add("TornPage2");
                Multi.Add("TornPage3");
                Multi.Add("TornPage4");
                Multi.Add("TornPage5");
                NewName = "TornPage";
                wasmulti = true;
            }
            else
            {
                Multi.Add(convertItemNames[itemname]);
                NewName = convertItemNames[itemname];
            }

            //this shouldn't ever happen, but return without doing anything else if the ghost values for magic/pages are higher than expected
            switch (NewName)
            {
                case "Fire":
                    if (Ghost_Fire >= 3)
                    {
                        return;
                    }
                    break;
                case "Blizzard":
                    if (Ghost_Blizzard >= 3)
                    {
                        return;
                    }
                    break;
                case "Thunder":
                    if (Ghost_Thunder >= 3)
                    {
                        return;
                    }
                    break;
                case "Cure":
                    if (Ghost_Cure >= 3)
                    {
                        return;
                    }
                    break;
                case "Magnet":
                    if (Ghost_Magnet >= 3)
                    {
                        return;
                    }
                    break;
                case "Reflect":
                    if (Ghost_Reflect >= 3)
                    {
                        return;
                    }
                    break;
                case "TornPage1":
                    if (Ghost_Pages >= 5)
                    {
                        return;
                    }
                    break;
            }

            //first we need to check if the real item already exists in the world
            if (Data.WorldItems.Keys.Contains(world))
            {
                foreach (string name in Multi)
                {
                    if (Data.WorldItems[world].Contains(name))
                        return;
                }
            }

            //if it doesn't then we proceede to track the ghost item to the world
            //Console.WriteLine("attempting track");
            if (wasmulti)
            {
                //Console.WriteLine("was multi item");
                switch (NewName)
                {
                    case "Fire":
                        Ghost_Fire += 1;
                        NewName += Ghost_Fire;
                        break;
                    case "Blizzard":
                        Ghost_Blizzard += 1;
                        NewName += Ghost_Blizzard;
                        break;
                    case "Thunder":
                        Ghost_Thunder += 1;
                        NewName += Ghost_Thunder;
                        break;
                    case "Cure":
                        Ghost_Cure += 1;
                        NewName += Ghost_Cure;
                        break;
                    case "Reflect":
                        Ghost_Reflect += 1;
                        NewName += Ghost_Reflect;
                        break;
                    case "Magnet":
                        Ghost_Magnet += 1;
                        NewName += Ghost_Magnet;
                        break;
                    case "TornPage":
                        Ghost_Pages += 1;
                        NewName += Ghost_Pages;
                        break;
                    default:
                        Console.WriteLine("unknown multi item name! exiting ghost tracking");
                        return;
                }
            }

            //get ghost item name then track it
            GhostName = "Ghost_" + NewName;
            Item Ghostimage = Data.GhostItems[GhostName];
            data.WorldsData[world].worldGrid.Add_Ghost(Ghostimage, window);

            var templist = new List<string>{GhostName};
            if (!Data.WorldItems.Keys.Contains(world))
            {
                Data.WorldItems.Add(world, templist);
            }
            else
            {
                Data.WorldItems[world].Add(GhostName);
            }

            //don't bother messing with magic or pages
            if (!itemname.Contains("Fire") && !itemname.Contains("Blizzard") && !itemname.Contains("Thunder")
                && !itemname.Contains("Cure") && !itemname.Contains("Reflect") && !itemname.Contains("Magnet")
                && !itemname.Contains("Page"))
                window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(convertItemNames[itemname]))].Opacity = universalOpacity;
        }

        public void SetItemPoolGhosts(MainWindow window)
        {
            string checkName = "";
            //Magic
            //fires
            for (int i = 0, j = 0; i < 3 && j < Ghost_Fire - Ghost_Fire_obtained; i++)
            {
                checkName = "Fire" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
            //blizzards
            for (int i = 0, j = 0; i < 3 && j < Ghost_Blizzard - Ghost_Blizzard_obtained; i++)
            {
                checkName = "Blizzard" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
            //thunders
            for (int i = 0, j = 0; i < 3 && j < Ghost_Thunder - Ghost_Thunder_obtained; i++)
            {
                checkName = "Thunder" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
            //cures
            for (int i = 0, j = 0; i < 3 && j < Ghost_Cure - Ghost_Cure_obtained; i++)
            {
                checkName = "Cure" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
            //reflects
            for (int i = 0, j = 0; i < 3 && j < Ghost_Reflect - Ghost_Reflect_obtained; i++)
            {
                checkName = "Reflect" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
            //magnets
            for (int i = 0, j = 0; i < 3 && j < Ghost_Magnet - Ghost_Magnet_obtained; i++)
            {
                checkName = "Magnet" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }

            //Pages
            for (int i = 0, j = 0; i < 5 && j < Ghost_Pages - Ghost_Pages_obtained; i++)
            {
                checkName = "TornPage" + (i + 1).ToString();
                //if the item is not in the bottom item pool, skip changing it's opacity
                if (window.ItemPool.Children.Contains((Item)window.ItemPool.FindName(checkName)))
                {
                    j++;
                    window.ItemPool.Children[window.ItemPool.Children.IndexOf((Item)window.ItemPool.FindName(checkName))].Opacity = universalOpacity;
                }
            }
        }

        public void Add_Ghost(Item item, MainWindow window)
        {
            // move item to world
            if (((MainWindow)Application.Current.MainWindow).GhostItemOption.IsChecked)
            {
                window.ItemPool.Children.Remove(item);
                Handle_WorldGrid(item, true);
            }
        }

        public void Remove_Ghost(string world, Item item)
        {
            //check to see if ghost of item exists
            if (!MainWindow.data.WorldsData[world].containsGhost)
                return;

            string itemname = item.Name;
            char[] numbers = { '1', '2', '3', '4', '5' };
            bool wasmulti = false;
            List<string> Multi = new List<string>();

            if (GetItemType[item.Name] == "magic" || GetItemType[item.Name] == "page")
            {
                itemname = "Ghost_" + itemname.TrimEnd(numbers);
                Multi.Add(itemname + "1");
                Multi.Add(itemname + "2");
                Multi.Add(itemname + "3");
                Multi.Add(itemname + "4");
                Multi.Add(itemname + "5");
                wasmulti = true;
            }
            else
            {
                itemname = "Ghost_" + itemname;
            }

            if (Data.WorldItems.Keys.Contains(world))
            {
                if (wasmulti)
                {
                    foreach (string multiname in Multi)
                    {
                        if (Data.WorldItems[world].Contains(multiname))
                        {
                            if (itemname.Contains("Fire"))
                                Ghost_Fire_obtained++;
                            if (itemname.Contains("Blizzard"))
                                Ghost_Blizzard_obtained++;
                            if (itemname.Contains("Thunder"))
                                Ghost_Thunder_obtained++;
                            if (itemname.Contains("Cure"))
                                Ghost_Cure_obtained++;
                            if (itemname.Contains("Reflect"))
                                Ghost_Reflect_obtained++;
                            if (itemname.Contains("Magnet"))
                                Ghost_Magnet_obtained++;
                            if (itemname.Contains("Page"))
                                Ghost_Pages_obtained++;
                            Item Ghostitem = Data.GhostItems[multiname];
                            Handle_WorldGrid(Ghostitem, false);
                            return;
                        }
                    }
                }
                else
                {
                    if (Data.WorldItems[world].Contains(itemname))
                    {
                        Item Ghostitem = Data.GhostItems[itemname];
                        Handle_WorldGrid(Ghostitem, false);
                        return;
                    }
                }
            }


        }

        public void CheckWorldGhost(string worldName)
        {
            foreach (Item ghost in Data.GhostItems.Values.ToList())
            {
                if (Children.Contains(ghost))
                {
                    MainWindow.data.WorldsData[worldName].containsGhost = true;
                    return;
                }
                else
                {
                    MainWindow.data.WorldsData[worldName].containsGhost = false;
                }
            }
        }

        private Dictionary<string, string> convertItemNames = new Dictionary<string, string>()
        {
            {"Secret Ansem's Report 1", "Report1"},
            {"Secret Ansem's Report 2", "Report2"},
            {"Secret Ansem's Report 3", "Report3"},
            {"Secret Ansem's Report 4", "Report4"},
            {"Secret Ansem's Report 5", "Report5"},
            {"Secret Ansem's Report 6", "Report6"},
            {"Secret Ansem's Report 7", "Report7"},
            {"Secret Ansem's Report 8", "Report8"},
            {"Secret Ansem's Report 9", "Report9"},
            {"Secret Ansem's Report 10", "Report10"},
            {"Secret Ansem's Report 11", "Report11"},
            {"Secret Ansem's Report 12", "Report12"},
            {"Secret Ansem's Report 13", "Report13"},
            {"Hades Cup Trophy", "HadesCup"},
            {"Valor Form", "Valor"},
            {"Wisdom Form", "Wisdom"},
            {"Limit Form", "Limit"},
            {"Master Form", "Master"},
            {"Final Form", "Final"},
            {"Once More", "OnceMore"},
            {"Second Chance", "SecondChance"},
            {"Baseball Charm (Chicken Little)", "Baseball"},
            {"Lamp Charm (Genie)", "Lamp"},
            {"Ukulele Charm (Stitch)", "Ukulele"},
            {"Feather Charm (Peter Pan)", "Feather"},
            {"Proof of Connection", "Connection"},
            {"Proof of Nonexistence", "Nonexistence"},
            {"Proof of Peace", "Peace"},
            {"PromiseCharm", "PromiseCharm"}
        };

        //private Dictionary<string, string> convertWorldNames = new Dictionary<string, string>()
        //{
        //    {"Level", "SorasHeart" },
        //    {"Form Levels", "DriveForms" },
        //    {"Simulated Twilight Town", "SimulatedTwilightTown" },
        //    {"Twilight Town", "TwilightTown" },
        //    {"Hollow Bastion", "HollowBastion" },
        //    {"Beast's Castle", "BeastsCastle" },
        //    {"Olympus Coliseum", "OlympusColiseum" },
        //    {"Agrabah", "Agrabah" },
        //    {"Land of Dragons", "LandofDragons" },
        //    {"Hundred Acre Wood", "HundredAcreWood" },
        //    {"Pride Lands", "PrideLands" },
        //    {"Disney Castle / Timeless River", "DisneyCastle" },
        //    {"Halloween Town", "HalloweenTown" },
        //    {"Port Royal", "PortRoyal" },
        //    {"Space Paranoids", "SpaceParanoids" },
        //    {"The World That Never Was", "TWTNW" },
        //    {"Atlantica", "Atlantica" }
        //};

        private Dictionary<string, string> GetItemType = new Dictionary<string, string>()
        {
            {"Report1", "report"},
            {"Report2", "report"},
            {"Report3", "report"},
            {"Report4", "report"},
            {"Report5", "report"},
            {"Report6", "report"},
            {"Report7", "report"},
            {"Report8", "report"},
            {"Report9", "report"},
            {"Report10", "report"},
            {"Report11", "report"},
            {"Report12", "report"},
            {"Report13", "report"},
            {"Fire1", "magic"},
            {"Fire2", "magic"},
            {"Fire3", "magic"},
            {"Blizzard1", "magic"},
            {"Blizzard2", "magic"},
            {"Blizzard3", "magic"},
            {"Thunder1", "magic"},
            {"Thunder2", "magic"},
            {"Thunder3", "magic"},
            {"Cure1", "magic"},
            {"Cure2", "magic"},
            {"Cure3", "magic"},
            {"Reflect1", "magic"},
            {"Reflect2", "magic"},
            {"Reflect3", "magic"},
            {"Magnet1", "magic"},
            {"Magnet2", "magic"},
            {"Magnet3", "magic"},
            {"Valor", "form"},
            {"Wisdom", "form"},
            {"Limit", "form"},
            {"Master", "form"},
            {"Final", "form"},
            {"OnceMore", "ability"},
            {"SecondChance", "ability"},
            {"TornPage1", "page"},
            {"TornPage2", "page"},
            {"TornPage3", "page"},
            {"TornPage4", "page"},
            {"TornPage5", "page"},
            {"Baseball", "summon"},
            {"Lamp", "summon"},
            {"Ukulele", "summon"},
            {"Feather", "summon"},
            {"Connection", "proof"},
            {"Nonexistence", "proof"},
            {"Peace", "proof"},
            {"PromiseCharm", "proof"},
            //ghost versions
            {"Ghost_Report1", "report"},
            {"Ghost_Report2", "report"},
            {"Ghost_Report3", "report"},
            {"Ghost_Report4", "report"},
            {"Ghost_Report5", "report"},
            {"Ghost_Report6", "report"},
            {"Ghost_Report7", "report"},
            {"Ghost_Report8", "report"},
            {"Ghost_Report9", "report"},
            {"Ghost_Report10", "report"},
            {"Ghost_Report11", "report"},
            {"Ghost_Report12", "report"},
            {"Ghost_Report13", "report"},
            {"Ghost_Fire1", "magic"},
            {"Ghost_Fire2", "magic"},
            {"Ghost_Fire3", "magic"},
            {"Ghost_Blizzard1", "magic"},
            {"Ghost_Blizzard2", "magic"},
            {"Ghost_Blizzard3", "magic"},
            {"Ghost_Thunder1", "magic"},
            {"Ghost_Thunder2", "magic"},
            {"Ghost_Thunder3", "magic"},
            {"Ghost_Cure1", "magic"},
            {"Ghost_Cure2", "magic"},
            {"Ghost_Cure3", "magic"},
            {"Ghost_Reflect1", "magic"},
            {"Ghost_Reflect2", "magic"},
            {"Ghost_Reflect3", "magic"},
            {"Ghost_Magnet1", "magic"},
            {"Ghost_Magnet2", "magic"},
            {"Ghost_Magnet3", "magic"},
            {"Ghost_Valor", "form"},
            {"Ghost_Wisdom", "form"},
            {"Ghost_Limit", "form"},
            {"Ghost_Master", "form"},
            {"Ghost_Final", "form"},
            {"Ghost_OnceMore", "ability"},
            {"Ghost_SecondChance", "ability"},
            {"Ghost_TornPage1", "page"},
            {"Ghost_TornPage2", "page"},
            {"Ghost_TornPage3", "page"},
            {"Ghost_TornPage4", "page"},
            {"Ghost_TornPage5", "page"},
            {"Ghost_Baseball", "summon"},
            {"Ghost_Lamp", "summon"},
            {"Ghost_Ukulele", "summon"},
            {"Ghost_Feather", "summon"},
            {"Ghost_Connection", "proof"},
            {"Ghost_Nonexistence", "proof"},
            {"Ghost_Peace", "proof"},
            {"Ghost_PromiseCharm", "proof"}
        };
    }
}
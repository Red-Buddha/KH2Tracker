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

        public MainWindow MainW = (MainWindow)App.Current.MainWindow;

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
                Children.Remove(button);
                addRemove = -1;
            }
            UpdateGhostObtained(button, addRemove);

            int gridremainder = 0;
            if (Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((Children.Count / 5) + gridremainder, 1);
            Rows = gridnum;

            // default 1, add .5 for every row
            double length = 1 + ((Children.Count - 1) / 5) / 2.0;
            Grid outerGrid = (Parent as Grid).Parent as Grid;
            int row = (int)Parent.GetValue(Grid.RowProperty);
            outerGrid.RowDefinitions[row].Height = new GridLength(length, GridUnitType.Star);
            string worldName = Name.Substring(0, Name.Length - 4);

            //visit lock check first
            if (MainW.VisitLockOption.IsChecked)
            {
                SetVisitLock(button, add);
            }

            if (MainWindow.data.mode == Mode.AltHints || MainWindow.data.mode == Mode.OpenKHAltHints || MainWindow.data.mode == Mode.PathHints)
            {
                WorldComplete();

                if (MainWindow.data.WorldsData[worldName].hint != null)
                {
                    MainW.SetReportValue(MainWindow.data.WorldsData[worldName].hint, Children.Count);
                }
            }

            if (MainWindow.data.mode == Mode.DAHints)
            {
                if (button.Name.StartsWith("Ghost_"))
                    SetWorldGhost(worldName);
                else
                    WorldComplete();

                Console.WriteLine(button.Name + ": " + worldName + " added/removed " + (TableReturn(button.Name) * addRemove));

                Grid hint = MainWindow.data.WorldsData[worldName].hint;

                MainW.SetPoints(worldName, MainW.GetPoints(worldName) - (TableReturn(button.Name) * addRemove));
                MainW.SetReportValue(hint, MainW.GetPoints(worldName));

                //remove ghost items as needed then update points score
                if (worldName != "GoA" && !button.Name.StartsWith("Ghost_"))
                {
                    if (add)
                    {
                        Remove_Ghost(worldName, button);
                    }

                    MainW.UpdatePointScore(TableReturn(button.Name) * addRemove);
                }
            }
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            Data data = MainWindow.data;
            MainWindow window = ((MainWindow)Application.Current.MainWindow);
            if (e.Data.GetDataPresent(typeof(Item)))
            {
                Item item = e.Data.GetData(typeof(Item)) as Item;
                if (data.mode == Mode.DAHints)
                {
                    if (Handle_PointReport(item, window, data))
                        Add_Item(item, window);

                }
                else if (data.mode == Mode.PathHints)
                {
                    if (Handle_PathReport(item, window, data))
                        Add_Item(item, window);
                }
                else
                {
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
                    // hint text
                    window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + data.reportInformation[index].Item2 + " important checks");

                    // resetting fail icons
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
                            window.SetReportValue(data.WorldsData[data.reportInformation[reportIndex].Item1].hint, data.reportInformation[reportIndex].Item2);
                        }
                    }

                    // auto update world important check number
                    window.SetReportValue(data.WorldsData[data.reportInformation[index].Item1].hint, data.reportInformation[index].Item2);
                }
                else
                {
                    // update fail icons when location is report location is wrong
                    AddFailIcon(index);
                    return false;
                }
            }

            // show hint text on report hover
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
            if (worldName == "GoA" || MainWindow.data.WorldsData[worldName].complete)
                return;

            List<string> items = new List<string>();
            items.AddRange(MainWindow.data.WorldsData[Name.Substring(0, Name.Length - 4)].checkCount);

            foreach (var child in Children)
            {
                Item item = child as Item;
                char[] numbers = { '1', '2', '3', '4', '5' };

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

        public void SetVisitLock(Item item, bool add)
        {
            //reminder: 1 = locked | 0 = unlocked
            //reminder for TT: 10 = 3rd visit locked | 1 = 2nd visit locked | 11 = both locked | 0 = both unlocked
            string ItemName = item.Name;
            int addRemove = -1;

            if (!add)
                addRemove = 1;

            switch (ItemName)
            {
                case "AuronWep":
                    MainWindow.data.WorldsData["OlympusColiseum"].visitLocks += addRemove;
                    break;
                case "MulanWep":
                    MainWindow.data.WorldsData["LandofDragons"].visitLocks += addRemove;
                    break;
                case "BeastWep":
                    MainWindow.data.WorldsData["BeastsCastle"].visitLocks += addRemove;
                    break;
                case "JackWep":
                    MainWindow.data.WorldsData["HalloweenTown"].visitLocks += addRemove;
                    break;
                case "SimbaWep":
                    MainWindow.data.WorldsData["PrideLands"].visitLocks += addRemove;
                    break;
                case "SparrowWep":
                    MainWindow.data.WorldsData["PortRoyal"].visitLocks += addRemove;
                    break;
                case "AladdinWep":
                    MainWindow.data.WorldsData["Agrabah"].visitLocks += addRemove;
                    break;
                case "TronWep":
                    MainWindow.data.WorldsData["SpaceParanoids"].visitLocks += addRemove;
                    break;
                case "IceCream":
                    MainWindow.data.WorldsData["TwilightTown"].visitLocks += (addRemove * 10);
                    break;
                case "Picture":
                    MainWindow.data.WorldsData["TwilightTown"].visitLocks += addRemove;
                    break;
                case "MembershipCard":
                    MainWindow.data.WorldsData["HollowBastion"].visitLocks += addRemove;
                    break;
                default:
                    return;
            }

            MainW.VisitLockCheck();
        }

        //Path hints stuff
        public bool Handle_PathReport(Item item, MainWindow window, Data data)
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
                    // hint text and proof icon siplay
                    window.SetHintText(Codes.GetHintTextName(data.pathreportInformation[index].Item1));
                    PathProofToggle(data.pathreportInformation[index].Item2, data.pathreportInformation[index].Item3);

                    // resetting fail icons
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

            // show hint text on report hover
            if (isreport)
            {
                item.MouseEnter -= item.Report_Hover;
                item.MouseEnter += item.Report_Hover;
            }

            return true;
        }

        public void PathProofToggle(string location, int proofs)
        {
            //reminder: location is what report is for, not from
            //reminder: con = 1 | non = 10 | peace = 100
            string world = location + "Path";
            bool conProof = false;
            bool nonProof = false;
            bool peaProof = false;

            Grid pathgrid = MainWindow.data.WorldsData[location].top.FindName(world) as Grid;
            Image top = pathgrid.FindName(world + "_Con") as Image;
            Image middle = pathgrid.FindName(world + "_Non") as Image;
            Image bottom = pathgrid.FindName(world + "_Pea") as Image;

            if (proofs >= 100) //peace
            {
                bottom.Visibility = Visibility.Visible;
                peaProof = true;
                proofs -= 100;
            }
            if (proofs >= 10) //nonexsitance
            {
                middle.Visibility = Visibility.Visible;
                nonProof = true;
                proofs -= 10;
            }
            if (proofs == 1) //connection
            {
                top.Visibility = Visibility.Visible;
                conProof = true;
            }

            if (proofs == 0 && !conProof && !nonProof && !peaProof) //no path to light
            {
                middle.Source = new BitmapImage(new Uri("Images/Other/cross.png", UriKind.Relative));
                middle.Visibility = Visibility.Visible;
            }
        }

        //points hints stuff
        public int TableReturn(string nameButton)
        {
            string type = Codes.FindItemType(nameButton);

            if (type == "Unknown" || (nameButton.StartsWith("Ghost_") && !MainW.GhostMathOption.IsChecked))
            {
                return 0;
            }
            else if (MainWindow.data.PointsDatanew.Keys.Contains(type))
            {
                return MainWindow.data.PointsDatanew[type];
            }
            else
            {
                return 0;
            }
        }

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
                    // hint text
                    window.SetHintText(Codes.GetHintTextName(data.pointreportInformation[index].Item1) + " has " + Codes.FindShortName(data.pointreportInformation[index].Item2));
                    CheckGhost(data.pointreportInformation[index].Item1, data.pointreportInformation[index].Item2, window, data, "Report" + index);

                    //resetting fail icons
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

            // show hint text on report hover
            if (isreport)
            {
                item.MouseEnter -= item.Report_Hover;
                item.MouseEnter += item.Report_Hover;
            }

            return true;
        }

        public void CheckGhost(string world, string item, MainWindow window, Data data, string report)
        {
            //don't bother checking if ghost tracking is off
            if (MainW.GhostItemOption.IsChecked == false)
                return;

            //check if report was tracked before in this session to avoid tracking multiple ghosts for removing and placing the same report back
            if (data.TrackedReports.Contains(report))
                return;
            else
                data.TrackedReports.Add(report);

            //parse item name
            string itemname = Codes.ConvertSeedGenName(item);

            //this shouldn't ever happen, but return without doing anything else if the ghost values for magic/pages are higher than expected
            switch (itemname)
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
                case "TornPage":
                    if (Ghost_Pages >= 5)
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }

            //cycle through hinted world's checks for items
            char[] numbers = { '1', '2', '3', '4', '5' };
            List<string> CurrentGhosts = new List<string>();
            List<string> CurrentItems = new List<string>();
            foreach (var child in data.WorldsData[world].worldGrid.Children)
            {
                Item ItemCheck = child as Item;
                if (ItemCheck.Name.StartsWith("Ghost_"))
                {
                    //parse ghost item name
                    string itemnameGhost = ItemCheck.Name;

                    //trim numbers if needed
                    if (Codes.FindItemType(ItemCheck.Name) == "magic" || Codes.FindItemType(ItemCheck.Name) == "page")
                    {
                        itemnameGhost = itemnameGhost.TrimEnd(numbers);
                    }

                    //avoid adding a ghost entirely if a ghost of the same type is found
                    if (CurrentGhosts.Contains(itemnameGhost))
                        continue;
                    else
                        CurrentGhosts.Add(itemnameGhost);
                }
                else
                {
                    //parse ghost item name
                    string itemstring = ItemCheck.Name;

                    //trim numbers if needed
                    if (Codes.FindItemType(ItemCheck.Name) == "magic" || Codes.FindItemType(ItemCheck.Name) == "page")
                    {
                        itemstring = itemstring.TrimEnd(numbers);
                    }

                    //avoid adding a ghost entirely if a ghost of the same type is found
                    if (CurrentItems.Contains(itemstring))
                        continue;
                    else
                        CurrentItems.Add(itemstring);
                }
            }

            //compare hinted item to current list of ghosts
            if (CurrentGhosts.Contains("Ghost_" + itemname) || CurrentItems.Contains(itemname))
            {
                return;
            }

            //look for avaiable ghost item in item pool to track
            foreach (var child in MainW.ItemPool.Children)
            {
                Item Ghost = child as Item;

                if (Ghost != null && Ghost.Name.Contains("Ghost_" + itemname))
                {
                    //found ghost item, let's track it and break
                    data.WorldsData[world].worldGrid.Add_Ghost(Ghost, window);
                    break;
                }
            }
        }

        public void SetItemPoolGhosts(string item, string type)
        {
            int GhostIC = 0;
            int ObtainedIC = 0;
            Grid ItemPool = MainW.ItemPool;

            //simplier icon opacity change for non pages/magic
            if (type != "magic" && type != "page")
            {
                if (item.StartsWith("Ghost_"))
                {
                    item = item.Remove(0, 6);
                    Item Check = ItemPool.FindName(item) as Item;
                    if (Check != null && Check.Parent == ItemPool)
                    {
                        Check.Opacity = universalOpacity;
                    }
                }
                return;
            }

            switch (item)
            {
                case "Ghost_Fire":
                case "Fire":
                    GhostIC = Ghost_Fire;
                    ObtainedIC = Ghost_Fire_obtained;
                    break;
                case "Ghost_Blizzard":
                case "Blizzard":
                    GhostIC = Ghost_Blizzard;
                    ObtainedIC = Ghost_Blizzard_obtained;
                    break;
                case "Ghost_Thunder":
                case "Thunder":
                    GhostIC = Ghost_Thunder;
                    ObtainedIC = Ghost_Thunder_obtained;
                    break;
                case "Ghost_Cure":
                case "Cure":
                    GhostIC = Ghost_Cure;
                    ObtainedIC = Ghost_Cure_obtained;
                    break;
                case "Ghost_Reflect":
                case "Reflect":
                    GhostIC = Ghost_Reflect;
                    ObtainedIC = Ghost_Reflect_obtained;
                    break;
                case "Ghost_Magnet":
                case "Magnet":
                    GhostIC = Ghost_Magnet;
                    ObtainedIC = Ghost_Magnet_obtained;
                    break;
                case "Ghost_TornPage":
                case "TornPage":
                    GhostIC = Ghost_Pages;
                    ObtainedIC = Ghost_Pages_obtained;
                    break;
                default:
                    Console.WriteLine("Something went wrong? item wasn't expected. Item: " + item);
                    return;
            }

            if ((type == "page" && ObtainedIC == 5) || (type == "magic" && ObtainedIC == 3))
            {
                GhostIC = 0;
                return;
            }

            //there shouldn't be any more than 3 (magic) or 5 (pages) visible
            //on the tracker with real and ghost combined
            if (type == "magic" && (GhostIC + ObtainedIC) > 3)
            {
                Console.WriteLine("more than 3 of this item visible? Item: " + item);
                return;
            }
            if (type == "page" && (GhostIC + ObtainedIC) > 5)
            {
                Console.WriteLine("more than 5 of this item visible? Item: " + item);
                return;
            }

            string checkName = "";

            if (item.StartsWith("Ghost_"))
                item = item.Remove(0, 6);

            int Count = 3;
            if (type == "pages")
                Count = 5;

            //reset opacity and add items to a temp list
            List<string> foundChecks = new List<string>();
            for (int i = 1; i <= Count; i++)
            {
                checkName = item + i.ToString();
                Item Check = ItemPool.FindName(checkName) as Item;
                if (Check != null && Check.Parent == ItemPool)
                {
                    Check.Opacity = 1.0;
                    foundChecks.Add(Check.Name);
                }
            }

            //calculate opacity again (for dynamic change on adding removing checks
            if (GhostIC > foundChecks.Count)
            {
                Console.WriteLine("Ghost Count is greater than number of items left in itempool! How did this happen?");
                return;
            }

            for (int i = 1; i <= GhostIC; i++)
            {
                Item Check = ItemPool.FindName(foundChecks[i-1]) as Item;
                if (Check != null)
                {
                    Check.Opacity = universalOpacity;
                }
            }
        }

        public void Add_Ghost(Item item, MainWindow window)
        {
            // move item to world
            if (MainW.GhostItemOption.IsChecked)
            {
                window.ItemPool.Children.Remove(item);
                Handle_WorldGrid(item, true);
            }
        }

        public void Remove_Ghost(string world, Item item)
        {
            //check to see if world currently contains a ghost
            if (!MainWindow.data.WorldsData[world].containsGhost)
                return;

            //get correct item name
            char[] numbers = { '1', '2', '3', '4', '5' };
            string itemname = item.Name;
            if (Codes.FindItemType(item.Name) == "magic" || Codes.FindItemType(item.Name) == "page")
            {
                itemname = itemname.TrimEnd(numbers);
            }

            foreach (var child in Children)
            {
                Item ghostItem = child as Item;
                string itemnameGhost;
                if (ghostItem != null && ghostItem.Name.StartsWith("Ghost_"))
                {
                    itemnameGhost = ghostItem.Name;

                    //trim numbers
                    if (Codes.FindItemType(ghostItem.Name) == "magic" || Codes.FindItemType(ghostItem.Name) == "page")
                    {
                        itemnameGhost = itemnameGhost.TrimEnd(numbers);
                    }

                    //compare and remove if same
                    if (itemname == itemnameGhost.Remove(0, 6))
                    {
                        Handle_WorldGrid(ghostItem, false);
                        return;
                    }
                }
            }
        }

        public void SetWorldGhost(string worldName)
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

        public void UpdateGhostObtained(Item item, int addremove)
        {
            if (MainWindow.data.mode != Mode.DAHints)
                return;

            //increase obtained number for magics/pages
            char[] numbers = { '1', '2', '3', '4', '5' };
            string itemname = item.Name.TrimEnd(numbers);
            string itemntype = Codes.FindItemType(item.Name);

            //update normal items obtained
            if ((itemntype == "magic" || itemntype == "page") && !itemname.StartsWith("Ghost_"))
            {
                switch (itemname)
                {
                    case "Fire":
                        Ghost_Fire_obtained += addremove;
                        break;
                    case "Blizzard":
                        Ghost_Blizzard_obtained += addremove;
                        break;
                    case "Thunder":
                        Ghost_Thunder_obtained += addremove;
                        break;
                    case "Cure":
                        Ghost_Cure_obtained += addremove;
                        break;
                    case "Reflect":
                        Ghost_Reflect_obtained += addremove;
                        break;
                    case "Magnet":
                        Ghost_Magnet_obtained += addremove;
                        break;
                    case "TornPage":
                        Ghost_Pages_obtained += addremove;
                        break;
                }
            }

            //update ghost items hinted
            if ((itemntype == "magic" || itemntype == "page") && itemname.StartsWith("Ghost_"))
            {
                switch (itemname)
                {
                    case "Ghost_Fire":
                        Ghost_Fire += addremove;
                        break;
                    case "Ghost_Blizzard":
                        Ghost_Blizzard += addremove;
                        break;
                    case "Ghost_Thunder":
                        Ghost_Thunder += addremove;
                        break;
                    case "Ghost_Cure":
                        Ghost_Cure += addremove;
                        break;
                    case "Ghost_Reflect":
                        Ghost_Reflect += addremove;
                        break;
                    case "Ghost_Magnet":
                        Ghost_Magnet += addremove;
                        break;
                    case "Ghost_TornPage":
                        Ghost_Pages += addremove;
                        break;
                }
            }

            SetItemPoolGhosts(itemname, itemntype);
        }

    }
}
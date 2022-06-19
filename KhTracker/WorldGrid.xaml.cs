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
                if (MainWindow.data.mode == Mode.SpoilerHints || MainWindow.data.mode == Mode.DAHints)
                {
                    int firstGhost = -1;

                    foreach (Item child in Children)
                    {
                        if (child.Name.StartsWith("Ghost_"))
                        {
                            firstGhost = Children.IndexOf(child);
                            break;
                        }
                    }

                    try
                    {
                        if (firstGhost != -1)
                        {
                            Children.Insert(firstGhost, button);
                        }
                        else
                            Children.Add(button);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else
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

                //Grid hint = MainWindow.data.WorldsData[worldName].hint;

                MainW.SetPoints(worldName, MainW.GetPoints(worldName) - (TableReturn(button.Name) * addRemove));
                MainW.SetReportValue(MainWindow.data.WorldsData[worldName].hint, MainW.GetPoints(worldName));

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

            if (MainWindow.data.mode == Mode.SpoilerHints)
            {
                if (MainWindow.SpoilerWorldCompletion && !button.Name.StartsWith("Ghost_"))
                    WorldComplete();

                //remove ghost items as needed
                if (worldName != "GoA" && !button.Name.StartsWith("Ghost_") && add)
                {
                    Remove_Ghost(worldName, button);
                }

                if (MainWindow.data.WorldsData[worldName].hint != null)
                {
                    //Get count - ghosts
                    int realcount = 0;

                    foreach (Item item in Children)
                    {
                        if (item.Name.StartsWith("Ghost"))
                            continue;
                        else
                            realcount += 1;
                    }

                    //Set world value
                    MainW.SetReportValue(MainWindow.data.WorldsData[worldName].hint, realcount);
                }
            }
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            Data data = MainWindow.data;
            if (e.Data.GetDataPresent(typeof(Item)))
            {
                Item item = e.Data.GetData(typeof(Item)) as Item;
                if (data.mode == Mode.DAHints)
                {
                    if (Handle_PointReport(item, MainW, data))
                        Add_Item(item, MainW);
                }
                else if (data.mode == Mode.PathHints)
                {
                    if (Handle_PathReport(item, MainW, data))
                        Add_Item(item, MainW);
                }
                else if (data.mode == Mode.SpoilerHints)
                {
                    if (Handle_SpoilerReport(item, MainW, data))
                        Add_Item(item, MainW);
                }
                else
                {
                    if (Handle_Report(item, MainW, data))
                        Add_Item(item, MainW);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.Path.GetExtension(files[0]).ToUpper() == ".TXT")
                    MainW.LoadHints(files[0]);
                else if (System.IO.Path.GetExtension(files[0]).ToUpper() == ".PNACH")
                    MainW.ParseSeed(files[0]);
            }
        }

        public void Add_Item(Item item, MainWindow window)
        {
            // move item to world
            Grid ItemRow = VisualTreeHelper.GetChild(window.ItemPool, GetItemPool[item.Name]) as Grid;
            ItemRow.Children.Remove(item);
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
            if (data.hintsLoaded && GetItemPool[item.Name] == 0)
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
                else if (data.reportLocations[index] == "Joke")
                {
                    // hint text
                    window.SetJokeText(data.reportInformation[index].Item1);
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

        //Spoiler hints stuff
        public bool Handle_SpoilerReport(Item item, MainWindow window, Data data)
        {
            bool isreport = false;
            int index = 0;

            // item is a report
            if (data.hintsLoaded && GetItemPool[item.Name] == 0)
            {
                index = (int)item.GetValue(Grid.ColumnProperty);

                // out of report attempts
                if (data.reportAttempts[index] == 0)
                    return false;

                // check for correct report location
                if (data.reportLocations[index] == Name.Substring(0, Name.Length - 4))
                {
                    // hint text
                    if (data.reportInformation[index].Item1 == "Empty")
                    {
                        window.SetHintText("This report is too faded to read...");
                    }
                    else
                    {
                        //set alt text for a hinted world that has 0 checks
                        //(for when a world is toggled on, but happens to contain nothing)
                        if (data.reportInformation[index].Item2 == -1)
                        {
                            window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has no Important Checks");
                        }
                        else
                        {
                            window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has been revealed!");
                            SpoilerWorldReveal(data.reportInformation[index].Item1, data, "Report" + index);
                        }
                    }

                    // resetting fail icons
                    data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
                    data.reportAttempts[index] = 3;
                    isreport = true;
                    //change hinted world to use green numbers
                    //(we do this here instead of using SetWorldGhost cause we want world numbers to stay green until they are actually complete)
                    data.WorldsData[data.reportInformation[index].Item1].containsGhost = true;
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

            if (data.WorldsData[data.reportInformation[index].Item1].containsGhost)
                Updatenumbers_spoil(data.WorldsData[data.reportInformation[index].Item1]);

            return true;
        }

        public void Updatenumbers_spoil(WorldData worldData)
        {
            if (worldData.complete || worldData.containsGhost == false)
                return;

            if (worldData.hint != null)
            {
                int WorldNumber = -1;

                if (worldData.hint.Text != "?")
                    WorldNumber = int.Parse(worldData.hint.Text); //MainW.GetWorldNumber(worldData.hint);

                MainW.SetWorldNumber(worldData.hint, WorldNumber, "G");
            }
            else
                return;
        }

        public void SpoilerWorldReveal(string worldname, Data data, string report)
        {
            //check if report was tracked before in this session to avoid tracking multiple ghosts for removing and placing the same report back
            if (data.TrackedReports.Contains(report))
                return;
            else
                data.TrackedReports.Add(report);

            List<string> WorldItems = data.WorldsData[worldname].checkCount;
            char[] numbers = { '1', '2', '3', '4', '5' };

            //Get list of items we should track .we don't want to place more ghosts than is needed
            //(ex. a world has 2 blizzards and we already have 1 tracked there)
            WorldGrid worldGrid = data.WorldsData[worldname].worldGrid;
            foreach (Item item in worldGrid.Children)
            {
                if (item.Name.Contains("Report") || item.Name.StartsWith("Ghost_"))
                    WorldItems.Remove(item.Name);
                else if (WorldItems.Contains(item.Name.TrimEnd(numbers)))
                {
                    WorldItems.Remove(item.Name.TrimEnd(numbers));
                }
            }

            foreach (string itemname in WorldItems)
            {
                //don't track item types not in reveal list
                if (!data.SpoilerRevealTypes.Contains(Codes.FindItemType(itemname)))
                {
                    continue;
                }

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

                //look for avaiable ghost item in item pool to track
                for (int i = 5; i <= 9; i++) //loop through ghost collumns only 
                {
                    Grid ItemRow = VisualTreeHelper.GetChild(MainW.ItemPool, i) as Grid;
                    Console.WriteLine(ItemRow.Children.Count);
                    foreach (Item Ghost in ItemRow.Children)
                    {
                        Console.WriteLine(Ghost.Name);
                        if (Ghost != null && Ghost.Name.Contains("Ghost_" + itemname))
                        {
                            //found ghost item
                            data.WorldsData[worldname].worldGrid.Add_Ghost(Ghost, MainW);
                            break;
                        }
                    }
                }
            }
        }

        //Path hints stuff
        public bool Handle_PathReport(Item item, MainWindow window, Data data)
        {
            bool isreport = false;

            // item is a report
            if (data.hintsLoaded && GetItemPool[item.Name] == 0)
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
            if (data.hintsLoaded && GetItemPool[item.Name] == 0)
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
            for (int i = 5; i <= 9; i++) //loop through ghost collumns only 
            {
                Grid ItemRow = VisualTreeHelper.GetChild(MainW.ItemPool, i) as Grid;
                foreach (Item Ghost in ItemRow.Children)
                {
                    if (Ghost != null && Ghost.Name.Contains("Ghost_" + itemname))
                    {
                        //found ghost item, let's track it and break
                        data.WorldsData[world].worldGrid.Add_Ghost(Ghost, window);
                        break;
                    }
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
                //check if a ghost item was tracked
                if (item.StartsWith("Ghost_"))
                {
                    item = item.Remove(0, 6);                       //remove "Ghost_" from name
                    Grid ItemRow = VisualTreeHelper.GetChild(ItemPool, GetItemPool[item]) as Grid;
                    Item Check = ItemRow.FindName(item) as Item;   //check to see if item exists in ItemPool
                    if (Check != null && Check.Parent == ItemRow)  //check to see if item is *in* in ItemPool (don't want the ones tracked to the world changed)
                    {
                        Check.Opacity = universalOpacity; //change opacity
                    }
                }
                return;
            }

            //figure out what kinda item we are working with
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

            //return and do nothing if the actual obtained number of items is maxed
            if ((type == "page" && ObtainedIC == 5) || (type == "magic" && ObtainedIC == 3))
            {
                GhostIC = 0;
                return;
            }

            //there shouldn't be any more than 3 (magic) or 5 (pages) of a
            //single type visible on worlds at once
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

            if (item.StartsWith("Ghost_"))
                item = item.Remove(0, 6);

            int Count = 3;
            if (type == "page")
                Count = 5;

            //reset opacity and add items to a temp list
            List<string> foundChecks = new List<string>();
            for (int i = 1; i <= Count; i++)
            {
                string checkName = item + i.ToString();
                Grid ItemRow = VisualTreeHelper.GetChild(ItemPool, GetItemPool[checkName]) as Grid;
                Item Check = ItemRow.FindName(checkName) as Item;

                if (Check != null && Check.Parent == ItemRow)
                {
                    Check.Opacity = 1.0;
                    foundChecks.Add(Check.Name);
                }
            }

            if (GhostIC > foundChecks.Count)
            {
                Console.WriteLine("Ghost Count is greater than number of items left in itempool! How did this happen?");
                return;
            }

            //calculate opacity again (for dynamic change on adding removing checks
            for (int i = 1; i <= GhostIC; i++)
            {
                Grid ItemRow = VisualTreeHelper.GetChild(ItemPool, GetItemPool[foundChecks[i - 1]]) as Grid;
                Item Check = ItemRow.FindName(foundChecks[i-1]) as Item;
                if (Check != null)
                {
                    Check.Opacity = universalOpacity;
                }
            }
        }

        public void Add_Ghost(Item item, MainWindow window)
        {
            if (MainW.GhostItemOption.IsChecked || MainWindow.data.mode == Mode.SpoilerHints)
            {
                Grid ItemRow = VisualTreeHelper.GetParent(item) as Grid;
                if (ItemRow != null && ItemRow.Parent == MainW.ItemPool)
                {
                    ItemRow.Children.Remove(item);
                    Handle_WorldGrid(item, true);
                }
            }

            // move item to world
            //if (MainW.GhostItemOption.IsChecked)
            //{
            //    ItemRow.Children.Remove(item);
            //    Handle_WorldGrid(item, true);
            //}
        }

        public void Remove_Ghost(string world, Item item)
        {
            //check to see if world currently contains a ghost

            //if Points Hints and world doesn't contain a ghost yet, do nothing and return
           if (!MainWindow.data.WorldsData[world].containsGhost && MainWindow.data.mode == Mode.DAHints)
                return;

            //If spoiler hints, check if ANY currently tracked item in this world is a ghost
            //and return and do nothing if there are none to not waste time.
            bool hasGhost = false;
            if (MainWindow.data.mode == Mode.SpoilerHints)
            {
                foreach (Item child in MainWindow.data.WorldsData[world].worldGrid.Children)
                {
                    if (child.Name.StartsWith("Ghost_"))
                    {
                        hasGhost = true;
                        break;
                    }
                }

                if (!hasGhost)
                    return;
            }

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
                        ghostItem.HandleItemReturn();
                        return;
                    }
                }
            }
        }

        public void SetWorldGhost(string worldName)
        {
            foreach (Item child in Children)
            {
                if (Data.GhostItems.Values.Contains(child))
                {
                    MainWindow.data.WorldsData[worldName].containsGhost = true;
                    return;
                }
                else
                {
                    MainWindow.data.WorldsData[worldName].containsGhost = false;
                }
            }

            //foreach (Item ghost in Data.GhostItems.Values.ToList())
            //{
            //    if (Children.Contains(ghost))
            //    {
            //        MainWindow.data.WorldsData[worldName].containsGhost = true;
            //        return;
            //    }
            //    else
            //    {
            //        MainWindow.data.WorldsData[worldName].containsGhost = false;
            //    }
            //}
        }

        public void UpdateGhostObtained(Item item, int addremove)
        {
            if (MainWindow.data.mode != Mode.DAHints && MainWindow.data.mode != Mode.SpoilerHints)
            {
                return;
            }

            char[] numbers = { '1', '2', '3', '4', '5' };
            string itemntype = Codes.FindItemType(item.Name);
            string itemname;

            if (item.Name.Contains("Report"))
                itemname = item.Name;
            else
                itemname = item.Name.TrimEnd(numbers);

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
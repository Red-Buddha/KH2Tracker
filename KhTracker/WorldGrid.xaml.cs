using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for WorldGrid.xaml
    /// </summary>
    public partial class WorldGrid : UniformGrid
    {
        //let's simplyfy some stuff and remove a ton of redundant code
        MainWindow window = (MainWindow)App.Current.MainWindow;
        Data data = MainWindow.data;

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

        ///Add Switch Case???
        public void Handle_WorldGrid(Item button, bool add)
        {
            int addRemove = 1;

            if (add)
            {
                if (data.mode == Mode.SpoilerHints || data.mode == Mode.DAHints)
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
            if (window.VisitLockOption.IsChecked)
            {
                SetVisitLock(button.Name, addRemove);
            }

            if (data.mode == Mode.AltHints || data.mode == Mode.OpenKHAltHints || data.mode == Mode.PathHints)
            {
                WorldComplete();

                if (data.WorldsData[worldName].value != null)
                {
                    window.SetWorldValue(data.WorldsData[worldName].value, Children.Count);
                }
            }

            if (data.mode == Mode.DAHints)
            {
                if (button.Name.StartsWith("Ghost_"))
                    SetWorldGhost(worldName);
                else
                    WorldComplete();

                //Console.WriteLine(button.Name + ": " + worldName + " added/removed " + (TableReturn(button.Name) * addRemove));

                window.SetPoints(worldName, window.GetPoints(worldName) - (TableReturn(button.Name) * addRemove));
                window.SetWorldValue(MainWindow.data.WorldsData[worldName].value, window.GetPoints(worldName));

                //remove ghost items as needed then update points score
                if (worldName != "GoA" && !button.Name.StartsWith("Ghost_"))
                {
                    if (add)
                    {
                        Remove_Ghost(worldName, button);
                    }

                    window.UpdatePointScore(TableReturn(button.Name) * addRemove);
                }
            }

            if (data.mode == Mode.SpoilerHints)
            {
                if (data.SpoilerWorldCompletion && !button.Name.StartsWith("Ghost_"))
                    WorldComplete();

                //remove ghost items as needed
                if (worldName != "GoA" && !button.Name.StartsWith("Ghost_") && add)
                {
                    Remove_Ghost(worldName, button);
                }

                if (data.WorldsData[worldName].value != null)
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
                    window.SetWorldValue(data.WorldsData[worldName].value, realcount);
                }
            }
        }

        private void Item_Drop(Object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Item)))
            {
                Item item = e.Data.GetData(typeof(Item)) as Item;

                if (ReportHandler(item))
                    Add_Item(item);
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

        public void Add_Item(Item item)
        {
            //remove item from itempool
            Grid ItemRow = VisualTreeHelper.GetParent(item) as Grid;
            ItemRow.Children.Remove(item);

            //add it to the world grid
            Handle_WorldGrid(item, true);

            //Reset any obtained item to be normal transparency
            item.Opacity = 1.0;

            // update collection count
            window.SetCollected(true);

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

        ///
        /// Report handling 
        ///

        public bool ReportHandler(Item item)
        {
            //we use this to check if a report is valid to be tracked before placing it in the world grid.
            //an incorrect report will update its fail status and return false
            //if any other item then alwys return true and skip any report related code.
            //(maybe this can be simplified?

            // item is a report
            if (data.hintsLoaded && item.Name.StartsWith("Report"))
            {
                //index used to get correct report info.
                //we just remove "Report" from the item name and parse the left over number as int -1 since lists are 0 based.
                int index = int.Parse(item.Name.Remove(0,6)) - 1;

                // out of report attempts
                if (data.reportAttempts[index] == 0)
                    return false;

                // check for correct report location then run report hint logic based on current hint mode
                if (data.reportLocations[index] == Name.Substring(0, Name.Length - 4))
                {
                    switch (data.mode)
                    {
                        case Mode.Hints:
                        case Mode.OpenKHHints:
                            //setup joke logic later
                            break;
                        case Mode.AltHints:
                        case Mode.OpenKHAltHints:
                            Report_Jsmartee(index, item);
                            break;
                        case Mode.DAHints:
                            Report_Points(index);
                            break;
                        case Mode.PathHints:
                            Report_Path(index);
                            break;
                        case Mode.SpoilerHints:
                            Report_Spoiler(index);
                            break;
                        default:
                            window.SetHintText("Impossible Report Error! How are you seeing this?");
                            return false;
                    }

                    // show hint text on report hover
                    item.MouseEnter -= item.Report_Hover;
                    item.MouseEnter += item.Report_Hover;
                }
                else
                {
                    // update fail icons when location is report location is wrong
                    data.reportAttempts[index]--;
                    switch (data.reportAttempts[index])
                    {
                        case 2:
                            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail1");
                            break;
                        case 1:
                            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail2");
                            break;
                        case 0:
                        default:
                            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail3");
                            break;
                    }
                    return false;
                }
            }

            return true;
        }

        private void Report_Jsmartee(int index, Item item)
        {
            // hint text
            window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item2) + " has " + data.reportInformation[index].Item3 + " important checks");

            // resetting fail icons
            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
            data.reportAttempts[index] = 3;

            item.DragDropEventFire(data.reportInformation[index].Item2, data.reportInformation[index].Item3);

            // set world report hints to as hinted then checks if the report location was hinted to set if its a hinted hint
            data.WorldsData[data.reportInformation[index].Item2].hinted = true;

            if (data.WorldsData[data.reportLocations[index]].hinted == true)
            {
                data.WorldsData[data.reportInformation[index].Item2].hintedHint = true;
            }

            // loop through hinted world for reports to set their info as hinted hints
            for (int i = 0; i < data.WorldsData[data.reportInformation[index].Item2].worldGrid.Children.Count; ++i)
            {
                Item gridItem = data.WorldsData[data.reportInformation[index].Item2].worldGrid.Children[i] as Item;
                if (gridItem.Name.Contains("Report"))
                {
                    int reportIndex = int.Parse(gridItem.Name.Substring(6)) - 1;
                    data.WorldsData[data.reportInformation[reportIndex].Item2].hintedHint = true;
                    window.SetWorldValue(data.WorldsData[data.reportInformation[reportIndex].Item2].value, data.reportInformation[reportIndex].Item3);
                }
            }

            // auto update world important check number
            window.SetWorldValue(data.WorldsData[data.reportInformation[index].Item2].value, data.reportInformation[index].Item3);

            //put here to remind myself later
            //else if (data.reportLocations[index] == "Joke")
            //{
            //    // hint text
            //    //window.SetJokeText(data.reportInformation[index].Item2);
            //    isreport = true;
            //}
        }

        private void Report_Points(int index)
        {
            // hint text
            window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has " + Codes.FindShortName(data.reportInformation[index].Item2));
            CheckGhost(data.reportInformation[index].Item1, data.reportInformation[index].Item2, "Report" + index);

            //resetting fail icons
            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
            data.reportAttempts[index] = 3;
        }

        private void Report_Path(int index)
        {
            // hint text and proof icon display
            window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1));
            PathProofToggle(data.reportInformation[index].Item2, data.reportInformation[index].Item3);

            // resetting fail icons
            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
            data.reportAttempts[index] = 3;
        }

        private void Report_Spoiler(int index)
        {
            // hint text
            if (data.reportInformation[index].Item1 == "Empty")
            {
                window.SetHintText("This report reveals nothing...");
            }
            else
            {
                //set alt text for a hinted world that has 0 checks
                //(for when a world is toggled on, but happens to contain nothing)
                if (data.reportInformation[index].Item3 == -1)
                {
                    window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has no Important Checks");
                }
                else
                {
                    window.SetHintText(Codes.GetHintTextName(data.reportInformation[index].Item1) + " has been revealed!");
                    SpoilerWorldReveal(data.reportInformation[index].Item1, "Report" + index);
                }
            }

            // resetting fail icons
            data.ReportAttemptVisual[index].SetResourceReference(ContentControl.ContentProperty, "Fail0");
            data.reportAttempts[index] = 3;

            //change hinted world to use green numbers
            //(we do this here instead of using SetWorldGhost cause we want world numbers to stay green until they are actually complete)
            data.WorldsData[data.reportInformation[index].Item1].containsGhost = true;

            if (data.WorldsData[data.reportInformation[index].Item1].containsGhost)
                window.SetWorldValue(data.WorldsData[data.reportInformation[index].Item1].value, int.Parse(data.WorldsData[data.reportInformation[index].Item1].value.Text));
        }

        ///
        /// world value handling
        ///

        //public void Updatenumbers_spoil(WorldData worldData)
        //{
        //    if (worldData.complete || worldData.containsGhost == false)
        //        return;
        //
        //    if (worldData.value != null)
        //    {
        //        int WorldNumber = -1;
        //
        //        if (worldData.value.Text != "?")
        //            WorldNumber = int.Parse(worldData.hint.Text);
        //
        //        MainW.SetWorldNumber(worldData.hint, WorldNumber, "G");
        //    }
        //    else
        //        return;
        //}

        private int TableReturn(string nameButton)
        {
            string type = Codes.FindItemType(nameButton);
            if (type != "Unknown")
            {
                return nameButton.StartsWith("Ghost_") && !window.GhostMathOption.IsChecked ? 0 : data.PointsDatanew[type];
            }
            return 0;

            //else if (MainWindow.data.PointsDatanew.Keys.Contains(type))
        }

        ///
        /// ghost item handling
        ///

        public void Add_Ghost(Item item)
        {
            //check if we even want to track a ghost item.
            if (window.GhostItemOption.IsChecked || data.mode == Mode.SpoilerHints)
            {
                //check item parent and track only if the parent is the itempool grid
                if (VisualTreeHelper.GetParent(item) is Grid ItemRow && ItemRow.Parent == window.ItemPool)
                {
                    ItemRow.Children.Remove(item);
                    Handle_WorldGrid(item, true);
                }
            }
        }

        private void Remove_Ghost(string world, Item item)
        {
            //check to see if world currently contains a ghost

            //if Points Hints and world doesn't contain a ghost yet, do nothing and return
            if (!data.WorldsData[world].containsGhost && data.mode == Mode.DAHints)
                return;

            //If spoiler hints, check if ANY currently tracked item in this world is a ghost
            //and return and do nothing if there are none.
            bool hasGhost = false;
            if (data.mode == Mode.SpoilerHints)
            {
                foreach (Item child in data.WorldsData[world].worldGrid.Children)
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

        private void CheckGhost(string world, string item, string report)
        {
            //don't bother checking if ghost tracking is off
            if (window.GhostItemOption.IsChecked == false)
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
                string itemName = ItemCheck.Name;

                //trim numbers if needed
                if (Codes.FindItemType(ItemCheck.Name) == "magic" || Codes.FindItemType(ItemCheck.Name) == "page")
                {
                    itemName = itemName.TrimEnd(numbers);
                }
                //update lists
                if (ItemCheck.Name.StartsWith("Ghost_"))
                {
                    //avoid adding a ghost entirely if a ghost of the same type is found (for magic and pages)
                    if (CurrentGhosts.Contains(itemName))
                        continue;
                    else
                        CurrentGhosts.Add(itemName);
                }
                else
                {
                    //avoid adding an item entirely if one of the same type is found (for magic and pages)
                    if (CurrentItems.Contains(itemName))
                        continue;
                    else
                        CurrentItems.Add(itemName);
                }
            }

            //compare hinted item to current list of ghosts
            if (CurrentGhosts.Contains("Ghost_" + itemname) || CurrentItems.Contains(itemname))
            {
                return;
            }

            //look for avaiable ghost item in item pool to track
            Grid ItemRow = VisualTreeHelper.GetChild(window.ItemPool, 4) as Grid;
            foreach (Item Ghost in ItemRow.Children)
            {
                if (Ghost != null && Ghost.Name.Contains("Ghost_" + itemname))
                {
                    //found ghost item, let's track it and break
                    data.WorldsData[world].worldGrid.Add_Ghost(Ghost);
                    break;
                }
            }
        }

        private void SpoilerWorldReveal(string worldname, string report)
        {
            //check if report was tracked before in this session to avoid tracking
            //multiple ghosts for removing and placing the same report back
            if (data.TrackedReports.Contains(report))
                return;
            else
                data.TrackedReports.Add(report);

            //create temp list of what a world should have
            List<string> WorldItems = data.WorldsData[worldname].checkCount;
            char[] numbers = { '1', '2', '3', '4', '5' };

            //Get list of items we should track. we don't want to place more ghosts than is needed
            //(ex. a world has 2 blizzards and we already have 1 tracked there)
            WorldGrid worldGrid = data.WorldsData[worldname].worldGrid;
            foreach (Item item in worldGrid.Children)
            {
                //just skip if item is a ghost. checkCount should never contain ghosts anyway
                if (item.Name.StartsWith("Ghost_"))
                    continue;

                //do not trim numbers if report
                if (item.Name.Contains("Report") && WorldItems.Contains(item.Name))
                    WorldItems.Remove(item.Name);
                else if (WorldItems.Contains(item.Name.TrimEnd(numbers)))
                {
                    WorldItems.Remove(item.Name.TrimEnd(numbers));
                }
            }

            //start tracking what's left in the temp list
            foreach (string itemname in WorldItems)
            {
                //don't track item types not set in reveal list
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
                //Note: for now the ghost items are always the 4th itemgrid.
                Grid ItemRow = VisualTreeHelper.GetChild(window.ItemPool, 4) as Grid;
                foreach (Item Ghost in ItemRow.Children)
                {
                    //Console.WriteLine(Ghost.Name);
                    if (Ghost != null && Ghost.Name.Contains("Ghost_" + itemname))
                    {
                        //found ghost item
                        data.WorldsData[worldname].worldGrid.Add_Ghost(Ghost);
                        break;
                    }
                }
            }
        }

        //TODO: this really doesn't need to be on its own.
        private void SetWorldGhost(string worldName)
        {
            foreach (Item child in Children)
            {
                if (data.GhostItems.Values.Contains(child))
                {
                    data.WorldsData[worldName].containsGhost = true;
                    return;
                }
                else
                {
                    data.WorldsData[worldName].containsGhost = false;
                }
            }
        }

        //TODO: these need to be changed and updated. we no longer need to make the icon transparent for magic/pages
        private void UpdateGhostObtained(Item item, int addremove)
        {
            //return if mod isn't either of these
            if (data.mode != Mode.DAHints && data.mode != Mode.SpoilerHints)
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

            //SetItemPoolGhosts(itemname, itemntype);
        }

        private void SetItemPoolGhosts(string item, string type)
        {
            //    int GhostIC = 0;
            //    int ObtainedIC = 0;
            //    Grid ItemPool = MainW.ItemPool;
            //
            //    //simplier icon opacity change for non pages/magic
            //    if (type != "magic" && type != "page")
            //    {
            //        //check if a ghost item was tracked
            //        if (item.StartsWith("Ghost_"))
            //        {
            //            item = item.Remove(0, 6);   //remove "Ghost_" from name
            //            Grid ItemRow = VisualTreeHelper.GetParent(item) as Grid;
            //            ItemRow.Children.Remove(item);
            //
            //
            //
            //
            //
            //            Tuple<Item, Grid> GhostItemInfo = GetItemCheck(item.Remove(0, 6), MainW);
            //            if (GhostItemInfo.Item1 != null && GhostItemInfo.Item1.Parent == GhostItemInfo.Item2)  //check to see if item is *in* in ItemPool (don't want the ones tracked to the world changed)
            //            {
            //                GhostItemInfo.Item1.Opacity = universalOpacity; //change opacity
            //            }
            //        }
            //        return;
            //    }
            //
            //    //figure out what kinda item we are working with
            //    switch (item)
            //    {
            //        case "Ghost_Fire":
            //        case "Fire":
            //            GhostIC = Ghost_Fire;
            //            ObtainedIC = Ghost_Fire_obtained;
            //            break;
            //        case "Ghost_Blizzard":
            //        case "Blizzard":
            //            GhostIC = Ghost_Blizzard;
            //            ObtainedIC = Ghost_Blizzard_obtained;
            //            break;
            //        case "Ghost_Thunder":
            //        case "Thunder":
            //            GhostIC = Ghost_Thunder;
            //            ObtainedIC = Ghost_Thunder_obtained;
            //            break;
            //        case "Ghost_Cure":
            //        case "Cure":
            //            GhostIC = Ghost_Cure;
            //            ObtainedIC = Ghost_Cure_obtained;
            //            break;
            //        case "Ghost_Reflect":
            //        case "Reflect":
            //            GhostIC = Ghost_Reflect;
            //            ObtainedIC = Ghost_Reflect_obtained;
            //            break;
            //        case "Ghost_Magnet":
            //        case "Magnet":
            //            GhostIC = Ghost_Magnet;
            //            ObtainedIC = Ghost_Magnet_obtained;
            //            break;
            //        case "Ghost_TornPage":
            //        case "TornPage":
            //            GhostIC = Ghost_Pages;
            //            ObtainedIC = Ghost_Pages_obtained;
            //            break;
            //        default:
            //            Console.WriteLine("Something went wrong? item wasn't expected. Item: " + item);
            //            return;
            //    }
            //
            //    //return and do nothing if the actual obtained number of items is maxed
            //    if ((type == "page" && ObtainedIC == 5) || (type == "magic" && ObtainedIC == 3))
            //    {
            //        GhostIC = 0;
            //        return;
            //    }
            //
            //    //there shouldn't be any more than 3 (magic) or 5 (pages) of a
            //    //single type visible on worlds at once
            //    if (type == "magic" && (GhostIC + ObtainedIC) > 3)
            //    {
            //        Console.WriteLine("more than 3 of this item visible? Item: " + item);
            //        return;
            //    }
            //    if (type == "page" && (GhostIC + ObtainedIC) > 5)
            //    {
            //        Console.WriteLine("more than 5 of this item visible? Item: " + item);
            //        return;
            //    }
            //
            //    if (item.StartsWith("Ghost_"))
            //        item = item.Remove(0, 6);
            //
            //    int Count = 3;
            //    if (type == "page")
            //        Count = 5;
            //
            //    //reset opacity and add items to a temp list
            //    List<string> foundChecks = new List<string>();
            //    for (int i = 1; i <= Count; i++)
            //    {
            //        string checkName = item + i.ToString();
            //        Tuple<Item, Grid> ItemInfo = GetItemCheck(checkName, MainW);
            //
            //        if (ItemInfo.Item1 != null && ItemInfo.Item1.Parent == ItemInfo.Item2)
            //        {
            //            ItemInfo.Item1.Opacity = 1.0;
            //            foundChecks.Add(ItemInfo.Item1.Name);
            //        }
            //    }
            //
            //    if (GhostIC > foundChecks.Count)
            //    {
            //        Console.WriteLine("Ghost Count is greater than number of items left in itempool! How did this happen?");
            //        return;
            //    }
            //
            //    //calculate opacity again (for dynamic change on adding removing checks
            //    //for (int i = 1; i <= GhostIC; i++)
            //    //{
            //    //    Grid ItemRow = VisualTreeHelper.GetChild(ItemPool, GetItemPool[foundChecks[i - 1]]) as Grid;
            //    //    Item Check = ItemRow.FindName(foundChecks[i-1]) as Item;
            //    //    if (Check != null)
            //    //    {
            //    //        Check.Opacity = universalOpacity;
            //    //    }
            //    //}
        }

        ///
        /// world/grid visual updating
        ///

        public void WorldComplete()
        {
            //run a check for current world to check if all checks have been found

            //get worldname by rmoving "Grid" from the end of the current worldgrid name
            string worldName = Name.Substring(0, Name.Length - 4);

            //if GoA or if the complete flag has been set. if so just return
            if (worldName == "GoA" || data.WorldsData[worldName].complete)
                return;

            //create a temp list for what checks a world should have
            List<string> tempItems = new List<string>();
            tempItems.AddRange(data.WorldsData[worldName].checkCount);

            //for each item currently tracked to worldgrid we remove it from the temp list
            char[] numbers = { '1', '2', '3', '4', '5' };
            foreach (var child in Children)
            {
                Item item = child as Item;

                //just skip if item is a ghost. checkCount should never contain ghosts anyway
                if (item.Name.StartsWith("Ghost_"))
                    continue;

                //do not trim numbers if report
                if (item.Name.Contains("Report") && tempItems.Contains(item.Name))
                    tempItems.Remove(item.Name);
                else if (tempItems.Contains(item.Name.TrimEnd(numbers)))
                {
                    tempItems.Remove(item.Name.TrimEnd(numbers));
                }
            }

            //if Templist is empty then worl can be marked as complete
            if (tempItems.Count == 0)
            {
                data.WorldsData[worldName].complete = true;
            }
        }

        private void SetVisitLock(string itemName, int addRemove)
        {
            //reminder: 1 = locked | 0 = unlocked
            //reminder for TT: 10 = 3rd visit locked | 1 = 2nd visit locked | 11 = both locked | 0 = both unlocked
            switch (itemName)
            {
                case "AuronWep":
                    data.WorldsData["OlympusColiseum"].visitLocks += addRemove;
                    break;
                case "MulanWep":
                    data.WorldsData["LandofDragons"].visitLocks += addRemove;
                    break;
                case "BeastWep":
                    data.WorldsData["BeastsCastle"].visitLocks += addRemove;
                    break;
                case "JackWep":
                    data.WorldsData["HalloweenTown"].visitLocks += addRemove;
                    break;
                case "SimbaWep":
                    data.WorldsData["PrideLands"].visitLocks += addRemove;
                    break;
                case "SparrowWep":
                    data.WorldsData["PortRoyal"].visitLocks += addRemove;
                    break;
                case "AladdinWep":
                    data.WorldsData["Agrabah"].visitLocks += addRemove;
                    break;
                case "TronWep":
                    data.WorldsData["SpaceParanoids"].visitLocks += addRemove;
                    break;
                case "IceCream":
                    data.WorldsData["TwilightTown"].visitLocks += (addRemove * 10);
                    break;
                case "Picture":
                    data.WorldsData["TwilightTown"].visitLocks += addRemove;
                    break;
                case "MembershipCard":
                    data.WorldsData["HollowBastion"].visitLocks += addRemove;
                    break;
                default:
                    return;
            }

            window.VisitLockCheck();
        }

        private void PathProofToggle(string location, int proofTotal)
        {
            //reminder: location is what report is for, not from
            //reminder: con = 1 | non = 10 | peace = 100

            //find grid path icons are in
            string worlthPath = location + "Path";
            Grid pathgrid = data.WorldsData[location].top.FindName(worlthPath) as Grid;
            //find each of the path icons
            Image top = pathgrid.FindName(worlthPath + "_Con") as Image;
            Image mid = pathgrid.FindName(worlthPath + "_Non") as Image;
            Image bot = pathgrid.FindName(worlthPath + "_Pea") as Image;

            //if total is 0 then world isn't a path to light,
            //so change the middle icon to a cross and return.
            if (proofTotal == 0) //no path to light
            {
                //TODO: set up custom images for the mini cross icon
                mid.Source = new BitmapImage(new Uri("Images/Other/cross.png", UriKind.Relative));
                mid.Visibility = Visibility.Visible;
                return;
            }

            //iterate through each of the proof values, highest first,
            //then set visibility, found state, and subtract the proof value from the total.
            //(i can simplify this without the bools, but this is a bit cleaner looking i think)
            if (proofTotal >= 100) //peace
            {
                bot.Visibility = Visibility.Visible;
                proofTotal -= 100;
            }
            if (proofTotal >= 10) //nonexsitance
            {
                mid.Visibility = Visibility.Visible;
                proofTotal -= 10;
            }
            if (proofTotal == 1) //connection
            {
                top.Visibility = Visibility.Visible;
            }
            else if (proofTotal < 1)
            {
                window.SetHintText("Impossible Path Error! How are you seeing this?");
            }
        }
    
    }
}
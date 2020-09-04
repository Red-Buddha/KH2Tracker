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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KhTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button selected = null;

        public MainWindow()
        {
            InitializeComponent();

            Worlds.Add(SoraHeart);
            Worlds.Add(DriveForms);
            Worlds.Add(SimulatedTwilightTown);
            Worlds.Add(TwilightTown);
            Worlds.Add(HollowBastion);
            Worlds.Add(BeastCastle);
            Worlds.Add(OlympusColiseum);
            Worlds.Add(Agrabah);
            Worlds.Add(LandofDragons);
            Worlds.Add(HundredAcreWood);
            Worlds.Add(PrideLands);
            Worlds.Add(DisneyCastle);
            Worlds.Add(HalloweenTown);
            Worlds.Add(PortRoyal);
            Worlds.Add(SpaceParanoids);
            Worlds.Add(TWTNW);
            Worlds.Add(GoA);

            Hints.Add(SoraHeartHint);
            Hints.Add(DriveFormsHint);
            Hints.Add(SimulatedHint);
            Hints.Add(TwilightTownHint);
            Hints.Add(HollowBastionHint);
            Hints.Add(BeastCastleHint);
            Hints.Add(OlympusColiseumHint);
            Hints.Add(AgrabahHint);
            Hints.Add(LandofDragonsHint);
            Hints.Add(HundredAcreWoodHint);
            Hints.Add(PrideLandsHint);
            Hints.Add(DisneyCastleHint);
            Hints.Add(HalloweenTownHint);
            Hints.Add(PortRoyalHint);
            Hints.Add(SpaceParanoidsHint);
            Hints.Add(TWTNWHint);

            Grids.Add(SoraHeartGrid);
            Grids.Add(DriveFormsGrid);
            Grids.Add(SimulatedGrid);
            Grids.Add(TwilightTownGrid);
            Grids.Add(HollowBastionGrid);
            Grids.Add(BeastCastleGrid);
            Grids.Add(OlympusColiseumGrid);
            Grids.Add(AgrabahGrid);
            Grids.Add(LandofDragonsGrid);
            Grids.Add(HundredAcreWoodGrid);
            Grids.Add(PrideLandsGrid);
            Grids.Add(DisneyCastleGrid);
            Grids.Add(HalloweenTownGrid);
            Grids.Add(PortRoyalGrid);
            Grids.Add(SpaceParanoidsGrid);
            Grids.Add(TWTNWGrid);
            Grids.Add(GoAGrid);
        }

        List<Button> Worlds = new List<Button>();
        List<TextBlock> Hints = new List<TextBlock>();
        List<UniformGrid> Grids = new List<UniformGrid>();

        private void HandleReportValue(TextBlock Hint, bool isTWTNW, int delta)
        {
            if (int.TryParse(Hint.Text, out int num) == false)
            {
                num = -1;
            }

            if (delta > 0)
                ++num;
            else
                --num;
            
            if (num >= 10)
            {
                if(!isTWTNW)
                    Hint.Margin = new Thickness(20, -7, 0, 0);
                else
                    Hint.Margin = new Thickness(30, -2, 0, 0);
            }
            else
            {
                if (!isTWTNW)
                    Hint.Margin = new Thickness(30, -7, 0, 0);
                else
                    Hint.Margin = new Thickness(40, -2, 0, 0);
            }

            if (num < 0)
                Hint.Text = "?";
            else
                Hint.Text = num.ToString();
        }

        private void HandleWorldGrid(UniformGrid grid, Button button, bool add)
        {
            if (add)
                grid.Children.Add(button);
            else
                grid.Children.Remove(button);
            
            int gridremainder = 0;
            if (grid.Children.Count % 5 != 0)
                gridremainder = 1;

            int gridnum = Math.Max((grid.Children.Count / 5) + gridremainder, 1);

            grid.Rows = gridnum;
            grid.Height = grid.Rows * 40;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            
            if (e.ChangedButton == MouseButton.Left)
            {
                if (selected != null)
                    selected.BorderBrush = Brushes.White;

                selected = button;
                selected.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF0E624"));
            }
            else if(e.ChangedButton == MouseButton.Middle)
            {
                for(int i = 0; i < Hints.Count; ++i)
                {
                    if(button == Worlds[i])
                    {
                        Hints[i].Text = "?";

                        if(Worlds[i] == TWTNW)
                            Hints[i].Margin = new Thickness(40, -2, 0, 0);
                        else
                            Hints[i].Margin = new Thickness(30, -7, 0, 0);

                        break;
                    }
                }
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Button button = sender as Button;

            for (int i = 0; i < Hints.Count; ++i)
            {
                if (button == Worlds[i])
                {
                    if (Worlds[i] == TWTNW)
                        HandleReportValue(Hints[i], true, e.Delta);
                    else
                        HandleReportValue(Hints[i], false, e.Delta);

                    break;
                }
            }
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(selected != null)
            {
                test.Children.Remove(button);

                for(int i = 0; i < Worlds.Count; ++i)
                {
                    if(selected == Worlds[i])
                    {
                        HandleWorldGrid(Grids[i], button, true);
                        break;
                    }
                }

                int collected = int.Parse(Collected.Text) + 1;
                Collected.Text = collected.ToString();
                if (collected >= 10)
                    Collected.Margin = new Thickness(0, 0, 10, 0);

                button.Click -= Item_Click;
                button.Click += Item_Return;
            }
        }

        private void Item_Return(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            
            HandleWorldGrid(button.Parent as UniformGrid, button, false);

            test.Children.Add(button);

            int collected = int.Parse(Collected.Text) - 1;
            Collected.Text = collected.ToString();
            if (collected < 10)
                Collected.Margin = new Thickness(0, 0, 20, 0);

            button.Click -= Item_Return;
            button.Click += Item_Click;
        }

        private void PromiseCharmToggle(object sender, RoutedEventArgs e)
        {
            if(PromiseCharmOption.IsChecked)
            {
                PromiseCharm.IsEnabled = true;
                PromiseCharm.Visibility = Visibility.Visible;
                CheckTotal.Text = "51";
            }
            else
            {
                PromiseCharm.IsEnabled = false;
                PromiseCharm.Visibility = Visibility.Hidden;
                CheckTotal.Text = "50";

                if (PromiseCharm.Parent != test)
                {
                    HandleWorldGrid(PromiseCharm.Parent as UniformGrid, PromiseCharm, false);

                    test.Children.Add(PromiseCharm);

                    int collected = int.Parse(Collected.Text) - 1;
                    Collected.Text = collected.ToString();
                    if (collected < 10)
                        Collected.Margin = new Thickness(0, 0, 20, 0);

                    PromiseCharm.Click -= Item_Return;

                    PromiseCharm.Click += Item_Click;
                }
            }
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            Collected.Text = "0";
            Collected.Margin = new Thickness(0, 0, 20, 0);

            if (selected != null)
                selected.BorderBrush = Brushes.White;
            selected = null;

            for (int i = 0; i < Grids.Count; ++i)
            {
                for (int j = Grids[i].Children.Count - 1; j >= 0; --j)
                {
                    Button item = Grids[i].Children[j] as Button;
                    Grids[i].Children.Remove(Grids[i].Children[j]);
                    test.Children.Add(item);

                    item.Click -= Item_Return;
                    item.Click += Item_Click;
                }

                Grids[i].Rows = 1;
                Grids[i].Height = 40;
            }

            for (int i = 0; i < Hints.Count; ++i)
            {
                Hints[i].Text = "?";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageDown && selected != null)
            {
                for(int i = 0; i < Hints.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        if (selected == TWTNW)
                            HandleReportValue(Hints[i], true, -1);
                        else
                            HandleReportValue(Hints[i], false, -1);
                    }
                }
            }
            if (e.Key == Key.PageUp && selected != null)
            {
                for (int i = 0; i < Hints.Count; ++i)
                {
                    if (Worlds[i] == selected)
                    {
                        if (selected == TWTNW)
                            HandleReportValue(Hints[i], true, 1);
                        else
                            HandleReportValue(Hints[i], false, 1);
                    }
                }
            }
        }
    }
}

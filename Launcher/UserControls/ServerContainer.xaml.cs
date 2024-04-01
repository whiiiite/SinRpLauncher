using Launcher.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SinRpLauncher
{
    /// <summary>
    /// Interaction logic for ServerContainer.xaml
    /// </summary>
    public partial class ServerContainer : UserControl
    {
        public ServerContainer()
        {
            InitializeComponent();
        }


        private static Brush BorderBrushTmp;


        public string ServerNameText
        {
            get { return (string)GetValue(ServerNameDP); }
            set { SetValue(ServerNameDP, value); }
        }


        /// <summary>
        /// Current count of players
        /// </summary>
        public uint CurrentPlayersCount
        {
            get { return (uint)GetValue(CurrentPlayersCountDP); }
            set { SetValue(CurrentPlayersCountDP, value); }
        }


        /// <summary>
        /// Max players count
        /// </summary>
        public uint AllPlayersCount
        {
            get { return (uint)GetValue(AllPlayersCountDP); }
            set { SetValue(AllPlayersCountDP, value); }
        }


        /// <summary>
        /// Return true if server picked
        /// </summary>
        public bool ServerPicked
        {
            get { return (bool)GetValue(ServerPickedDP); }
            set { SetValue(ServerPickedDP, value); }
        }


        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveDP); }
            set { SetValue(IsActiveDP, value); }
        }


        public static readonly DependencyProperty ServerNameDP = 
            DependencyProperty.Register("ServerName", typeof(string), typeof(ServerContainer), new PropertyMetadata("Server #1", OnServerNameChanged));

        public static readonly DependencyProperty CurrentPlayersCountDP =
            DependencyProperty.Register("CurrentPlayersCount", typeof(uint), typeof(ServerContainer), 
                new PropertyMetadata(new PropertyChangedCallback(OnCurrentPlayersCountChanged)));

        public static readonly DependencyProperty AllPlayersCountDP =
            DependencyProperty.Register("AllPlayersCount", typeof(uint), typeof(ServerContainer), new PropertyMetadata(OnAllPlayersCountChanged));

        public static readonly DependencyProperty ServerPickedDP =
            DependencyProperty.Register("ServerPicked", typeof(bool), typeof(ServerContainer), new PropertyMetadata(true, new PropertyChangedCallback(OnServerPickedChanged)));

        public static readonly DependencyProperty IsActiveDP =
            DependencyProperty.Register("IsActiveDP", typeof(bool), typeof(ServerContainer), new PropertyMetadata(true, new PropertyChangedCallback(OnIsActiveChanged)));


        public static void OnServerNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerContainer c = d as ServerContainer;
            c.ServerName.Content = (string)e.NewValue;
        }


        public static void OnCurrentPlayersCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerContainer c = d as ServerContainer;
            c.CountPlayersNow.Text = ((uint)e.NewValue).ToString();
            c.ProgressFillPlayers.Value = (uint)e.NewValue;
        }


        public static void OnAllPlayersCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerContainer c = d as ServerContainer;
            c.ProgressFillPlayers.Maximum = (uint)e.NewValue;
            c.CountPlayersAll.Text = ((uint)e.NewValue).ToString();
        }


        public static void OnServerPickedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerContainer c = d as ServerContainer;
            bool val = (bool)e.NewValue;
            if(val == true)
            {
                c.PickedIndicatorBorder.Visibility = Visibility.Visible;
            }
            else
            {
                c.PickedIndicatorBorder.Visibility = Visibility.Hidden;
            }
        }


        public static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ServerContainer c = d as ServerContainer;
            bool val = (bool)e.NewValue;
            if (val == true)
            {
            }
            else
            {
                c.IsEnabled = false;
                c.ServerName.Content = "Not active";
                c.CurrentPlayersCount = 0;
                c.CountPlayersAll.Text = "0";
            }
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentPlayersCount = 0;
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for ProfileContainer.xaml
    /// </summary>
    public partial class ProfileContainer : UserControl
    {
        public ProfileContainer()
        {
            InitializeComponent();
        }


        #region Props
        public new double Width
        {
            get { return (double)GetValue(WidthDP); }
            set { SetValue(WidthDP, value); }   
        }

        public new double Height
        {
            get { return (double)GetValue(HeightDP); }
            set { SetValue(HeightDP, value); }
        }

        public string ProfileNameText
        {
            get { return (string)GetValue(ProfileNameTextDP); }
            set { SetValue(ProfileNameTextDP, value); }
        }
        
        public byte PickedServer
        {
            get { return (byte)GetValue(PickedServerDP); }
            set { SetValue(PickedServerDP, value); }
        }

        public string NicknameText
        {
            get { return (string)GetValue(NicknameTextDP); }
            set { SetValue(NicknameTextDP, value); }
        }

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundDP); }
            set { SetValue(BackgroundDP, value); }
        } 

        public new SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundDP); }
            set { SetValue(ForegroundDP, value); }
        }
        #endregion


        #region Dependency Props
        public static DependencyProperty WidthDP =
            DependencyProperty.Register("WidthDP", typeof(double), typeof(ProfileContainer), new PropertyMetadata(600.0, OnWidthChanged));

        public static DependencyProperty HeightDP =
            DependencyProperty.Register("HeightDP", typeof(double), typeof(ProfileContainer), new PropertyMetadata(20.0, OnHeightChanged));

        public static DependencyProperty ProfileNameTextDP =
            DependencyProperty.Register("ProfileNameTextDP", typeof(string), typeof(ProfileContainer), new PropertyMetadata("", OnProfileNameTextChanged));

        public static DependencyProperty PickedServerDP =
            DependencyProperty.Register("PickedServerDP", typeof(byte), typeof(ProfileContainer), new PropertyMetadata((byte)0, OnPickedProfileChanged));

        public static DependencyProperty NicknameTextDP =
            DependencyProperty.Register("NicknameTextDP", typeof(string), typeof(ProfileContainer), new PropertyMetadata("", OnNicknameTextChanged));

        public static DependencyProperty BackgroundDP =
            DependencyProperty.Register("BackgroundDP", typeof(SolidColorBrush), typeof(ProfileContainer), new PropertyMetadata(new SolidColorBrush(Colors.White), OnBackgroundChanged));

        public static DependencyProperty ForegroundDP = 
            DependencyProperty.Register("ForegroundDP", typeof(SolidColorBrush), typeof(ProfileContainer), new PropertyMetadata(new SolidColorBrush(Colors.White), OnForegroundChanged));
        #endregion


        #region Callback Handlers
        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            double valPerBlock = (double)e.NewValue / 3.0d;

            profileContainer.Container.Width = (double)e.NewValue;

            profileContainer.ProfileNameTxtBlock.Width = valPerBlock ;
            profileContainer.ServerTxtBlock.Width = valPerBlock;
            profileContainer.NicknameTxtBlock.Width = valPerBlock;
        }


        private static void OnHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            profileContainer.Container.Height = (double)e.NewValue;
        }


        private static void OnProfileNameTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            profileContainer.ProfileNameTxtBlock.Text = (string)e.NewValue;
        }


        private static void OnPickedProfileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            profileContainer.ServerTxtBlock.Text = ((byte)e.NewValue).ToString();
        }


        private static void OnNicknameTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            profileContainer.NicknameTxtBlock.Text = (string)e.NewValue;
        }


        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;
            profileContainer.Container.Background = (SolidColorBrush)e.NewValue;
        }


        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProfileContainer profileContainer = (ProfileContainer)d;

            profileContainer.ProfileNameTxtBlock.Foreground = (SolidColorBrush)e.NewValue;
            profileContainer.ServerTxtBlock.Foreground = (SolidColorBrush)e.NewValue;
            profileContainer.NicknameTxtBlock.Foreground = (SolidColorBrush)e.NewValue;
        }
        #endregion

    }
}

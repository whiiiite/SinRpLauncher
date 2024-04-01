// -----------------------------------------------------------------------------------

// RU.
// Когда-то я работал над этим кодом один..
// Этот код далеко не идеален, но я старался сделать его максимально понятным.
// Некоторые моменты возможно, придется переписать с нуля,
// Ибо они устаревшие, или написаны в спешке и без соблюдения всяких норм.
// Я почти все важные шаги закоментировал, надеюсь код будет приятно и легко читать ^_^.

// В этом коде специально минимально использовались выражения из библиотеки Linq.
// Хоть они и выглядят удобно и коротко. Однако коротко - не значит легко-читаемо.

// Компилировать строго в Visual Studio 2022 и выше. Строго согласно с зависимостями.

// Зависимости: 
//      .net 6.0
//      MonoTorrent v 2.0.7
//      SAMPQuery   v 1.0.2

// -----------------------------------------------------------------------------------


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Launcher.Classes;
using Launcher.Handlers;
using Launcher.Loaders;
using Launcher.Handlers.HotKeysHandlers;
using Image = System.Windows.Controls.Image;
using System.Threading;
using SinRpLauncher;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // This window is a main window of all launcher.
    public partial class MainWindow : Window
    {
        private Ellipse? currentNewsEllipse; // Current news ellipse that picked
        public Ellipse? CurrentNewsEllipse { get { return currentNewsEllipse; } set { currentNewsEllipse = value; } }


        public Image _CurrentNews; // Contains current news in order
        public Image CurrentNews { get { return _CurrentNews; } set { _CurrentNews = value; } }


        private Utils _Utils = new Utils();


        private readonly ThemesHandler _ThemesHandler;

        private MainWindowHandlers mwHandlers;


        private Thread scrollNewsThread { get; set; } // only for inner using


        private ServerContainer _PickedServer;
        public ServerContainer PickedServer { get { return _PickedServer; } set { _PickedServer = value; } }

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                mwHandlers = new MainWindowHandlers(this);
                _ThemesHandler = new ThemesHandler();
                _CurrentNews = NewsImg1;
                CurrentNewsEllipse = Frst1;
                _PickedServer = null;
                
            }
            catch (Exception ex) 
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex); 
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindowsLoader loader = new MainWindowsLoader(this, mwHandlers);

                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));

                loader.LoadWindow();

                // put thread to field for get control over this thread
                scrollNewsThread = mwHandlers.StartScrollNewsThread();
                // we got ServersContainerListBox.SelectedIndex from LoadWindow method
                PickedServer = (ServerContainer)ServersContainerListBox.Items[ServersContainerListBox.SelectedIndex];
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.CloseButtonHnd();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
            finally
            {
                GC.Collect();
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mwHandlers.CloseButtonHnd(); 
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            scrollNewsThread.Interrupt();
            scrollNewsThread = null;
        }


        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.MinimizedButtonHnd(this);
            }
            catch (Exception ex)
             {
                MessageBox.Show(ex.Message);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void ToGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.ToGameButtonClickHnd(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
            
        }


        private void TopBarGrid_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception) 
            {
                // it works fine, but throws exception everytime.
                // We just need skeep this.
            }
        }


        private void PlaceHolderNickNameBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.PlaceHolderNickNameBoxHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
            
        }


        private void NickNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                mwHandlers.NickNameBox_LostFocusHnd(this);
            }
            catch (Exception ex) 
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        /// <summary>
        /// Handler for social media buttons(border buttons) and also nav bar buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SocialMediaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                await mwHandlers.SocialMediaButtonHnd(sender, this);
            }
            catch(Exception ex) 
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void NewsToggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {                
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.ToggleNewsHnd(this, sender);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.SettingsButtonHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
            finally
            {
                GC.Collect();
            }
        }


        private async void ServersContainerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                await mwHandlers.ServersBox_SelectionChangedHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void UserProfiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.UserProfilesButtonHnd(this);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                mwHandlers.Button_MouseEnterHnd(_ThemesHandler, sender);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                mwHandlers.Button_MouseLeaveHnd(_ThemesHandler, sender);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void SocialMediaButton_MouseEnter(object sender, MouseEventArgs e)
        {
            _ThemesHandler.SocialMediaButtonMouseEnter((Button)sender);
        }


        private void SocialMediaButton_MouseLeave(object sender, MouseEventArgs e)
        {
            _ThemesHandler.SocialMediaButtonMouseLeave((Button)sender);
        }


        private void NickNameBox_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                mwHandlers.NickNameBox_MouseEnterHnd(_ThemesHandler, sender);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex); 
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void NickNameBox_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                mwHandlers.NickNameBox_MouseLeaveHnd(_ThemesHandler, sender);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void RefreshCheckConnetionBorderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.RefreshCheckConnetionBorderButtonHnd(this); // handle event
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void ChangeColorControlMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                mwHandlers.PickColorControlHnd(sender, this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void NewsImg_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            LogsSys.WriteSimpleLog(_Utils.GetErrorLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
            mwHandlers.NewsImg_Failed_Download_Hnd();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                MainWindowHotKeysHandler hk = new MainWindowHotKeysHandler(this, mwHandlers);
                hk.HandleHotKeys(e);

                string eventMsg = e.Key + " Hotkey used" + " | " + this.Name;
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, eventMsg);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void NickNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                mwHandlers.NickNameBoxTextChangedHnd();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }

        private void StopDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mwHandlers.StopDownloadButton_ClickHnd();
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }
    }
}
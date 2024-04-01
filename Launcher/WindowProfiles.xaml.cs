using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json;
using System.IO;
using Launcher.Classes;
using Launcher.Handlers.HotKeysHandlers;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for WindowProfiles.xaml
    /// Use as Dialog
    /// </summary>
    public partial class WindowProfiles : Window
    {

        private Utils _Utils = new Utils();
        private Handlers.WindowProfilesHandlers wph;
        private Handlers.ThemesHandler _ThemesHandler;


        private ProfileContainer pickedProfileContainer;
        public ProfileContainer PickedProfileContainer
        {
            get { return pickedProfileContainer; }
            set { pickedProfileContainer = value; }
        }


        private string _pickedProfileNickName;

        public string PickedProfileNickName
        {
            get { return _pickedProfileNickName; }
            set { _pickedProfileNickName = value; }
        }


        private byte _pickedProfileServer;

        public byte PickedProfileServer
        {
            get { return _pickedProfileServer; }
            set { _pickedProfileServer = value; }
        }


        private Dictionary<string, ProfileContainer> _NamesProfilesContainers = new Dictionary<string, ProfileContainer>();

        public Dictionary<string, ProfileContainer> NamesProfilesContainers
        {
            get { return _NamesProfilesContainers; }
            set { _NamesProfilesContainers = value; }
        }
        
        private short lenghtProfiles;

        public short LenghtProfiles
        {
            get { return lenghtProfiles; }
            set { lenghtProfiles = value; }
        }


        public WindowProfiles()
        {
            InitializeComponent();
            _ThemesHandler = new Handlers.ThemesHandler();
            wph = new Handlers.WindowProfilesHandlers(this);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Loaders.ProfilesWindowLoader loader = new Loaders.ProfilesWindowLoader(this);
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log

                loader.LoadWindow();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void CloseBorderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
            
        }


        private void CancelProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // just close it (also it clean from RAM)
                Close();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex); 
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wph.DeleteProfileButtonClickHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void PickProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wph.PickProfileButtonClickHnd(this);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wph.AddProfileButtonClickHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void ProfileContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                wph.ProfileContainerMouseDwnHnd(this, sender);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        #region Handlers
        /// <summary>
        /// Method for set profiles list to window from json
        /// </summary>
        /// <param name="pathToJson"></param>
        public void SetProfilesList(string pathToJson)
        {
            try
            {
                PickProfileButton.IsEnabled = false;
                DeleteProfileButton.IsEnabled = false;
                string jsonString = File.ReadAllText(pathToJson);
                Dictionary<string, List<string>>? profileList = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);
                if (profileList != null)
                {
                    short countProfiles = Convert.ToInt16(profileList["max"][0]);
                    lenghtProfiles = countProfiles;

                    for (short i = 0; i < countProfiles; i++)
                    {
                        #region initialize ProfileContainer
                        ProfileContainer profileContainer = new ProfileContainer();
                        #endregion


                        #region subscribe TextBlocks to events
                        profileContainer.MouseLeftButtonDown += ProfileContainer_MouseLeftButtonDown;
                        #endregion


                        #region set values from profiles
                        profileContainer.ProfileNameText    = profileList["prof" + $"{i}"][0];
                        profileContainer.PickedServer       = byte.Parse(profileList["prof" + $"{i}"][1]);
                        profileContainer.NicknameText       = profileList["prof" + $"{i}"][2];
                        #endregion

                        profileContainer.Foreground = Brushes.White;

                        #region set names to ProfileContainer
                        // from this name we need only i(number) for get it after in cycle
                        profileContainer.Name = $"ProfCont{i}";
                        #endregion


                        #region set ProfileContainers to dict for get it after
                        NamesProfilesContainers.Add(profileContainer.Name, profileContainer);
                        #endregion


                        #region set width to Containers
                        profileContainer.Width = this.Width;
                        #endregion


                        #region set Containers to MainGrid
                        ProfilesContainerGrid.Children.Add(profileContainer);   
                        #endregion


                        #region put TextBlocks to rows and columns
                        Grid.SetRow(profileContainer, i + 1);
                        #endregion


                        #region set Alignments to TextBlocks
                        profileContainer.VerticalAlignment = VerticalAlignment.Center;
                        profileContainer.HorizontalAlignment = HorizontalAlignment.Center;
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
            }
        }


        /// <summary>
        /// Method for add profile to json
        /// </summary>
        /// <returns></returns>
        public bool AddProfile()
        {
            try
            {
                AddProfileWindow addProfWindow = new AddProfileWindow();
                bool? resDialog = addProfWindow.ShowDialog();
                if (resDialog == true)
                {
                    string pathToJson = Utils.GetPathToJsonProfs();
                    addProfWindow.SetNewProfileData(pathToJson);

                    if (addProfWindow.profileAdded) // if profile added, set new data to grid
                    {
                        wph.UpdateProfilesData(pathToJson, NamesProfilesContainers, ProfilesContainerGrid);
                        return true;
                    }
                    else
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
                return false;
            }
        }


        /// <summary>
        ///  Method for changle background colors for profile container
        /// </summary>
        public void SetSelectedProfileColor(SolidColorBrush backColor, ProfileContainer profContainer)
        {
            try
            {
                for (int i = 0; i < LenghtProfiles; i++)
                {
                    if (profContainer.Name.Last().ToString().Contains(i.ToString()))
                    {
                        profContainer.Background = backColor;
                    }
                }
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
            }
        }


        /// <summary>
        /// Method for delete profile from json
        /// </summary>
        public void DeleteProfile()
        {
            try
            {
                string pathToJson = Utils.GetPathToJsonProfs();
                Dictionary<string, List<string>> profileData = GetJsonProfiles(pathToJson);
                if (PickedProfileContainer != null && profileData != null)
                {
                    for (short i = 0; i < LenghtProfiles; i++)
                    {
                        if (PickedProfileContainer.Name.Last().ToString().Contains(i.ToString()))
                        {
                            profileData[$"prof{i}"][0] = "<empty>"; // field name of profile
                            profileData[$"prof{i}"][1] = "1";       // field id server
                            profileData[$"prof{i}"][2] = "<empty>"; // field NickName of account
                        }
                    }
                    string json = JsonSerializer.Serialize(profileData);
                    File.WriteAllText(pathToJson, json);
                    wph.UpdateProfilesData(pathToJson, NamesProfilesContainers, ProfilesContainerGrid);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
            }
        }


        public void PickProfile()
        {
            try
            {
                for (short i = 0; i < LenghtProfiles; i++)
                {
                    if (PickedProfileContainer.Name.Last().ToString().Contains(i.ToString()))
                    {
                        byte pickedServer       = NamesProfilesContainers[$"ProfCont{i}"].PickedServer;
                        string pickedNickName   = NamesProfilesContainers[$"ProfCont{i}"].NicknameText;
                        PickedProfileServer     = pickedServer;
                        PickedProfileNickName   = pickedNickName;
                        return; // exit from function
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
            }
        }


        public Dictionary<string, List<string>> GetJsonProfiles(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                Dictionary<string, List<string>>? profileList = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);
                if (profileList != null)
                    return profileList;
                else
                    return new Dictionary<string, List<string>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null snd", ex);
                return new Dictionary<string, List<string>>();
            }
        }
        #endregion


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            wph.ButtonEnterHnd(sender, _ThemesHandler);
        }


        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            wph.ButtonLeaveHnd(sender, _ThemesHandler);
        }


        private void ChangeColorControlMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent));
                wph.PickColorControlHnd(sender, this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            using (WindowProfilesHotKeysHandler hk = new WindowProfilesHotKeysHandler(this))
            {
                string eventMsg = e.Key + " Hotkey used" + " | " + this.Name;
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, eventMsg); // write log

                hk.HandleHotKeys(e);
            }
        }
    }
}

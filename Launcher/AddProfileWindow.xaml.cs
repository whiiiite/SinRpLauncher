using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using Launcher.Classes;
using Launcher.Handlers.HotKeysHandlers;
using CDialogWindow;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for AddProfileWindow.xaml
    /// Use as Dialog
    /// </summary>
    public partial class AddProfileWindow : Window
    {
        #region props
        private bool _profileAdded = false;

        public bool profileAdded
        {
            get { return _profileAdded; }
            private set { _profileAdded = value; }
        }

        #endregion end props


        private Utils _Utils;

        public AddProfileWindow()
        {
            InitializeComponent();
            _Utils = new Utils();
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }


        private void CloseBorderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        /// <summary>
        /// Load window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaders.AddProfileWindowLoader loader = new Loaders.AddProfileWindowLoader(this);
            LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), 
                sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log

            loader.LoadWindow();
        }


        /// <summary>
        /// Returns dialog result true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch(System.Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }



        #region Handlers
        /// <summary>
        /// Set new profiles data to json file with profiles 
        /// </summary>
        /// <param name="pathToJson"></param>
        public void SetNewProfileData(string pathToJson)
        {
            try
            {
                SetNewProfileDataPrivate(pathToJson);
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex);
            }
        }


        /// <summary>
        /// Set new profiles data to json file with profiles.
        /// Set profileAdded property to true if has no issues.
        /// </summary>
        /// <param name="pathToJson"></param>
        private void SetNewProfileDataPrivate(string pathToJson)
        {
            try
            {
                string jsonString = File.ReadAllText(pathToJson);
                Dictionary<string, List<string>>? profilesData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString);

                if (profilesData != null)
                {
                    foreach (KeyValuePair<string, List<string>> profileData in profilesData)
                    {
                        if (!FieldsIsValid())
                        {
                            DialogWindow msgBox = new DialogWindow();
                            string caption = "Одно из полей возможно пустое.\nТак-же поля не могут быть больше 20-ти символов.";
                            msgBox.ShowCustomDialogWindow(caption, "Ошибка", DialogWindowButtons.Ok);
                            break;
                        }

                        string profileKey = profileData.Key;
                        List<string> profilesListData = profileData.Value;

                        if (profileKey == "max") // max value only contains number of all profiles
                            continue;

                        // if nickanme and name is <empty> we can add new profile
                        if (profilesListData[2] == "<empty>" || profilesListData[0] == "<empty>")
                        {
                            SetProfileData(profilesData, profileKey);
                            profileAdded = true;
                            break;
                        }
                    }
                }
                if (profileAdded) // if profile added serialize data and write it to json
                {
                    string serializedProfilesData = JsonSerializer.Serialize(profilesData);
                    File.WriteAllText(pathToJson, serializedProfilesData);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex);
            }
        }


        private bool FieldsIsValid()
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(ProfileNameTextBox.Text) || string.IsNullOrWhiteSpace(ProfileNickNameTextBox.Text)) ||
                  (ProfileNameTextBox.Text.Length > 20 || ProfileNickNameTextBox.Text.Length > 20))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex);
                return false;
            }
        }


        /// <summary>
        /// Set profile data to json file with profiles. Get dict with all profiles 
        /// and key with empty values. And set there new data
        /// </summary>
        /// <param name="DictProfiles"></param>
        /// <param name="profileKey"></param>
        private void SetProfileData(object DictProfiles, string profileKey)
        {
            try
            {
                Dictionary<string, List<string>> profilesData = (Dictionary<string, List<string>>)DictProfiles;

                profilesData[profileKey][0] = ProfileNameTextBox.Text;
                profilesData[profileKey][1] = (ProfileServersBox.SelectedIndex + 1).ToString();
                profilesData[profileKey][2] = ProfileNickNameTextBox.Text;
            }
            catch(Exception ex)
            {
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex);
            }
        }
        #endregion


        private void AddProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
            }
            catch(Exception ex)
            {
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }

        private void AddProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            AddProfileWindowHotKeysHandler hk = new AddProfileWindowHotKeysHandler(this);
            string eventMsg = e.Key + " Hotkey used" + " | " + this.Name;
            LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, eventMsg); // write log
            hk.HandleHotKeys(e);
        }


        private void ProfileNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProfileNameTextBox.Text) || ProfileNameTextBox.Text.Length > 20)
            {
                WarnNotValidFields("Имя профиля невалидно. \nОно не может быть более 20 символов Или пустым");
                AddProfileButton.IsEnabled = false;
            }
            else
            {
                WarnNotValidFields("");
                AddProfileButton.IsEnabled = true;
            }
        }


        private void ProfileNickNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProfileNickNameTextBox.Text) || ProfileNickNameTextBox.Text.Length > 20)
            {
                WarnNotValidFields("Никнейм невалиден. \nОн не может быть более 20 символов Или пустым");
                AddProfileButton.IsEnabled = false;
            }
            else
            {
                WarnNotValidFields("");
                AddProfileButton.IsEnabled = true;
            }
        }


        private void WarnNotValidFields(string warnText)
        {
            ErrorBox.Text = warnText;
        }
    }
}

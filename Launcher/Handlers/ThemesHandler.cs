using System;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using Launcher.Classes;
using Launcher.BaseClasses;
using System.Xml;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SinRpLauncher;
using System.Windows.Shapes;
using System.Windows.Input;
using SinRpLauncher.Util;

namespace Launcher.Handlers
{
    /// <summary>
    /// Represents windows of the launcher
    /// </summary>
    enum Wnds : int
    {
        main = 0, settings = 1, profiles = 2, addprofile = 3,
    }

    /// <summary>
    /// Class that contains methods and props for theme of the launcher
    /// </summary>
    public class ThemesHandler : BaseHandler
    {


        // region for some experiments
        #region Experemental

        private const string rootNode               = "Root";  // root node in xml
        private const string wndNameNode            = "window-color-theme";  // node contains info color theme
        private const string wndNameAttr            = "windowname";  // attribute contains window name
        private const string colorSetNode           = "color-set";  // node contains color set bg and fg
        private const string colorSetElemNameAttr   = "elementname";  // attribute contains name of control
        private const string bgNode                 = "background";  // node contains background in color set
        private const string fgNode                 = "foreground";  // node contains foreground in color set
        private const string colorAttr              = "color";  // attribute contains color in color from color set

        private string[] wndTags = new string[] {
            "main", "settings", "profiles", "addprofile"
        };

        // dictionary that contains value of attribute
        // element name in color-set tag
        // main window
        private readonly List<string> mwSetsTags = new List<string>() {

            "MAINGRID"              ,
            "CLOSEBTN"              ,
            "MINIMIZEBTN"           ,
            "SETTINGSBTN"           ,
            "TOGAMEBTN"             ,
            //"PROFSBTN"              ,
            "PERSCABBTN"            ,
            "FORUMBTN"              ,
            "TECHSUPBTN"            ,
            "PROJLABEL"             ,
            "SUBPROJLABEL"          ,
            "TXTBLCKUPDATES"        ,
            "SUBTXTBLCKUPDATES"     ,
            "NICKBOXIMGCONTAINER"   ,
            "NICKINPUTCONTAINER"    ,
            "PLCHOLDERNCNM"         ,
            "UPDPRGRSBAR"           ,
            "REFRBTN"               ,
            "DSBTN"                 ,
            "VKBTN"                 ,
            "YTBTN"                 ,
            "SV1"                   ,
            "SV2"                   ,
        };


        /// <summary>
        /// Loads color theme to main window from xml file
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void LoadColorThemeToMainWindow(string xmlPath, MainWindow w)
        {
            BrushConverter conv = new BrushConverter();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            // get xml node of window 
            XmlNode mwNode = GetWindowThemeByName(doc, wndTags[(int)Wnds.main]);

            foreach (string tag in mwSetsTags)
            {
                string elem = tag; // rename

                // get color set by element name
                XmlNode colorsSet   = GetColorsSetsByElementName(mwNode, elem);
                // returns list of hex colors
                List<string> colors = GetColorsFromColorsSets(colorsSet);

                // check that hex code of color is correct
                // if is false -> fix this to normal code
                colors[0] = Utils.FixHexColorCodeIfIncorrect(colors[0]);
                colors[1] = Utils.FixHexColorCodeIfIncorrect(colors[1]);

                // convert hex colors to brushes for controls
                Brush background, foreground;
                background = (Brush?)conv.ConvertFrom(colors[0]) ?? Brushes.Black;
                foreground = (Brush?)conv.ConvertFrom(colors[1]) ?? Brushes.Black;

                // find current element and set color to this
                switch (elem)
                {
                    case "MAINGRID":
                        w.MainGrid.Background = background;
                        break;

                    case "CLOSEBTN":
                        //w.CloseButton.Background = background;
                        w.CloseButton.Foreground = foreground;
                        break;

                    case "MINIMIZEBTN":
                        //w.MinimizeButton.Background = background;
                        w.MinimizeButton.Foreground = foreground;
                        break;

                    case "SETTINGSBTN":
                        w.SettingsButton.Background = background;
                        break;

                    case "TOGAMEBTN":
                        w.toGameButton.Background = background;
                        w.toGameButton.Foreground = foreground;
                        break;

                    //case "PROFSBTN":
                    //    w.NavBarUserProfilesButton.Background = background;
                    //    w.NavBarUserProfilesButton.Foreground = foreground;
                    //    break;

                    case "PERSCABBTN":
                        w.NavBarCabinetButton.Background = background;
                        w.NavBarCabinetButton.Foreground = foreground;
                        break;

                    case "FORUMBTN":
                        w.NavBarForumButton.Background = background;
                        w.NavBarForumButton.Foreground = foreground;
                        break;

                    case "TECHSUPBTN":
                        w.NavBarTechSupportButton.Background = background;
                        w.NavBarTechSupportButton.Foreground = foreground;
                        break;

                    case "PROJLABEL":
                        w.ProjectLabel.Foreground = foreground;
                        break;

                    case "SUBPROJLABEL":
                        w.SubProjectLabel.Foreground = foreground;
                        w.SubProjectLabel.Opacity = 0.4;
                        break;

                    case "TXTBLCKUPDATES":
                        w.TextBlockUpdates.Foreground = foreground;
                        break;

                    case "SUBTXTBLCKUPDATES":
                        w.SubTextBlockUpdates.Foreground = foreground;
                        break;

                    case "NICKBOXIMGCONTAINER":
                        w.NickNameBoxImgBorder.Background = background;
                        break;

                    case "NICKINPUTCONTAINER":
                        w.NicknameInputContainer.Background = background;
                        w.NicknameInputContainer.Background.Opacity = 0.4;
                        w.NickNameBox.Foreground = foreground;
                        w.NickNameBox.CaretBrush = foreground;
                        break;

                    case "PLCHOLDERNCNM":
                        w.PlaceHolderNickNameBox.Foreground = foreground;
                        break;

                    case "UPDPRGRSBAR":
                        w.UpdateProgressBar.Background = background;
                        w.UpdateProgressBar.Foreground = foreground;
                        break;

                    case "REFRBTN":
                        w.RefreshCheckConnetionBorderButton.Background = background;
                        break;

                    case "DSBTN":
                        w.DiscordButton.Background = background;
                        break;

                    case "VKBTN":
                        w.VkButton.Background = background;
                        break;

                    case "YTBTN":
                        w.YoutubeButton.Background = background;
                        break;

                    case "SV1":
                        SetColorsToServerContainer(w.Server_1, foreground, background);
                        break;

                    case "SV2":
                        SetColorsToServerContainer(w.Server_2, foreground, background);
                        break;

                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// Write new data theme to xml file with theme
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void SetNewMainWindowColorsThemeToXML(string xmlPath, MainWindow w)
        {
            XmlWriterSettings xmlSets = new XmlWriterSettings();
            xmlSets.Indent = true;
            xmlSets.CloseOutput = true;

            using (var xmlWriter = XmlWriter.Create(xmlPath, xmlSets))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(rootNode);
                xmlWriter.WriteStartElement(wndNameNode);
                xmlWriter.WriteAttributeString(wndNameAttr, wndTags[(int)Wnds.main]);

                foreach (string item in mwSetsTags)
                {
                    string key = item;

                    // 0 - foreground, 1 - background
                    List<string> colors = new List<string>();

                    // checks key and get colors for control
                    // after this - write colors to XML
                    switch (key)
                    {
                        case "MAINGRID":
                            colors = GetColorsByControl(w.MainGrid);                            break;

                        case "CLOSEBTN":
                            colors = GetColorsByControl(w.CloseButton);                         break;

                        case "MINIMIZEBTN":
                            colors = GetColorsByControl(w.MinimizeButton);                      break;

                        case "SETTINGSBTN":
                            colors = GetColorsByControl(w.SettingsButton);                      break;

                        case "TOGAMEBTN":
                            colors = GetColorsByControl(w.toGameButton);                        break;

                        //case "PROFSBTN":
                        //    colors = GetColorsByControl(w.NavBarUserProfilesButton);            break;

                        case "PERSCABBTN":
                            colors = GetColorsByControl(w.NavBarCabinetButton);                 break;

                        case "FORUMBTN":
                            colors = GetColorsByControl(w.NavBarForumButton);                   break;

                        case "TECHSUPBTN":
                            colors = GetColorsByControl(w.NavBarTechSupportButton);             break;

                        case "PROJLABEL":
                            colors = GetColorsByControl(w.ProjectLabel);                        break;

                        case "SUBPROJLABEL":
                            colors = GetColorsByControl(w.SubProjectLabel);                     break;

                        case "NICKBOXIMGCONTAINER":
                            colors = GetColorsByControl(w.NickNameBoxImgBorder);                break;

                        case "TXTBLCKUPDATES":
                            colors = GetColorsByControl(w.TextBlockUpdates);                    break;

                        case "SUBTXTBLCKUPDATES":
                            colors = GetColorsByControl(w.SubTextBlockUpdates);                 break;

                        case "NICKINPUTCONTAINER":
                            colors = GetColorsByControl(w.NicknameInputContainer);              break;

                        case "PLCHOLDERNCNM":
                            colors = GetColorsByControl(w.PlaceHolderNickNameBox);              break;

                        case "UPDPRGRSBAR":
                            colors = GetColorsByControl(w.UpdateProgressBar);                   break;

                        case "REFRBTN":
                            colors = GetColorsByControl(w.RefreshCheckConnetionBorderButton);   break;

                        case "DSBTN":
                            colors = GetColorsByControl(w.DiscordButton);                       break;

                        case "VKBTN":
                            colors = GetColorsByControl(w.VkButton);                            break;

                        case "YTBTN":
                            colors = GetColorsByControl(w.YoutubeButton);                       break;

                        case "SV1":
                            colors = GetColorsByControl(w.Server_1);                            break;

                        case "SV2":
                            colors = GetColorsByControl(w.Server_2);                            break;
                    }
                    WriteColorSetToXml(xmlWriter, key, colors[0], colors[1]);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }


        // dictionary that contains value of attribute
        // element name in color-set tag
        // settings window
        private readonly List<string> swSetsTags = new List<string>()
        {
            "MAINGRID"          ,
            "CLOSEBTN"          ,
            "FILESRCBTN"        ,
            "GAMEPATHTXTBOX"    ,
            "CANCELBTN"         ,
            "APPLYBTN"          ,
        };


        /// <summary>
        /// Loads color theme to settings window from xml file
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void LoadColorThemeToSettingsWindow(string xmlPath, WindowSettings w)
        {
            BrushConverter conv = new BrushConverter();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            // get xml node of settings window 
            XmlNode swNode = GetWindowThemeByName(doc, wndTags[(int)Wnds.settings]);

            foreach (string tag in swSetsTags)
            {
                string elem = tag;

                // get color set by element name
                XmlNode colorsSet = GetColorsSetsByElementName(swNode, elem);
                // returns list of hex colors
                List<string> colors = GetColorsFromColorsSets(colorsSet);

                // check that hex code of color is correct
                // if is false -> fix this to normal code
                colors[0] = Utils.FixHexColorCodeIfIncorrect(colors[0]);
                colors[1] = Utils.FixHexColorCodeIfIncorrect(colors[1]);

                // convert hex colors to brushes for controls
                Brush background, foreground;
                background = (Brush?)conv.ConvertFrom(colors[0]) ?? Brushes.Black;
                foreground = (Brush?)conv.ConvertFrom(colors[1]) ?? Brushes.Black;

                // find current element and set color to this
                switch (elem)
                {
                    case "MAINGRID":
                        w.MainGrid.Background = background;
                        break;
                    case "CLOSEBTN":
                        w.CloseButton.Background = background;
                        w.CloseButton.Foreground = foreground;
                        break;
                    case "FILESRCBTN":
                        w.FileSourceButton.Background = background;
                        w.FileSourceButton.Foreground = foreground;
                        break;
                    case "GAMEPATHTXTBOX":
                        w.GamePathTextBox.Background = background;
                        w.GamePathTextBox.Foreground = foreground;
                        w.GamePathTextBox.CaretBrush = foreground;
                        break;
                    case "CANCELBTN":
                        w.CancelSettingsButton.Background = background;
                        w.CancelSettingsButton.Foreground = foreground;
                        break;
                    case "APPLYBTN":
                        w.ApplySettingsButton.Background = background;
                        w.ApplySettingsButton.Foreground = foreground;
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// Sets new color theme to settings window
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void SetNewSettingsWindowColorsThemeToXML(string xmlPath, WindowSettings w)
        {
            XmlWriterSettings xmlSets = new XmlWriterSettings();
            xmlSets.Indent = true;
            xmlSets.CloseOutput = true;

            using (var xmlWriter = XmlWriter.Create(xmlPath, xmlSets))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(rootNode);
                xmlWriter.WriteStartElement(wndNameNode);
                xmlWriter.WriteAttributeString(wndNameAttr, wndTags[(int)Wnds.settings]);

                foreach (string item in swSetsTags)
                {
                    string key = item;

                    // 0 - foreground, 1 - background
                    List<string> colors = new List<string>();

                    // checks key and get colors for control
                    // after this - write colors to XML
                    switch (key)
                    {
                        case "MAINGRID":
                            colors = GetColorsByControl(w.MainGrid);                break;
                        case "CLOSEBTN":
                            colors = GetColorsByControl(w.CloseButton);             break;
                        case "FILESRCBTN":
                            colors = GetColorsByControl(w.FileSourceButton);        break;
                        case "GAMEPATHTXTBOX":
                            colors = GetColorsByControl(w.GamePathTextBox);         break;
                        case "CANCELBTN":
                            colors = GetColorsByControl(w.CancelSettingsButton);    break;
                        case "APPLYBTN":
                            colors = GetColorsByControl(w.ApplySettingsButton);     break;
                        default:
                            break;
                    }
                    WriteColorSetToXml(xmlWriter, key, colors[0], colors[1]);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }


        // dictionary that contains value of attribute
        // element name in color-set tag
        // main window
        private readonly List<string> profsSetsTags = new List<string>() {
            "MAINGRID"      ,
            "CLOSEBTN"      ,
            "PROFSLABEL"    ,
            "PROFNAMETXT"   ,
            "SRVRTXT"       ,
            "NCKNMTXT"      ,
            "CNCLBTN"       ,
            "DELBTN"        ,
            "ADDBTN"        ,
            "PICKBTN"       ,
        };


        /// <summary>
        /// Loads color theme to profiles window from xml file
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void LoadColorThemeToProfilesWindow(string xmlPath, WindowProfiles w)
        {
            BrushConverter conv = new BrushConverter();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            // get xml node of window 
            XmlNode pwNode = GetWindowThemeByName(doc, wndTags[(int)Wnds.profiles]);

            foreach (string tag in profsSetsTags)
            {
                string elem = tag;

                // get color set by element name
                XmlNode colorsSet = GetColorsSetsByElementName(pwNode, elem);
                // returns list of hex colors
                List<string> colors = GetColorsFromColorsSets(colorsSet);

                // check that hex code of color is correct
                // if is false -> fix this to normal code
                colors[0] = Utils.FixHexColorCodeIfIncorrect(colors[0]);
                colors[1] = Utils.FixHexColorCodeIfIncorrect(colors[1]);

                // convert hex colors to brushes for controls
                Brush background, foreground;
                background = (Brush?)conv.ConvertFrom(colors[0]) ?? Brushes.Black;
                foreground = (Brush?)conv.ConvertFrom(colors[1]) ?? Brushes.Black;

                // find current element and set color to this
                switch (elem)
                {
                    case "MAINGRID":
                        w.MainGrid.Background = background;
                        break;
                    case "CLOSEBTN":
                        w.CloseButton.Background = background;
                        w.CloseButton.Foreground = foreground;
                        break;
                    case "PROFSLABEL":
                        w.ProfileWinLabel.Foreground = foreground;
                        break;
                    case "PROFNAMETXT":
                        w.ProfileNickNameTextBlock.Foreground = foreground;
                        break;
                    case "SRVRTXT":
                        w.ProfileServerTextBlock.Foreground = foreground;
                        break;
                    case "NCKNMTXT":
                        w.ProfileNickNameTextBlock.Foreground = foreground;
                        break;
                    case "CNCLBTN":
                        w.CancelProfileButton.Background = background;
                        w.CancelProfileButton.Foreground = foreground;
                        break;
                    case "DELBTN":
                        w.DeleteProfileButton.Background = background;
                        w.DeleteProfileButton.Foreground = foreground;      
                        break;
                    case "ADDBTN":
                        w.AddProfileButton.Background = background;
                        w.AddProfileButton.Foreground = foreground;
                        break;
                    case "PICKBTN":
                        w.PickProfileButton.Background = background;
                        w.PickProfileButton.Foreground = foreground;
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// Sets new color theme to settings window
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="w"></param>
        public void SetNewProfileWindowColorsThemeToXML(string xmlPath, WindowProfiles w)
        {
            XmlWriterSettings xmlSets = new XmlWriterSettings();
            xmlSets.Indent = true;
            xmlSets.CloseOutput = true;

            using (var xmlWriter = XmlWriter.Create(xmlPath, xmlSets))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(rootNode);
                xmlWriter.WriteStartElement(wndNameNode);
                xmlWriter.WriteAttributeString(wndNameAttr, wndTags[(int)Wnds.profiles]);

                foreach (string item in profsSetsTags)
                {
                    string key = item;

                    // 0 - foreground, 1 - background
                    List<string> colors = new List<string>();

                    // checks key and get colors for control
                    // after this - write colors to XML
                    switch (key)
                    {
                        case "MAINGRID":
                            colors = GetColorsByControl(w.MainGrid);                    break;
                        case "CLOSEBTN":
                            colors = GetColorsByControl(w.CloseButton);                 break;
                        case "PROFSLABEL":
                            colors = GetColorsByControl(w.ProfileWinLabel);             break;
                        case "PROFNAMETXT":
                            colors = GetColorsByControl(w.ProfileNickNameTextBlock);    break;
                        case "SRVRTXT":
                            colors = GetColorsByControl(w.ProfileServerTextBlock);      break;
                        case "NCKNMTXT":
                            colors = GetColorsByControl(w.ProfileNickNameTextBlock);    break;
                        case "CNCLBTN":
                            colors = GetColorsByControl(w.CancelProfileButton);         break;
                        case "DELBTN":
                            colors = GetColorsByControl(w.DeleteProfileButton);         break;
                        case "ADDBTN":
                            colors = GetColorsByControl(w.AddProfileButton);            break;
                        case "PICKBTN":
                            colors = GetColorsByControl(w.PickProfileButton);           break;
                        default:
                            break;
                    }
                    WriteColorSetToXml(xmlWriter, key, colors[0], colors[1]);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }


        /// <returns>Xml node of colors set by name</returns>
        public XmlNode GetColorsSetsByElementName(XmlNode winNode, string elemName)
        {
            XmlNodeList colorsSet = winNode.ChildNodes;

            for (int i = 0; i < colorsSet.Count; i++)
            {
                XmlNode node = colorsSet[i];
                string curNameTag = node?.Attributes?[colorSetElemNameAttr]?.Value ?? "";
                if (curNameTag == elemName && node != null)
                    return node;
            }
            return null;
        }


        /// <summary>
        /// Get xml node for get colors of colors set for put it to control
        /// </summary>
        /// <returns>Colors of specific colors set background and foreground</returns>
        public List<string> GetColorsFromColorsSets(XmlNode colorsSet)
        {
            List<string> colors = new List<string>();
            XmlNodeList listColors = colorsSet.ChildNodes;

            for (int i = 0; i < listColors?.Count; i++)
            {
                colors.Add(listColors[i]?.Attributes?[colorAttr]?.Value ?? "#000000");
            }
            return colors;
        }


        /// <returns>Xml node that contains color sets for window controls</returns>
        public XmlNode GetWindowThemeByName(XmlDocument doc, string winName)
        {
            XmlNodeList wnd = doc.GetElementsByTagName(wndNameNode);

            for (int i = 0; i < wnd?.Count; i++)
            {
                XmlNode node = wnd[i];
                string curWinName = node?.Attributes?[wndNameAttr]?.Value ?? "";
                if (curWinName == winName && node != null)
                    return node;
            }
            return null;
        }


        /// <summary>
        /// Open custom pick color dialog. For pick background & foreground for control
        /// </summary>
        /// <param name="sender">menu item</param>
        public void PickControlColors(MenuItem m, bool offFG = false, bool offBG = false)
        {
            // get the parent of menu item
            UIElement parentMenuItem = ((ContextMenu)m.Parent).PlacementTarget;

            // object for handle some changes in control
            // this row - just rename parentMenuItem for easier work
            UIElement owner = parentMenuItem;

            // object of pick color dialog
            PickColorDialog pickColorDialog = new PickColorDialog();
            pickColorDialog.Topmost = true;

            // set color to default
            Color ownerFGColor = default(Color);
            Color ownerBGColor = default(Color);

            // checks what object we got and set foreground and background
            // as default for dialog colors
            // if control do not need to be color back or fore ground - just hide needed pickers
            if(owner is FrameworkElement)
            {
                if (offBG)
                {
                    pickColorDialog.BackgroundColor.Visibility = Visibility.Hidden;
                    pickColorDialog.BGColor = Color.FromArgb(0, 0, 0, 0);
                }
                if(offFG)
                {
                    pickColorDialog.ForegroundColor.Visibility = Visibility.Hidden;
                    pickColorDialog.FGColor = Color.FromArgb(0, 0, 0, 0);
                }


                if (owner is TextBlock)
                {
                    TextBlock textBlock = (TextBlock)owner;
                    pickColorDialog.BackgroundColor.Visibility = Visibility.Hidden;
                    pickColorDialog.BGColor = Color.FromArgb(0, 0, 0, 0);
                    ownerFGColor = ((SolidColorBrush)textBlock.Foreground).Color;
                }
                else if (owner is Control && owner is not ServerContainer)
                {
                    Control c = (Control)owner;
                    if(c is Label)
                    {
                        pickColorDialog.BackgroundColor.Visibility = Visibility.Hidden;
                        pickColorDialog.BGColor = Color.FromArgb(0, 0, 0, 0);
                        ownerFGColor = ((SolidColorBrush)c.Foreground).Color;
                    }
                    else
                    {
                        ownerFGColor = ((SolidColorBrush)c.Foreground).Color;
                        ownerBGColor = ((SolidColorBrush)c.Background).Color;
                    }
                }
                else if(owner is Panel)
                {
                    Panel p = (Panel)owner;
                    pickColorDialog.ForegroundColor.Visibility = Visibility.Hidden;
                    ownerBGColor = ((SolidColorBrush)p.Background).Color;
                }
                else if(owner is Border)
                {
                    Border brd = (Border)owner;
                    pickColorDialog.ForegroundColor.Visibility = Visibility.Hidden;
                    ownerBGColor = ((SolidColorBrush)brd.Background).Color;
                }
                else if (owner is ServerContainer)
                {
                    ServerContainer sc = (ServerContainer)owner;
                    ownerBGColor = ((SolidColorBrush)sc.PickedIndicatorBorder.Background).Color;
                    ownerFGColor = ((SolidColorBrush)sc.CountPlayersAll.Foreground).Color;
                }
            }

            string ownerFGHex = GetHexColor(ownerFGColor);
            string ownerBGHex = GetHexColor(ownerBGColor);

            // change hex code text box text
            // when it changes. Work event TextChanged.
            // And color is changing in dialog
            pickColorDialog.ForegroundColor.HexCodeBox.Text = ownerFGHex;
            pickColorDialog.BackgroundColor.HexCodeBox.Text = ownerBGHex;

            // result of success pick color dialog
            bool? resColor = pickColorDialog.ShowDialog();

            // if is success
            if (resColor == true)
            {
                // check specified type of owner menu item
                // and set new colors theme
                Color selectedBG = pickColorDialog.BGColor;
                Color selectedFG = pickColorDialog.FGColor;
                
                // check element
                // change color to control 
                if (owner is FrameworkElement)
                {
                    if(owner is TextBlock textBlock)
                    {
                        textBlock.Foreground = new SolidColorBrush(selectedFG);
                    }
                    else if (owner is Control control && owner is not ServerContainer)
                    {
                        control.Background = new SolidColorBrush(selectedBG);
                        control.Foreground = new SolidColorBrush(selectedFG);
                    }
                    else if (owner is Panel panel)
                    {
                        panel.Background = new SolidColorBrush(selectedBG);
                    }
                    else if (owner is Border border)
                    {
                        border.Background = new SolidColorBrush(selectedBG);
                    }
                    else if(owner is ServerContainer serverContainer) 
                    {
                        SolidColorBrush b = new SolidColorBrush(selectedBG);
                        SolidColorBrush f = new SolidColorBrush(selectedFG);
                        serverContainer.ServerName.Foreground                = f;
                        serverContainer.CountPlayersNow.Foreground           = f;
                        serverContainer.CountPlayersAll.Foreground           = f;
                        serverContainer.PickedIndicatorBorder.Background     = b;
                        serverContainer.PickedPolyLine.Fill                  = f;
                        serverContainer.CountPlayersSeparator.Foreground     = f;
                        serverContainer.MainBorderContainer.BorderBrush      = b;
                    }
                }
            }
        }


        /// <summary>
        /// Write color set(background and foreground) to xml file
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="controlName"></param>
        /// <param name="hexFG"></param>
        /// <param name="hexBG"></param>
        /// <returns></returns>
        public int WriteColorSetToXml(XmlWriter writer, string controlName, string hexFG, string hexBG)
        {
            writer.WriteStartElement(colorSetNode);

            writer.WriteAttributeString(colorSetElemNameAttr, controlName);

            writer.WriteStartElement(bgNode);
            writer.WriteAttributeString(colorAttr, hexBG);
            writer.WriteEndElement();
           
            writer.WriteStartElement(fgNode);
            writer.WriteAttributeString(colorAttr, hexFG);
            writer.WriteEndElement();

            writer.WriteEndElement();
            return 0;
        }

        
        /// <summary>
        /// Get colors from control
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>List of 2 elements with hex values of colors. 1st - FG, 2st - BG</returns>
        private List<string> GetColorsByControl(FrameworkElement owner) 
        {
            List<string> hexColors = new List<string>();
            Color FG = default(Color);
            Color BG = default(Color);
            string FGHEX = "#FFFFFF";
            string BGHEX = "#FFFFFF00";

            if (owner is TextBlock)
            {
                TextBlock textBlock = (TextBlock)owner;
                FG = ((SolidColorBrush)textBlock.Foreground).Color;
            }
            if (owner is Control && owner is not ServerContainer)
            {
                Control c = (Control)owner;
                FG = ((SolidColorBrush)c.Foreground).Color;
                BG = ((SolidColorBrush)c.Background).Color;
            }
            else if (owner is Panel)
            {
                Panel p = (Panel)owner;
                BG = ((SolidColorBrush)p.Background).Color;
                FG = (Color)Utils.GetColorConverter().ConvertFrom("#ffff");
            }
            else if (owner is Border)
            {
                Border brd = (Border)owner;
                BG = ((SolidColorBrush)brd.Background).Color;
                FG = (Color)Utils.GetColorConverter().ConvertFrom("#ffff");
            }
            else if(owner is ServerContainer)
            {
                ServerContainer sc = (ServerContainer)owner;
                BG = ((SolidColorBrush)sc.PickedIndicatorBorder.Background).Color;
                FG = ((SolidColorBrush)sc.CountPlayersAll.Foreground).Color;
            }

            FGHEX = GetHexColor(FG);
            BGHEX = GetHexColor(BG);

            hexColors.Add(FGHEX);
            hexColors.Add(BGHEX);

            return hexColors;
        }
        

        /// <summary>
        /// Converts the Color struct to hex string of this color.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetHexColor(Color c)
        {
            return '#'  + Utils.DecimalToHexByte(c.A) + Utils.DecimalToHexByte(c.R) 
                        + Utils.DecimalToHexByte(c.G) + Utils.DecimalToHexByte(c.B);
        }


        /// <summary>
        /// Hnadler when mouse is enter to the button
        /// </summary>
        /// <param name="btn"></param>
        public void ButtonMouseEnter(Button btn)
        {
            ButtonMouseAnimationGeneral(btn, 1.0, 0.6, new TimeSpan(0, 0, 0, 0, 300), "(Button.Opacity)");
        }


        /// <summary>
        /// Hnadler when mouse is leave the button
        /// </summary>
        public void ButtonMouseLeave(Button btn)
        {
            ButtonMouseAnimationGeneral(btn, 0.6, 1.0, new TimeSpan(0, 0, 0, 0, 300), "(Button.Opacity)");
        }


        /// <summary>
        /// General implementation of button animation 
        /// </summary>
        // Resolve the problem code repeat
        // is general method for invoke it while mouse enter and leave
        private void ButtonMouseAnimationGeneral(Button btn, double from, double to, TimeSpan time, string targetPath)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();

            doubleAnimation.From = from;
            doubleAnimation.To = to;
            doubleAnimation.Duration = time;

            PropertyPath colorTargetPath = new PropertyPath(targetPath);
            Storyboard ColorChangeStory = new Storyboard();

            Storyboard.SetTarget(doubleAnimation, btn);
            Storyboard.SetTargetProperty(doubleAnimation, colorTargetPath);

            ColorChangeStory.Children.Add(doubleAnimation);
            ColorChangeStory.Begin();
        }


        /// <summary>
        /// Handler when Mouse enter the Social Media Button
        /// </summary>
        /// <param name="button"></param>
        public void SocialMediaButtonMouseEnter(Button button)
        {
            Color to = (Color)Utils.GetColorConverter().ConvertFrom("#F3A22A"); // to color
            SocialMediaButtonMouseAnimGeneral(button, ((SolidColorBrush)button.BorderBrush).Color, to, 
                new TimeSpan(0, 0, 0, 0, 300), "(Button.BorderBrush).(SolidColorBrush.Color)");
        }


        /// <summary>
        /// Handler when Mouse leave the Social Media Button
        /// </summary>
        public void SocialMediaButtonMouseLeave(Button button)
        {
            Color from = (Color)Utils.GetColorConverter().ConvertFrom("#F3A22A"); // from color
            SocialMediaButtonMouseAnimGeneral(button, from, Brushes.Gray.Color,
                new TimeSpan(0, 0, 0, 0, 300), "(Button.BorderBrush).(SolidColorBrush.Color)");
        }


        /// <summary>
        /// General implementation of social media button animation 
        /// </summary>
        /// <param name="button"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="time"></param>
        /// <param name="targetPath"></param>
        // Resolve the problem code repeat
        // is general method for invoke it while mouse enter and leave
        public void SocialMediaButtonMouseAnimGeneral(Button button, Color from, Color to, TimeSpan time, string targetPath)
        {
            ColorAnimation colorAnimation = new ColorAnimation();

            colorAnimation.From = from;
            colorAnimation.To = to;
            colorAnimation.Duration = time;

            PropertyPath colorTargetPath = new PropertyPath(targetPath);
            Storyboard colorChangeStory = new Storyboard();
            Storyboard.SetTarget(colorAnimation, button);
            Storyboard.SetTargetProperty(colorAnimation, colorTargetPath);
            colorChangeStory.Children.Add(colorAnimation);
            colorChangeStory.Begin();
        }


        /// <summary>
        /// Animate the ellipse of news 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="time"></param>
        /// <param name="targetPath"></param>
        public void ToggleChangeAnim(MainWindow mw, Ellipse start, Ellipse end, TimeSpan time, string targetPath)
        {
            // create fake copy ellipse for animation.
            Ellipse? fakeForAnim = XamlUtil.XamlCloneElement(mw.CurrentNewsEllipse);

            if (fakeForAnim == null) return;

            mw.NewsTogglesContainer.Children.Add(fakeForAnim);
            
            // create animation
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = start.Margin;
            ta.To = end.Margin;
            ta.Duration = time;

            // sets target paths for animations and storyboard for it.
            PropertyPath marginTargetPath = new PropertyPath(targetPath);
            Storyboard marginAnimStory = new Storyboard();
            Storyboard.SetTarget(ta, fakeForAnim);
            Storyboard.SetTargetProperty(ta, marginTargetPath);
            marginAnimStory.Children.Add(ta);

            // !!! EVENTS MUST BE SET ONLY BEFORE BEGIN OF ANIMATION
            marginAnimStory.Completed += (s, e) => // while is complete
            {
                // just delete fake copy from screen and memory
                mw.NewsTogglesContainer.Children.Remove(fakeForAnim);
                fakeForAnim = null;
            };
            marginAnimStory.Begin();
        }


        /// <summary>
        /// Sets colors to server container.
        /// <para>Need for change color in server container. Because if not linear object</para>
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        private void SetColorsToServerContainer(ServerContainer sc, Brush fg, Brush bg)
        {
            sc.ServerName.Foreground                = fg;
            sc.CountPlayersNow.Foreground           = fg;
            sc.CountPlayersAll.Foreground           = fg;
            sc.PickedIndicatorBorder.Background     = bg;
            sc.PickedPolyLine.Fill                  = fg;
            sc.CountPlayersSeparator.Foreground     = fg;
            sc.MainBorderContainer.BorderBrush      = bg;
        }

        #endregion
    }
}

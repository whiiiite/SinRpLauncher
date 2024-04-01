using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Launcher.Classes;
using Launcher.Handlers.HotKeysHandlers;
using SinRpLauncher.Handlers.HotKeysHandlers;

namespace Launcher
{
    public enum DialogWindowButtons
    {
        YesNo, Ok, YesNoCancel
    }

    /// <summary>
    /// Interaction logic for CustomDialogWindow.xaml
    /// Use as dialog
    /// </summary>
    public partial class CustomDialogWindow : Window
    {
        private Utils _Utils;

        public CustomDialogWindow()
        {
            InitializeComponent();
            _Utils = new Utils();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Loaders.CustomDialogWindowLoader loader = new Loaders.CustomDialogWindowLoader(this))
                {
                    loader.LoadWindow();
                }
                StretchBox(MainTextDialog.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex, DateTime.Now);
            }
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AgreeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex, DateTime.Now);
            }
        }


        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex, DateTime.Now);
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        public bool? ShowCustomDialogWindow(string mainText, string title, DialogWindowButtons buttons)
        {
            try
            {
                LabelDialog.Content = title;
                MainTextDialog.Text = mainText;

                SetButtonsByType(buttons);

                StretchBox(MainTextDialog.Text);

                bool? dialog = ShowDialog();
                if (dialog == true)
                {
                    return true;
                }
                else if (dialog == false)
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex, DateTime.Now);
                return false;
            }
        }


        public bool? ShowCustomDialogWindow(string mainText, string title)
        {
            try
            {
                LabelDialog.Content = title;
                MainTextDialog.Text = mainText;

                SetButtonsByType(DialogWindowButtons.YesNo);

                StretchBox(MainTextDialog.Text);

                bool? dialog = ShowDialog();
                if (dialog == true)
                {
                    return true;
                }
                else if (dialog == false)
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex, DateTime.Now);
                return false;
            }
        }


        public bool? ShowCustomDialogWindow(string mainText)
        {
            try
            {
                MainTextDialog.Text = mainText;

                SetButtonsByType(DialogWindowButtons.YesNo);

                StretchBox(MainTextDialog.Text);

                bool? dialog = ShowDialog();
                if (dialog == true)
                {
                    return true;
                }
                else if (dialog == false)
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex, DateTime.Now);
                return false;
            }
        }


        private void SetButtonsByType(DialogWindowButtons buttons)
        {
            if(buttons == DialogWindowButtons.YesNo)
            {
                TurnOffButton(CancelButton);
                TurnOffButton(OkButton);

                AgreeButton.Margin  = new Thickness (0, 0, 150, 10);
                NoButton.Margin = new Thickness (0, 0, -150, 10);
            }

            if(buttons == DialogWindowButtons.Ok)
            {
                TurnOffButton(NoButton);
                TurnOffButton(CancelButton);
                TurnOffButton(AgreeButton);

                OkButton.Margin = new Thickness(0, 0, 0, 10);
            }

            if(buttons == DialogWindowButtons.YesNoCancel)
            {
                TurnOffButton(OkButton);

                AgreeButton.Margin = new Thickness(0, 0, 230, 10);
                NoButton.Margin = new Thickness(0, 0, 0, 10);
                CancelButton.Margin = new Thickness(0, 0, -230, 10);
            }
        }


        private void StretchBox(string text)
        {
            const int offset = 8;
            string[] lines = text.Split('\n');
            if(lines.Length >= 3)
            {
                for (int i = 3; i < lines.Length; i++)
                {
                    this.Height += offset;

                    ReplaceTopControl(AgreeButton, offset);
                    ReplaceTopControl(NoButton, offset);
                    ReplaceTopControl(OkButton, offset);
                    ReplaceTopControl(CancelButton, offset);
                }
            }
        }


        private void ReplaceTopControl(Control c, double valRep)
        {
            double left = c.Margin.Left;
            double top = c.Margin.Top;
            double right = c.Margin.Right;
            double bottom = c.Margin.Bottom;

            top += valRep;
            c.Margin = new Thickness(left, top, right, bottom);
        }


        private void TurnOffButton(Button b)
        {
            b.Visibility = Visibility.Hidden;
            b.IsEnabled = false;
        }


        private void TurnOnButton(Button b)
        {
            b.Visibility = Visibility.Visible;
            b.IsEnabled = true;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            using (CustomDlgWinHotKeysHandler hk = new CustomDlgWinHotKeysHandler(this))
            {
                hk.HandleHotKeys(e);

                string eventMsg = e.Key + " Hotkey used" + " | " + this.Name;
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, eventMsg, DateTime.Now); // write log
            }
        }
    }
}

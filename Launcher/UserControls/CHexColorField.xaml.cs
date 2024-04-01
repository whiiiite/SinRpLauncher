using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for CHexColorField.xaml
    /// </summary>
    [Obsolete("Do not used anymore")]
    public partial class CHexColorField : UserControl
    {
        public CHexColorField()
        {
            InitializeComponent();
        }


        public SolidColorBrush TextForeground
        {
            get { return (SolidColorBrush)GetValue(TextForegroundDP); }
            set { SetValue(TextForegroundDP, value); }
        }


        public SolidColorBrush BackgroundPanel
        {
            get { return (SolidColorBrush)GetValue(BackgroundPanelDP); }
            set { SetValue(BackgroundPanelDP, value); }
        }


        public string Text
        {
            get { return (string)GetValue(TextDP); }
            set { SetValue(TextDP, value); }
        }


        public new double Width
        {
            get { return (double)GetValue(WidthDP); }
            set { SetValue(WidthDP, value); }
        }



        public static readonly DependencyProperty TextForegroundDP =
            DependencyProperty.Register("TextForeground", typeof(SolidColorBrush), typeof(CHexColorField), 
                new PropertyMetadata(new SolidColorBrush(Colors.Black), OnTextForegroundChanged));

        public static readonly DependencyProperty BackgroundPanelDP =
            DependencyProperty.Register("BackgroundPanelDP", typeof(SolidColorBrush), typeof(CHexColorField),
                new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBackgroundPanelChanged));

        public static readonly DependencyProperty TextDP =
            DependencyProperty.Register("Text", typeof(string), typeof(CHexColorField),
                new PropertyMetadata("", OnTextChanged));

        public static readonly DependencyProperty WidthDP =
            DependencyProperty.Register("Width", typeof(double), typeof(CHexColorField),
                new PropertyMetadata(0.0, OnWidthChanged));


        public static void OnTextForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CHexColorField hexF = (CHexColorField)d;
            hexF.TextControl.Foreground = (SolidColorBrush)e.NewValue;
        }


        public static void OnBackgroundPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CHexColorField hexF = (CHexColorField)d;
            hexF.MainPanel.Background = (SolidColorBrush)e.NewValue;
        }


        public static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CHexColorField hexF = (CHexColorField)d;
            hexF.TextControl.Text = (string)e.NewValue;
        }


        public static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CHexColorField hexF = (CHexColorField)d;
            hexF.MainPanel.Width = (double)e.NewValue;
        }


        private void HexColorTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    string s = HexColorTxt.Text;

            //    char[] allowedChars =
            //    {
            //        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            //        'a', 'b', 'c', 'd', 'e', 'f',
            //        '#'
            //    };

            //    foreach (char ch in s)
            //    {
            //        if (!ContainsInChar(allowedChars, ch))
            //        {
            //            HexColorTxt.Text = HexColorTxt.Text.Remove(s.Length - 1);
            //            HexColorTxt.CaretIndex = HexColorTxt.Text.Length;
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Utils.ShowMessageBoxErrorIfNotDebug(ex);
            //}
        }


        private bool ContainsInChar(char[] allowedChars, char ch)
        {
            foreach(char ach in allowedChars)
            {
                if(ach == ch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

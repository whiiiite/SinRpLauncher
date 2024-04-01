using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for CToggleButton.xaml.
    /// Logic for Custom Toggle Button
    /// </summary>
    [Obsolete("Do not used anymore")]
    public partial class CToggleButton : UserControl
    {

        public CToggleButton()
        {
            InitializeComponent();
        }


        public double WidthP
        {
            get { return (double)GetValue(WidthDP); }
            set { SetValue(WidthDP, value); }
        }


        public double HeightP
        {
            get { return (double)GetValue(HeightDP); }
            set { SetValue(HeightDP, value); }
        }


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedDP); }
            set { SetValue(IsCheckedDP, value); }
        }



        private bool _ChangeChkckdInProc = false;
        /// <summary>
        /// Return true if caret(toggle) changes condition, else - false
        /// </summary>
        public bool ChangeChackedInProcs
        {
            get { return _ChangeChkckdInProc; }
            set { _ChangeChkckdInProc = value; }
        }


        public SolidColorBrush CaretColor
        {
            get { return (SolidColorBrush)GetValue(CaretColorDP); }
            set { SetValue(CaretColorDP, value); }
        }


        public SolidColorBrush BackgroundP
        {
            get { return (SolidColorBrush)(GetValue(BackgroundDP)); }
            set { SetValue(BackgroundDP, value); }
        }


        public static readonly DependencyProperty WidthDP =
            DependencyProperty.Register("WidthDP", typeof(double), typeof(CToggleButton), new PropertyMetadata(0.0, OnWidthChange));

        public static readonly DependencyProperty HeightDP =
            DependencyProperty.Register("HeightDP", typeof(double), typeof(CToggleButton), new PropertyMetadata(0.0, OnHeightChange));

        public static readonly DependencyProperty IsCheckedDP =
                        DependencyProperty.Register("IsChecked", typeof(bool), typeof(CToggleButton), new PropertyMetadata(false, OnIsCheckedChange));

        public static readonly DependencyProperty CaretColorDP =
                DependencyProperty.Register("CaretColor", typeof(SolidColorBrush), typeof(CToggleButton),
                new PropertyMetadata(new SolidColorBrush(Colors.White), OnCaretColorChanged));

        public static readonly DependencyProperty BackgroundDP =
                DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(CToggleButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Black), OnBackgroundColorChanged));


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsChecked == false)
                this.ThemeImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\light_thm_img.png", UriKind.Absolute));
            else
                this.ThemeImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\drk_thm_img.png", UriKind.Absolute));
        }


        private static void OnWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CToggleButton ctgl = (CToggleButton)d;
            ctgl.MainBorder.Width = (double)e.NewValue;
        }


        private static void OnHeightChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CToggleButton ctgl = (CToggleButton)d;
            ctgl.MainBorder.Height = (double)e.NewValue;
            ctgl.Caret.Height = (double)e.NewValue - 5;
            ctgl.Caret.Width = (double)e.NewValue - 5;
        }


        private static async void OnIsCheckedChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CToggleButton ctgl = (CToggleButton)d;
            ctgl.IsChecked = (bool)e.NewValue;

            if (!ctgl.ChangeChackedInProcs)
            {
                if (ctgl.IsChecked == false)
                    ctgl.ThemeImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\light_thm_img.png", UriKind.Absolute));
                else
                    ctgl.ThemeImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\drk_thm_img.png", UriKind.Absolute));

                ctgl.ChangeChackedInProcs = true;
                double c = ctgl.Caret.Margin.Left;
                for (int i = 0; i < ctgl.MainBorder.Width - 26; i++)
                {
                    ctgl.Caret.Margin = new Thickness(c, 0, 0, 0);
                    if (ctgl.IsChecked == false)
                        c -= 1;
                    else
                        c += 1;
                    await Task.Delay(1);
                }
                ctgl.ChangeChackedInProcs = false;
            }
        }


        private static void OnCaretColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CToggleButton ctgl = (CToggleButton)d;
            ctgl.Caret.Background = (SolidColorBrush)e.NewValue;
        }


        private static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CToggleButton ctgl = (CToggleButton)d;
            ctgl.MainBorder.Background = (SolidColorBrush)e.NewValue;
        }
    }
}


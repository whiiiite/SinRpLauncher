using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for CustomTextBox.xaml
    /// </summary>
    [Obsolete("Do not used anymore")]
    public partial class CustomTextBox : UserControl
    {
        private DoubleAnimation cursorAnimation = new DoubleAnimation();

        public CustomTextBox()
        {
            InitializeComponent();
            this.MainTextBox.LostFocus += (sender, e) => Caret.Visibility = Visibility.Collapsed;
            this.MainTextBox.GotFocus += (sender, e) => Caret.Visibility = Visibility.Visible;
        }

        public TextWrapping TextWrapping
        {
            get
            {
                return (TextWrapping)GetValue(WrappingProperty);
            }
            set
            {
                SetValue(WrappingProperty, value);
            }
        }

        public double CaretWidth
        {
            get
            {
                return (double)GetValue(CaretWidthProperty);
            }
            set
            {
                SetValue(CaretWidthProperty, value);
            }
        }

        public double TextFontSize
        {
            get
            {
                return (double)GetValue(TextFontSizeProperty);
            }
            set
            {
                SetValue(TextFontSizeProperty, value);
            }
        }

        public SolidColorBrush Foreground
        {
            get
            {
                return (SolidColorBrush)GetValue(SetForegroundProperty);
            }
            set
            {
                SetValue(SetForegroundProperty, value);
            }
        }



        DependencyProperty WrappingProperty =
            DependencyProperty.Register("TextWrap", typeof(TextWrapping), typeof(CustomTextBox), new PropertyMetadata(TextWrapping.Wrap, OnChangeTextWrapping));

        DependencyProperty CaretWidthProperty =
            DependencyProperty.Register("CaretWidth", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0.0, OnChangeCaretWidth));

        DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register("TextFontSize", typeof(double), typeof(CustomTextBox), new PropertyMetadata(0.0, OnChangeFontSize));

        //DependencyProperty SetBackgroundProperty =
        //    DependencyProperty.Register("Background", typeof(Brush), typeof(CustomTextBox), new PropertyMetadata(new SolidColorBrush(Colors.White), new PropertyChangedCallback(OnBackgroudChanged)));

        DependencyProperty SetForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(CustomTextBox), new PropertyMetadata(null, new PropertyChangedCallback(OnForegroundChanged)));

            
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox? customTextBox = (CustomTextBox)d;
            customTextBox.MainTextBox.Foreground = (SolidColorBrush)e.NewValue;
        }

        //private static void OnBackgroudChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    CustomTextBox? customTextBox = (CustomTextBox)d;
        //    customTextBox.MainTextBox.Background = (SolidColorBrush)e.NewValue;
        //}

        private static void OnChangeFontSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox customTextBox = (CustomTextBox)d;
            customTextBox.MainTextBox.FontSize = (double)e.NewValue;
        }

        private static void OnChangeCaretWidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox customTextBox = (CustomTextBox)d;
            customTextBox.Caret.Width = (double)e.NewValue;
        }

        private static void OnChangeTextWrapping(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox customTextBox = (CustomTextBox)d;
            customTextBox.MainTextBox.TextWrapping = (TextWrapping)e.NewValue;
        }


        private void UpdateCaretPosition()
        {
            var caret = MainTextBox.GetRectFromCharacterIndex(MainTextBox.CaretIndex);
            Caret.Height = caret.Bottom - caret.Top;
            Canvas.SetTop(Caret, caret.Top);
            Canvas.SetBottom(Caret, caret.Bottom);

            var left = Canvas.GetLeft(Caret);
            if (!double.IsNaN(left))
            {
                cursorAnimation.From = left;
                cursorAnimation.To = caret.Right;
                cursorAnimation.Duration = new Duration(TimeSpan.FromSeconds(.05));

                Caret.BeginAnimation(Canvas.LeftProperty, cursorAnimation);
            }
            else
            {
                Canvas.SetLeft(Caret, caret.Right);
            }
        }

        private void MainTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateCaretPosition();
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCaretPosition();
        }

        private void MainTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            UpdateCaretPosition();
        }
    }
}

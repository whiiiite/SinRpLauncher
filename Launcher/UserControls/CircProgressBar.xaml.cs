using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for CircProgressBar.xaml
    /// </summary>
    public partial class CircProgressBar : UserControl
    {
        public CircProgressBar()
        {
            InitializeComponent();
        }
        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        public int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public int Height
        {
            get { return (int)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public SolidColorBrush ProgressLineColor
        {
            get { return (SolidColorBrush)GetValue(ProgressLineColorProperty); }
            set { SetValue(ProgressLineColorProperty, value); }
        }

        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(CircProgressBar), new PropertyMetadata(0.0, OnProgressValueChanged));

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(int), typeof(CircProgressBar), new PropertyMetadata(0, OnWidthChanged));

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(int), typeof(CircProgressBar), new PropertyMetadata(0, OnHeightChanged));

        public static readonly DependencyProperty ProgressLineColorProperty =
             DependencyProperty.Register("ProgressLineColor", typeof(SolidColorBrush), typeof(CircProgressBar), new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnProgressLineColorChange)));


        private static void OnProgressLineColorChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircProgressBar? circularProgressBar = d as CircProgressBar;
            circularProgressBar.ProgressLine.Fill = (SolidColorBrush)e.NewValue;
        }

        private static void OnProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircProgressBar? circularProgressBar = d as CircProgressBar;
            circularProgressBar.ProgressLine.EndAngle = (double)e.NewValue * 3.6;
        }

        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircProgressBar? circularProgressBar = d as CircProgressBar;
            circularProgressBar.ProgressLine.Width = (int)e.NewValue;
        }

        private static void OnHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircProgressBar? circularProgressBar = d as CircProgressBar;
            circularProgressBar.ProgressLine.Height = (int)e.NewValue;
        }
    }
}

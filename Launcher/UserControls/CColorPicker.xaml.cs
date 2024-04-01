using Launcher.Classes;
using Microsoft.VisualBasic.Devices;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for CColorPicker.xaml
    /// </summary>
    public partial class CColorPicker : UserControl
    {
        private string _HexCodeColor = "#000000";

        public string HexCodeColor
        {
            get { return _HexCodeColor; }
        }

        private Color _Color = Color.FromArgb(255, 0, 0, 0);
        
        public Color Color 
        {
            get { return _Color; }
        }

        public CColorPicker()
        {
            InitializeComponent();
        }


        public string Title
        {
            get { return (string)GetValue(TitleDP); }
            set { SetValue(TitleDP, value); }
        }


        public static DependencyProperty TitleDP =
            DependencyProperty.Register("Title", typeof(string), typeof(CColorPicker), new PropertyMetadata("", OnTitleTextChanged));


        public static void OnTitleTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CColorPicker cColorPicker = (CColorPicker)d;
            cColorPicker.TitlePicker.Content = (string)e.NewValue;
        }


        private void CircleGradient_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    BitmapSource visual_BitmapSource = Get_BitmapSource_of_Element(CircleGradient);

                    CroppedBitmap cb = new CroppedBitmap(visual_BitmapSource, new Int32Rect(
                                                         (int)e.GetPosition(CircleGradient).X,
                                                         (int)e.GetPosition(CircleGradient).Y, 
                                                         1, 1));

                    byte[] pixels = new byte[4];
                    try
                    {
                        cb.CopyPixels(pixels, 4, 0);
                    }
                    catch (Exception) { }

                                                // A    ->     R     ->      G    ->       B
                    Color color = Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);

                    _Color = color;

                    string HexAlpha = Utils.DecimalToHexByte(color.A);
                    string HexRed   = Utils.DecimalToHexByte(color.R);
                    string HexGreen = Utils.DecimalToHexByte(color.G);
                    string HexBlue  = Utils.DecimalToHexByte(color.B);

                    _HexCodeColor = "#" + HexAlpha + HexRed + HexGreen + HexBlue;
                    HexCodeBox.Text = _HexCodeColor; // text changed event is work here
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public BitmapSource Get_BitmapSource_of_Element(FrameworkElement element)

        {

            //-------------< get_BitmapSource_of_Element() >------------

            //< check >

            if (element == null) return null; 

            //</ check >


            //< init >
            double dpi = 96;

            double width = element.ActualWidth;
            double height = element.ActualHeight;
            //</ init >

            RenderTargetBitmap bitmap_of_Element = null;

            if (bitmap_of_Element == null)
            {

                try
                {
                    //< create empty Bitmap of element >

                    bitmap_of_Element = new RenderTargetBitmap((int)width, (int)height, dpi, dpi, PixelFormats.Default);

                    //</ create empty Bitmap of element >



                    //----< render area into bitmap >----

                    DrawingVisual visual_area = new DrawingVisual();

                    using (DrawingContext dc = visual_area.RenderOpen())
                    {
                        VisualBrush visual_brush = new VisualBrush(element);
                        dc.DrawRectangle(visual_brush, null, new Rect(new Point(), new Size((int)width, (int)height)));
                    }

                    //----</ render area into bitmap >----



                    //< render >

                    bitmap_of_Element.Render(visual_area);

                    //</ render >
                }
                catch (Exception ex)
                {
                    Utils.ShowMessageBoxErrorIfNotDebug(ex);
                }

            }

            return bitmap_of_Element;

            //-------------</ get_BitmapSource_of_Element() >------------

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //HexCodeBox.Text = _HexCodeColor;
        }

        private void HexCodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _HexCodeColor = HexCodeBox.Text;

                bool isCorrectCol = Utils.IsCorrectHexColor(_HexCodeColor);
                if (isCorrectCol == false)
                    _HexCodeColor = Utils.FixHexCodeColor(_HexCodeColor);

                PreviewColor.Fill = (Brush)(Utils.GetBrushConverter().ConvertFrom(_HexCodeColor)) ?? Brushes.Black;
                _Color = ((SolidColorBrush)(PreviewColor.Fill)).Color;
            }
            catch (Exception) 
            {
                PreviewColor.Fill = Brushes.Black;
            }
        }
    }
}

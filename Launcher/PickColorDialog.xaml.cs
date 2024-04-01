using System.Windows;
using System.Windows.Media;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for PickColorDialog.xaml
    /// </summary>
    public partial class PickColorDialog : Window
    {
        private Color _FGColor;
        private Color _BGColor;

        public Color FGColor
        {
            get { return _FGColor; }
            set { _FGColor = value; }
        }

        public Color BGColor
        {
            get { return _BGColor; }
            set { _BGColor = value; }
        }

        public PickColorDialog()
        {
            InitializeComponent();
            _FGColor = ForegroundColor.Color;
            _BGColor = BackgroundColor.Color;
        }


        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            _FGColor = ForegroundColor.Color; //((System.Windows.Media.SolidColorBrush)ForegroundColor.PreviewColor.Fill).Color;
            _BGColor = BackgroundColor.Color;
            DialogResult = true;
        }
    }
}

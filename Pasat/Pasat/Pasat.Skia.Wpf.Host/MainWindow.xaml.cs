using Pasat.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pasat.WPF.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Locator.CurrentMutable.Register(() => new FileSaver(), typeof(ISaver));
            Locator.CurrentMutable.Register(() => new AudioPlayer(), typeof(IMediaPlayer));
            root.Content = new global::Uno.UI.Skia.Platform.WpfHost(Dispatcher, () => new Pasat.App());
        }
    }
}

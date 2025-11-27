using DictionatiesForEverything.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DictionatiesForEverything
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Нужно придумать, как можно переделать
        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var rtb = sender as RichTextBox;
            if (DataContext is ManageDataVM vm)
                vm.StyleTextViaToolBar.SetSelection(rtb.Selection);
        }
    }
}
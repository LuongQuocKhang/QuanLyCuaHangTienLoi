using Quan_Ly_Ban_Hang.ViewModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Quan_Ly_Ban_Hang
{
    /// <summary>
    /// Interaction logic for Don_Dat_Hang.xaml
    /// </summary>
    public partial class Don_Dat_Hang : Window
    {
        public Don_Dat_Hang()
        {
            InitializeComponent();
        }

        private void txbSoluong_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

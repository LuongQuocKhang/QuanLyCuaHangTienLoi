using System.Text.RegularExpressions;
using System.Windows;

namespace Quan_Ly_Ban_Hang.View
{
    /// <summary>
    /// Interaction logic for Ban_Hang.xaml
    /// </summary>
    public partial class Ban_Hang : Window
    {
        public Ban_Hang()
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

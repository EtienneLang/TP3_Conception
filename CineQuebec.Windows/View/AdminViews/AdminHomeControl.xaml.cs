using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class AdminHomeControl : UserControl
    {
        public AdminHomeControl()
        {
            InitializeComponent();
        }

        private void ButtonUsers_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).UserListControl();
        }

        private void ButtonFilms_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FilmListControl();
        }

        private void ButtonGift_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
        }
    }
}
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

        private void btn_users_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).UserListControl();
        }

        private void btn_films_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FilmListControl();
        }

        private void btn_gift_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GiftHomeControl();
        }
    }
}
using System.Windows;
using System.Windows.Controls;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class GiftHomeControl : UserControl
    {
        public GiftHomeControl()
        {
            InitializeComponent();
        }

        private void Btn_gift_ticket_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).TicketGratuitProjection();
        }

        private void ButtonGiftAvantPremiere_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).InvitationAvantPremiere();
        }
    }
}
using CineQuebec.Windows.View;
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
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.View.AbonneViews;
using CineQuebec.Windows.View.AdminViews;

namespace CineQuebec.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainContentControl.Content = new ConnexionControl();
        }

        public void AdminHomeControl()
        {
            mainContentControl.Content = new AdminHomeControl();
        }

        public void UserListControl()
        {
            mainContentControl.Content = new UserListControl();
        }

        public void FilmListControl()
        {
            mainContentControl.Content = new FilmListControl();
        }
        
        public void AbonneHomeControl(Abonne abonne)
        {
            mainContentControl.Content = new AbonneHomeControl(abonne);
        }
        
        public void FilmListForUser(Abonne abonne)
        {
            mainContentControl.Content = new FilmListForUser(abonne);
        }
        
        public void GiftHomeControl()
        {
            mainContentControl.Content = new GiftHomeControl();
        }
        
        public void TicketGratuitProjection()
        {
            mainContentControl.Content = new TicketGratuitProjection();
        }
        
    }
}
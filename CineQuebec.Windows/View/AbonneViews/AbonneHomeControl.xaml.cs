using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AbonneViews
{
    public partial class AbonneHomeControl : UserControl
    {
        Abonne abonneConnecte;
        public AbonneHomeControl(Abonne abonne)
        {
            abonneConnecte = abonne;
            InitializeComponent();
        }

        private void btn_films_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FilmListForUser(abonneConnecte);
        }
    }
}
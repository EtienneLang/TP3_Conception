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
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View
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

        private void Btn_NoteClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AbonneListeFilmNoteControl(abonneConnecte);
        }
    }
}
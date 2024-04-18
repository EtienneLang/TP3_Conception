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
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Interfaces;

namespace CineQuebec.Windows.View
{
    public partial class UserListControl : UserControl
    {
        private AbonneService _db;
        private List<Abonne> _abonnes;

        public UserListControl()
        {
            _db = new AbonneService();
            InitializeComponent();
            GenerateUserList();
        }

        private void GetAbonnes()
        {
            _abonnes = _db.ReadAbonnes();
        }

        private void GenerateUserList()
        {
            GetAbonnes();
            foreach (Abonne abonne in _abonnes)
            {
                ListBoxItem itemAbonne = new ListBoxItem();
                itemAbonne.Content = abonne;
                lstUsers.Items.Add(itemAbonne);
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
        }
    }
}
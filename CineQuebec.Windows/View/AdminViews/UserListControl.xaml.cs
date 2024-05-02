using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class UserListControl : UserControl
    {
        private readonly AbonneService _db;
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
                ListBoxItem itemAbonne = new ListBoxItem
                {
                    Content = abonne
                };
                ListBoxUsers.Items.Add(itemAbonne);
            }
        }

        private void ButtonRetourVersAccueil_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
        }
    }
}
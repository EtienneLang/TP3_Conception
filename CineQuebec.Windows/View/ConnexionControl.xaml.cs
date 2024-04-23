using CineQuebec.Windows.DAL;
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

namespace CineQuebec.Windows.View
{
    public partial class ConnexionControl : UserControl
    {
        AbonneService _abonneService = new AbonneService(); 
        
        
        public ConnexionControl()
        {
            InitializeComponent();
        }

        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {
            string formUsername = txt_username.Text;
            string formPassword = txt_password.Text;

            Abonne abonneResponse = _abonneService.GetAbonneByUsername(formUsername);

            if (!String.IsNullOrWhiteSpace(formUsername))
            {
                if (abonneResponse == null)
                {
                    MessageBox.Show("Nom d'utilisateur introuvable.");
                }

                else if (abonneResponse.Password == formPassword)
                {
                    if (abonneResponse.Role == "Admin")
                    {
                        ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
                    }
                    else
                    {
                        ((MainWindow)Application.Current.MainWindow).AbonneHomeControl(abonneResponse);
                    }
                }

                else
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nom d'utilisateur.");
            }
        }
    }
}
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
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View
{
    public partial class ConnexionControl : UserControl
    {
        private IAuthService _authService;
        
        
        public ConnexionControl(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private bool ValidateForm()
        {
            string formUsername = txt_username.Text;
            string formPassword = txt_password.Password;
            if (String.IsNullOrWhiteSpace(formUsername))
            {
                LabelError.Content = "Veuillez entrer votre nom d'utilisateur";
                return false;
            }
            if (String.IsNullOrWhiteSpace(formPassword))
            {
                LabelError.Content = "Veuillez entrer votre mot de passe";
                return false;
            }

            return true;
        }

        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm())
                return;
            try
            {
                if (!_authService.AbonneExiste(txt_username.Text))
                {
                    LabelError.Content = "Nom d'utilisateur ou mot de passe invalide";
                    return;
                }

                Abonne abonne = _authService.Login(txt_username.Text, txt_password.Password);
                if (abonne == null)
                {
                    LabelError.Content = "Nom d'utilisateur ou mot de passe invalide";
                    return;
                }
                if (abonne?.Role == "Admin")
                {
                    ((MainWindow)Application.Current.MainWindow).AdminHomeControl();
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).AbonneHomeControl(abonne);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
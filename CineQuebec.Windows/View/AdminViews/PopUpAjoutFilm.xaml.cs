using System.Windows;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class PopUpAjoutFilm : Window
    {
        public PopUpAjoutFilm()
        {
            InitializeComponent();
        }

        private void ButtonDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TextBoxTitre.SelectAll();
            TextBoxTitre.Focus();
        }

        public string Answer
        {
            get { return TextBoxTitre.Text; }
        }
    }
}
using System;
using System.Windows;

namespace WpfTutorialSamples.Dialogs
{
    public partial class PopUpAjoutFilm : Window
    {
        public PopUpAjoutFilm()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtTitre.SelectAll();
            txtTitre.Focus();
        }

        public string Answer
        {
            get { return txtTitre.Text; }
        }
    }
}
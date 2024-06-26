﻿using System.Windows;
using System.Windows.Controls;
using CineQuebec.Windows.BLL.Interfaces;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Data;

namespace CineQuebec.Windows.View.AdminViews
{
    public partial class UserListControl : UserControl
    {
        private readonly IAbonneService _abonneService;
        private List<Abonne> _abonnes;

        public UserListControl(IAbonneService abonneService)
        {
            InitializeComponent();
            _abonneService = abonneService;
            GenerateUserList();
        }

        private void GetAbonnes()
        {
            _abonnes = _abonneService.ReadAbonnes();
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
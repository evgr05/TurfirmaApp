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
using TurfirmaApp.DataFilesApp;
using TurfirmaApp.Pages;

namespace TurfirmaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        int roleId;
        string nameUser;
        public PageMenu(int idRole, string userName)
        {
            InitializeComponent();
            if(idRole == 2) {btnAdmin.IsEnabled = false;}
            tblkuserName.Text = ($"Оператор: {userName}");
            roleId = idRole;
            nameUser = userName;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageAdmin(roleId, nameUser));
        }

        private void btnTours_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageTours(roleId, nameUser));
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageLogin());
        }

        private void btnFlights_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageFlights(roleId, nameUser));
        }

        private void btnHotels_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageHotels(roleId, nameUser));
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageClients(roleId, nameUser));
        }
    }
}

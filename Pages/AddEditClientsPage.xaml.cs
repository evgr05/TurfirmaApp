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

namespace TurfirmaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditClientsPage.xaml
    /// </summary>
    public partial class AddEditClientsPage : Page
    {
        private Clients _currentClient = new Clients();
        int roleId;
        string nameUser;
        public AddEditClientsPage(Clients _selectedClient, int idRole, string userName)
        {
            InitializeComponent();       
            if( _selectedClient != null)
            {
                _currentClient = _selectedClient;
            }
            cmbGender.ItemsSource = DBConnect.entObj.Genders.ToList();
            cmbHotel.ItemsSource = DBConnect.entObj.Hotels.ToList();
            cmbTour.ItemsSource = DBConnect.entObj.Tours.ToList();
            DataContext = _currentClient;
            roleId = idRole;
            nameUser = userName;
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageClients(roleId, nameUser));
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (_currentClient.Id == 0)
                {
                    DBConnect.entObj.Clients.Add(_currentClient);
                }
                DBConnect.entObj.SaveChanges();
                PageFrame.frmObj.Navigate(new PageClients(roleId, nameUser));
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Критический сбой в работе приложения." +
                    ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}

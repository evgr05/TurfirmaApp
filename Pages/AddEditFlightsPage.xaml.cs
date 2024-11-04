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
    /// Логика взаимодействия для AddEditFlightsPage.xaml
    /// </summary>
    public partial class AddEditFlightsPage : Page
    {
        int roleId;
        string nameUser;
        private Flights _currentFlight = new Flights();
        public AddEditFlightsPage(Flights _selectedFlight, int idRole, string userName)
        {
            InitializeComponent();
            if (_selectedFlight != null)
            {
                _currentFlight = _selectedFlight;
            }
            roleId = idRole;
            nameUser = userName;
            DataContext = _currentFlight;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentFlight.Id == 0)
                {
                    DBConnect.entObj.Flights.Add(_currentFlight);
                }                
                DBConnect.entObj.SaveChanges();
                PageFrame.frmObj.Navigate(new PageFlights(roleId, nameUser));

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Критический сбой в работе приложения." +
                    ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageFlights(roleId, nameUser));
        }
    }
}


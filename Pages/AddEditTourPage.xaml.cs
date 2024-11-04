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
    /// Логика взаимодействия для AddEditTourPage.xaml
    /// </summary>
    public partial class AddEditTourPage : Page
    {
        private Tours _currentTour = new Tours();
        int roleId;
        string nameUser;
        public AddEditTourPage(Tours _selectedTour, int idRole, string userName)
        {
            InitializeComponent();
            if (_selectedTour != null)
            {
                _currentTour = _selectedTour;
            }
            DataContext = _currentTour;
            cmbFlight.ItemsSource = DBConnect.entObj.Flights.ToList();
            roleId = idRole;
            nameUser = userName;
            /*cmbFlight.SelectedValuePath = "Id";
            cmbFlight.DisplayMemberPath = "Name";*/


        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            /*Tours toursObj = new Tours()
            {
                Name = txbName.Text,
                Country = txbCountry.Text,
                Date = Convert.ToDateTime(txbDate.Text),
                Places = txbPlace.Text,
                Price = Convert.ToDecimal(txbPrice.Text),
                Flights = cmbFlight.SelectedItem as Flights
            };
            DBConnect.entObj.Tours.Add(toursObj);
            DBConnect.entObj.SaveChanges();*/
            
            try
            {
                if (_currentTour.Id == 0)
                {
                    DBConnect.entObj.Tours.Add(_currentTour);
                }
                DBConnect.entObj.SaveChanges();
                PageFrame.frmObj.Navigate(new PageTours(roleId, nameUser));
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
            PageFrame.frmObj.Navigate(new PageTours(roleId, nameUser));
        }
    }
}

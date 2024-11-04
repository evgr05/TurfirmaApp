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
    /// Логика взаимодействия для AddEditHotelsPage.xaml
    /// </summary>
    public partial class AddEditHotelsPage : Page
    {
        int roleId;
        string nameUser;
        Hotels _currentHotel = new Hotels();
        public AddEditHotelsPage(Hotels _selectedHotel ,int idRole, string userName)
        {
            InitializeComponent();
            roleId = idRole;
            nameUser = userName;
            if (_selectedHotel != null)
            {
                _currentHotel = _selectedHotel;
            }
            DataContext = _currentHotel;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentHotel.Id == 0)
                {
                    DBConnect.entObj.Hotels.Add(_currentHotel);
                }
                DBConnect.entObj.SaveChanges();
                PageFrame.frmObj.Navigate(new PageHotels(roleId, nameUser));
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageHotels(roleId, nameUser));
        }
    }
}

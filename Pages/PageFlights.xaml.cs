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
using TurfirmaApp.Pages;
using TurfirmaApp.DataFilesApp;

namespace TurfirmaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageFlights.xaml
    /// </summary>
    public partial class PageFlights : Page
    {
        int roleId;
        string nameUser;
        public PageFlights(int idRole, string userName)
        {
            InitializeComponent();
            
            roleId = idRole;
            nameUser = userName;
            flightsGrid.ItemsSource = DBConnect.entObj.Flights.ToList();
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageMenu(roleId, nameUser));
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var flightsForRemoving = flightsGrid.SelectedItems.Cast<Flights>().ToList();
            try
            {
                if(MessageBox.Show(
                    $"Вы точно хотите удалить {flightsForRemoving.Count} запись?",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Flights.RemoveRange(flightsForRemoving);
                    DBConnect.entObj.SaveChanges();
                }
                flightsGrid.ItemsSource = DBConnect.entObj.Flights.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Критический сбой в работе приложения. " +
                    ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Question);
            }            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditFlightsPage(null, roleId, nameUser));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditFlightsPage((sender as Button).DataContext as Flights, roleId, nameUser));
        }

        /*private void dellBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить эту запись?",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question)==MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Flights.Remove((sender as Button).DataContext as Flights); 
                    DBConnect.entObj.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Критический сбой в работе приложения. " +
                    ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Question);
            }
            flightsGrid.ItemsSource = DBConnect.entObj.Flights.ToList();
        }*/

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                flightsGrid.ItemsSource = DBConnect.entObj.Flights.Where(item => item.Name == txbSearch.Text || item.Name.Contains(txbSearch.Text)).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Критический сбой в работе приложения. " +
                    ex.Message.ToString(),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            
        }
    }
}

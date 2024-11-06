using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для PageTours.xaml
    /// </summary>
    public partial class PageTours : Page
    {
        int roleId;
        string nameUser;
        public PageTours(int idRole, string userName)
        {
            InitializeComponent();
            toursGrid.ItemsSource = DBConnect.entObj.Tours.ToList();
            roleId = idRole;
            nameUser = userName;
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditTourPage(((sender as Button).DataContext as Tours), roleId, nameUser));
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditTourPage((null), roleId, nameUser));
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var toursForRemoving = toursGrid.SelectedItems.Cast<Tours>().ToList();
            try
            {
                if (MessageBox.Show(
                $"Вы точно хотите удалить {toursForRemoving.Count} запись?",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Tours.RemoveRange(toursForRemoving);
                    DBConnect.entObj.SaveChanges();
                    toursGrid.ItemsSource = DBConnect.entObj.Tours.ToList();
                }
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

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageMenu(roleId, nameUser));
        }

        /*private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (MessageBox.Show(
                $"Вы точно хотите удалить эту запись?",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Tours.Remove((sender as Button).DataContext as Tours);
                    DBConnect.entObj.SaveChanges();
                }
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
            
            toursGrid.ItemsSource = DBConnect.entObj.Tours.ToList();
        }*/

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                toursGrid.ItemsSource = DBConnect.entObj.Tours.Where(item => item.Name ==  txbSearch.Text || item.Name.Contains(txbSearch.Text)).ToList();
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
    }
}

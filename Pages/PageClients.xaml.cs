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
    /// Логика взаимодействия для PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        int roleId;
        string nameUser;
        public PageClients(int idRole, string userName)
        {
            InitializeComponent();
            clientsGrid.ItemsSource = DBConnect.entObj.Clients.ToList();
            roleId = idRole;
            nameUser = userName;
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageMenu(roleId, nameUser));
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            var clientsForRemoving = clientsGrid.SelectedItems.Cast<Clients>().ToList();
            try
            {
                if (MessageBox.Show(
                $"Вы точно хотите удалить {clientsForRemoving.Count} запись?",
                "Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Clients.RemoveRange(clientsForRemoving);
                    DBConnect.entObj.SaveChanges();
                }
                clientsGrid.ItemsSource = DBConnect.entObj.Clients.ToList();
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

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditClientsPage(null, roleId, nameUser));
        }

        /*private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show($"Вы точно хотите удалить эту запись?",
                                    "Удаление",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Clients.Remove((sender as Button).DataContext as Clients);
                    DBConnect.entObj.SaveChanges();
                }
                clientsGrid.ItemsSource = DBConnect.entObj.Clients.ToList();
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
        }*/

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditClientsPage(((sender as Button).DataContext as Clients), roleId, nameUser));
        }      
        

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                clientsGrid.ItemsSource = DBConnect.entObj.Clients.Where(item => item.FullName == txbSearch.Text || item.FullName.Contains(txbSearch.Text)).ToList();

            }
            catch (Exception ex)
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

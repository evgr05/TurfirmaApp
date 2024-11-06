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
    /// Логика взаимодействия для PageHotels.xaml
    /// </summary>
    public partial class PageHotels : Page
    {
        int roleId;
        string nameUser;
        public PageHotels(int idRole, string userName)
        {
            InitializeComponent();
            hotelsGrid.ItemsSource = DBConnect.entObj.Hotels.ToList();
            roleId = idRole;
            nameUser = userName;
            if (idRole == 2) 
            { 
                addBtn.IsEnabled = false;
                delBtn.IsEnabled = false;
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditHotelsPage(null, roleId, nameUser));
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var hotelsForRemoving = hotelsGrid.SelectedItems.Cast<Hotels>().ToList();
                if(MessageBox.Show($"Вы точно хотите удалить {hotelsForRemoving.Count} запись?",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Hotels.RemoveRange(hotelsForRemoving);
                    DBConnect.entObj.SaveChanges();
                }
                hotelsGrid.ItemsSource = DBConnect.entObj.Hotels.ToList();
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
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageMenu(roleId, nameUser));
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            if (roleId == 2)
            {
                MessageBox.Show("Для выполнения данной функции недостаточно прав",
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            if (roleId == 1)
            {
                PageFrame.frmObj.Navigate(new AddEditHotelsPage((sender as Button).DataContext as Hotels, roleId, nameUser));
            }
            
        }

        /*private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (roleId == 2)
                {
                    MessageBox.Show("Для выполнения данной функции недостаточно прав",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }
                if (roleId == 1)
                {
                    if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                    "Удаление",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DBConnect.entObj.Hotels.Remove((sender as Button).DataContext as Hotels);
                        DBConnect.entObj.SaveChanges();
                    }
                    hotelsGrid.ItemsSource = DBConnect.entObj.Hotels.ToList();
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
        }*/

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                hotelsGrid.ItemsSource = DBConnect.entObj.Hotels.Where(item => item.Name == txbSearch.Text || item.Name.Contains(txbSearch.Text)).ToList();
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

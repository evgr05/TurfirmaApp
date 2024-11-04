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
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        int roleId;
        string nameUser;
        public PageAdmin(int idRole, string userName)
        {
            InitializeComponent();
            roleId = idRole;
            nameUser = userName;
            if(roleId != 1)
            {
                MessageBox.Show("Для перехода на данную страницу требуются права администратора!",
                    "Недостаточно прав",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                PageFrame.frmObj.Navigate(new PageMenu(idRole, userName));
            }
            tblkuserName.Text = $"Администратор: {userName}";
            dgUsers.ItemsSource = DBConnect.entObj.Users.ToList();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new PageMenu(roleId, nameUser));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditUsersPage((sender as Button).DataContext as Users, roleId, nameUser));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Вы точно хотите удалить аккаунт этого пользователя?",
                                    "Удаление",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Users.Remove((sender as Button).DataContext as Users);
                    DBConnect.entObj.SaveChanges();
                }
                dgUsers.ItemsSource = DBConnect.entObj.Users.ToList();

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

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var usersForRemoving = dgUsers.SelectedItems.Cast<Users>().ToList();
                if (MessageBox.Show($"Вы точно хотите удалить аккаунты {usersForRemoving.Count} пользователей?",
                                    "Удаление",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.entObj.Users.RemoveRange(usersForRemoving);
                    DBConnect.entObj.SaveChanges();
                }
                dgUsers.ItemsSource = DBConnect.entObj.Users.ToList();
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

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.frmObj.Navigate(new AddEditUsersPage(null, roleId, nameUser));
        }
    }
}

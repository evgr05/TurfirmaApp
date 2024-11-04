using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddEditUsersPage.xaml
    /// </summary>
    public partial class AddEditUsersPage : Page
    {
        int roleId;
        string nameUser;
        Users _currentUser = new Users();
        public AddEditUsersPage(Users _selectedUser, int idRole, string userName)
        {
            InitializeComponent();
            roleId = idRole;
            nameUser = userName;
            cmbRole.ItemsSource = DBConnect.entObj.Role.ToList();
            if (_selectedUser != null)
            {
                _currentUser = _selectedUser;
            }
            DataContext = _currentUser;
        }        
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if(psbPassword.Password == psbRepPassword.Password)
                {
                    
                    
                    _currentUser.Password = md5.hashPassword(psbPassword.Password);
                    //_currentUser.Password = psbPassword.Password;
                    if (_currentUser.Id == 0)
                    {
                        DBConnect.entObj.Users.Add(_currentUser);
                    }
                    DBConnect.entObj.SaveChanges();
                    PageFrame.frmObj.Navigate(new PageAdmin(roleId, nameUser));
                }
                else
                {
                    MessageBox.Show("Введенные пароли не совпадают. Повторите ещё раз",
                        "Пароли не совпадают",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
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
            PageFrame.frmObj.Navigate(new PageAdmin(roleId, nameUser));
        }
    }
}

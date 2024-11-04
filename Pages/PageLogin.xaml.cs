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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var passUser = md5.hashPassword(psbPassword.Password);
                var loginUser = txbLogin.Text;
                var userObj = DBConnect.entObj.Users.FirstOrDefault(x => x.Login == loginUser && x.Password == passUser);
                if (userObj != null)
                {
                    PageFrame.frmObj.Navigate(new PageMenu(userObj.RoleId, userObj.Name));
                }
                else
                {
                    MessageBox.Show(
                    "Неверное имя пользователя или пароль",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    psbPassword.Clear();
                    txbLogin.Clear();
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

        private void txbLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    var passUser = md5.hashPassword(psbPassword.Password);
                    var loginUser = txbLogin.Text;
                    var userObj = DBConnect.entObj.Users.FirstOrDefault(x => x.Login == loginUser && x.Password == passUser);
                    if (userObj != null)
                    {
                        PageFrame.frmObj.Navigate(new PageMenu(userObj.RoleId, userObj.Name));
                    }
                    else
                    {
                        MessageBox.Show(
                        "Неверное имя пользователя или пароль",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                        psbPassword.Clear();
                        txbLogin.Clear();
                    }
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
        }
    }
}

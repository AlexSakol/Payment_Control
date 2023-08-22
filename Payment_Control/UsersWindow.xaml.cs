using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
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
using System.Windows.Shapes;
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        User authorizedUser;
        EntityContext entityContext = new EntityContext();
        public UsersWindow()
        {
            InitializeComponent();
            Users.ItemsSource = entityContext.Users.ToList();
            Roles.ItemsSource = entityContext.Roles.ToList();
            this.Closing += Window_Closing;
        }

        public UsersWindow(User authorizedUser) : this()
        {
            this.authorizedUser = authorizedUser;
        }

        private void Add_User_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Authorized_User_Checker(authorizedUser) == true)
            {
                AddUserWindow addUserWindow = new AddUserWindow();
                addUserWindow.ShowDialog();
                Users.ItemsSource = entityContext.Users.ToList();
            }
        }
        private void Button_Remove_User_Click(object sender, RoutedEventArgs e)
        {
            if (Authorized_User_Checker(authorizedUser) == true)
            {
                var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (Users.SelectedItems.Count > 0)
                    {
                        for (int i = Users.SelectedItems.Count - 1; i >= 0; i--)
                        {
                            User user = Users.SelectedItems[i] as User;
                            if (user != null && user.RoleId != 1 && user != authorizedUser)
                            {
                                entityContext.Users.Remove(user);
                            }
                            else MessageBox.Show("Выполнение данного действия невозможно");
                        }
                        entityContext.SaveChanges();
                    }
                }
                Users.ItemsSource = entityContext.Users.ToList();
            }
        }
        private void Button_Edit_User_Click(object sender, RoutedEventArgs e)
        {
            if (Authorized_User_Checker(authorizedUser) == true)
            {
                User user = Users.SelectedItem as User;
                if (user == null) return;
                EditUserWindow editWindow = new EditUserWindow(user);
                if (user.RoleId != 1)
                {
                    if (editWindow.ShowDialog() == true)
                        entityContext.SaveChanges();
                    else
                    {
                        if (user != null)
                        {
                            entityContext.Entry(user).Reload();
                            Users.DataContext = null;
                            Users.DataContext = entityContext.Users.Local;
                        }
                    }
                }
                else MessageBox.Show("Выполнение данного действия невозможно");
            }
                Users.ItemsSource = entityContext.Users.ToList();
        }

            /// <summary>
            /// Проверяет роль авторизированного пользователя и,
            /// если он не администратор, возвращает ложь, а также выводит сообщение
            /// о невозможности выполнения действия
            /// </summary>
            /// <param name="authorizedUser"></param>
            bool Authorized_User_Checker(User authorizedUser)
            {
                if (authorizedUser.RoleId != 1)
                {
                    MessageBox.Show("У вас нет прав на выполнение данной операции");
                    return false;
                }
                else return true;
            }
            void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                entityContext.Dispose();
            }
    }

}



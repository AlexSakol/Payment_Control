using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        User user = new User();        
        EntityContext context = new EntityContext();
        public AddUserWindow()
        {
            InitializeComponent();
            this.Closing += Window_Closing;
            grd.DataContext = user;
            Role.ItemsSource = context.Roles.ToList();
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == null || Regex.IsMatch(UserName.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                || UserName.Text.Length < 3)
                    return;            
            if (Login.Text == null || Regex.IsMatch(Login.Text, @"[А-Яа-я\W\d]") || Login.Text.Length < 3)
                    return;
            if (context.Users.Any(u => u.Login == Login.Text))
            {
                MessageBox.Show("Пользователь с данным логином уже существует");
                return;
            }
            if (Password.Password.Length < 4)
            {
                MessageBox.Show("Пароль не может быть меньше 4-х символов");
                return;
            }
            if (Password.Password != RepeatPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            if (Regex.IsMatch(Password.Password, @"[А-Яа-я]"))
            {
                MessageBox.Show("Пароль не может содержать кириллицу");
                return;
            }            
            if (Role.SelectedIndex < 0) return;
            user.Password = Password.Password;
            context.Users.Add(user);
            context.SaveChanges();         
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
}

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
            RoleComboBox.ItemsSource = context.Roles.ToList();
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameBox.Text == null || Regex.IsMatch(UserNameBox.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                || UserNameBox.Text.Length < 3)
                    return;            
            if (LoginBox.Text == null || Regex.IsMatch(LoginBox.Text, @"[А-Яа-я\W\d]") || LoginBox.Text.Length < 3)
                    return;
            if (context.Users.Any(u => u.Login == LoginBox.Text))
            {
                MessageBox.Show("Пользователь с данным логином уже существует");
                return;
            }
            if (PasswordBox.Password.Length < 4)
            {
                MessageBox.Show("Пароль не может быть меньше 4-х символов");
                return;
            }
            if (PasswordBox.Password != RepeatPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            if (Regex.IsMatch(PasswordBox.Password, @"[А-Яа-я]"))
            {
                MessageBox.Show("Пароль не может содержать кириллицу");
                return;
            }            
            if (RoleComboBox.SelectedIndex < 0) return;
            user.Password = PasswordBox.Password;
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

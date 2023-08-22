using Payment_Control.Models;
using System;
using System.Collections.Generic;
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

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        User user;
        EntityContext entityContext = new EntityContext();
        public EditUserWindow()
        {
            InitializeComponent();
            foreach (Role role in entityContext.Roles) Role.Items.Add(role);
            this.Closing += Window_Closing;
        }
        public EditUserWindow(User user):this()
        {
            this.user = user;         
            grd.DataContext = user;
            Role.SelectedIndex = user.RoleId - 1;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (entityContext.Users.Any(u => u.Login == Login.Text && u.UserId != user.UserId))
            {
                MessageBox.Show("Пользователь с данным логином уже существует");
                return;
            }
            if (UserName.Text == null || Regex.IsMatch(UserName.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                || UserName.Text.Length < 3)
                return;
            if (Login.Text == null || Regex.IsMatch(Login.Text, @"[А-Яа-я\W\d]") || Login.Text.Length < 3)
                return;            
            if (Role.SelectedIndex < 0) return;
            user.RoleId = Role.SelectedIndex + 1;
            this.DialogResult = true;
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => this.DialogResult = false;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
           entityContext.Dispose();
    }
}

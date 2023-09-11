using Payment_Control.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;


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
            foreach (Role role in entityContext.Roles) RoleComboBox.Items.Add(role);
            this.Closing += Window_Closing;
        }
        public EditUserWindow(User user):this()
        {
            this.user = user;         
            grd.DataContext = user;
            RoleComboBox.SelectedIndex = user.RoleId - 1;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (entityContext.Users.Any(u => u.Login == LoginBox.Text && u.Id != user.Id))
            {
                MessageBox.Show("Пользователь с данным логином уже существует");
                return;
            }
            if (UserNameBox.Text == null || Regex.IsMatch(UserNameBox.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                || UserNameBox.Text.Length < 3)
                return;
            if (LoginBox.Text == null || Regex.IsMatch(LoginBox.Text, @"[А-Яа-я\W\d]") || LoginBox.Text.Length < 3)
                return;            
            if (RoleComboBox.SelectedIndex < 0) return;
            user.RoleId = RoleComboBox.SelectedIndex + 1;
            this.DialogResult = true;
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => this.DialogResult = false;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
           entityContext.Dispose();
    }
}

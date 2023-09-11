using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow() => InitializeComponent();            
        
        private void Log_In_Button_Click(object sender, RoutedEventArgs e)
        {
            User authorizedUser = null;
            using (EntityContext entityContext = new EntityContext())
            {
                authorizedUser = entityContext.Users.Where(u => u.Login == LoginBox.Text
                    && u.Password == PasswordBox.Password).FirstOrDefault();
            }
            if (authorizedUser == null) MessageBox.Show("Проверьте логин и пароль");
            else
            {
                MessageBox.Show("Вы вошли в систему");
                new MainWindow(authorizedUser).Show();
                this.Close();
            }
        }            
    }
}


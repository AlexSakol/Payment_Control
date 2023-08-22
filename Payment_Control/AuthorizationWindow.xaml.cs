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
using System.Windows.Shapes;
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();            
        }

        private void Log_In_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            User authorizedUser = null;
            using (EntityContext entityContext = new EntityContext())
            {
                authorizedUser = entityContext.Users.Where(u => u.Login == login && u.Password == password)
                    .FirstOrDefault();
            }
            if (authorizedUser != null)
            {
                MessageBox.Show("Вы вошли в систему");
                MainWindow mainWindow = new MainWindow(authorizedUser);
                mainWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Проверьте логин и пароль");
        }        
    }
}

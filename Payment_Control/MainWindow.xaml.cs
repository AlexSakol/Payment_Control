using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
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
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
        
    public partial class MainWindow : Window
    {
        EntityContext context = new EntityContext();
        User user;
        public MainWindow()
        {
            InitializeComponent();            
            this.Closing += Window_Closing;
        }

        public MainWindow(User user):this()
        {
            this.user = user;
            this.Title = user.UserName;            
            Incomes.ItemsSource = context.IncomesOfUser(user);
            Payments.ItemsSource = context.PaymentsOfUser(user);
            PaymentCategories.ItemsSource = context.PaymentCategories.ToArray();
            IncomeCategories.ItemsSource = context.IncomeCategories.ToArray();
        }

        private void Add_Payment_Button_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment() { UserId = user.UserId};
            EditPaymentWindow editWindow = new EditPaymentWindow(payment);            
            if(editWindow.ShowDialog() == true)
            {
                context.Payments.Add(payment);
                context.SaveChanges();                
            }
            Payments.ItemsSource = context.PaymentsOfUser(user);
        }

        private void Remove_Payment_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Payments.SelectedItems.Count > 0)
                {
                    for (int i = Payments.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        Payment payment = Payments.SelectedItems[i] as Payment;
                        if (payment != null)
                        {
                            context.Payments.Remove(payment);
                        }
                    }
                    context.SaveChanges();
                }
            }
            Payments.ItemsSource = context.PaymentsOfUser(user);
        }

            private void Edit_Payment_Button_Click(object sender, RoutedEventArgs e)
            {
                Payment payment = Payments.SelectedItem as Payment;
                if (payment == null) return;
                EditPaymentWindow editWindow = new EditPaymentWindow(payment);
                if (editWindow.ShowDialog() == true) context.SaveChanges();
                else
                {
                    if (payment != null)
                    {
                        context.Entry(payment).Reload();
                        Payments.DataContext = null;
                        Payments.DataContext = context.Payments.Local;
                    }
                }
                Payments.ItemsSource = context.PaymentsOfUser(user);
            }

        private void Add_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            Income income = new Income() { UserId = user.UserId};
            EditIncomeWindow editWindow = new EditIncomeWindow(income);            
            if (editWindow.ShowDialog() == true)
            {
                context.Incomes.Add(income);
                context.SaveChanges();                
            }
            Incomes.ItemsSource = context.IncomesOfUser(user);
        }

        private void Remove_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Incomes.SelectedItems.Count > 0)
                {
                    for (int i = Incomes.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        Income income = Incomes.SelectedItems[i] as Income;
                        if (income != null)
                        {
                            context.Incomes.Remove(income);
                        }
                    }
                    context.SaveChanges();
                }
            }
            Incomes.ItemsSource = context.IncomesOfUser(user);
        }

        private void Edit_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            Income income = Incomes.SelectedItem as Income;
            if (income == null) return;
            EditIncomeWindow editWindow = new EditIncomeWindow(income);
            if (editWindow.ShowDialog() == true) context.SaveChanges();
            else
            {
                if (income != null)
                {
                    context.Entry(income).Reload();
                    Incomes.DataContext = null;
                    Incomes.DataContext = context.Incomes.Local;
                }
            }
            Incomes.ItemsSource = context.IncomesOfUser(user);
        }
        
        private void Help_button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для просмотра пользователей нажмите на значок с изображением пользователя \n" +
                "Для добавления/редактирования/удаления пользователей в открывшемся окне нажмите на кнопки " +
                "+/-/редактировать соответсвенно (указанные опреции доступны только администратору) \n"+
                "Для посторения диаграммы нажмите на значок с изображением диаграммы\n"+
                "Для добавления/удаления/редактирования доходов и расходов нажмите на кнопки " +
                "+/-/редактировать под соответсвующей таблицей\n" );
        }

        private void Users_Button_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow(user);
            usersWindow.ShowDialog();
        }
       

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>        
            context.Dispose();

        private void Diagramm_Click(object sender, RoutedEventArgs e)
        {
            DiagrammWindow diagrammWindow = new DiagrammWindow(user);
            diagrammWindow.ShowDialog();
        }
    }
}


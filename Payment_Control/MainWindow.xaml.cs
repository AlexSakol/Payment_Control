﻿using System.Linq;
using System.Windows;
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
            this.Title = user.Name;            
            IncomesDataGrd.ItemsSource = context.IncomesOfUser(user);
            PaymentsDataGrd.ItemsSource = context.PaymentsOfUser(user);
            PaymentCategoriesComboBox.ItemsSource = context.PaymentCategories.ToArray();
            IncomeCategoriesComboBox.ItemsSource = context.IncomeCategories.ToArray();
        }

        private void Add_Payment_Button_Click(object sender, RoutedEventArgs e)
        {
            Payment payment = new Payment() { UserId = user.Id};
            EditPaymentWindow editWindow = new EditPaymentWindow(payment);            
            if(editWindow.ShowDialog() == true)
            {
                context.Payments.Add(payment);
                context.SaveChanges();                
            }
            PaymentsDataGrd.ItemsSource = context.PaymentsOfUser(user);
        }

        private void Remove_Payment_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (PaymentsDataGrd.SelectedItems.Count > 0)
                {
                    for (int i = PaymentsDataGrd.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        Payment payment = PaymentsDataGrd.SelectedItems[i] as Payment;
                        if (payment != null)
                        {
                            context.Payments.Remove(payment);
                        }
                    }
                    context.SaveChanges();
                }
            }
            PaymentsDataGrd.ItemsSource = context.PaymentsOfUser(user);
        }

            private void Edit_Payment_Button_Click(object sender, RoutedEventArgs e)
            {
                Payment payment = PaymentsDataGrd.SelectedItem as Payment;
                if (payment == null) return;
                EditPaymentWindow editWindow = new EditPaymentWindow(payment);
                if (editWindow.ShowDialog() == true) context.SaveChanges();
                else
                {
                    if (payment != null)
                    {
                        context.Entry(payment).Reload();
                        PaymentsDataGrd.DataContext = null;
                        PaymentsDataGrd.DataContext = context.Payments.Local;
                    }
                }
                PaymentsDataGrd.ItemsSource = context.PaymentsOfUser(user);
            }

        private void Add_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            Income income = new Income() { UserId = user.Id};
            EditIncomeWindow editWindow = new EditIncomeWindow(income);            
            if (editWindow.ShowDialog() == true)
            {
                context.Incomes.Add(income);
                context.SaveChanges();                
            }
            IncomesDataGrd.ItemsSource = context.IncomesOfUser(user);
        }

        private void Remove_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (IncomesDataGrd.SelectedItems.Count > 0)
                {
                    for (int i = IncomesDataGrd.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        Income income = IncomesDataGrd.SelectedItems[i] as Income;
                        if (income != null)
                        {
                            context.Incomes.Remove(income);
                        }
                    }
                    context.SaveChanges();
                }
            }
            IncomesDataGrd.ItemsSource = context.IncomesOfUser(user);
        }

        private void Edit_Income_Button_Click(object sender, RoutedEventArgs e)
        {
            Income income = IncomesDataGrd.SelectedItem as Income;
            if (income == null) return;
            EditIncomeWindow editWindow = new EditIncomeWindow(income);
            if (editWindow.ShowDialog() == true) context.SaveChanges();
            else
            {
                if (income != null)
                {
                    context.Entry(income).Reload();
                    IncomesDataGrd.DataContext = null;
                    IncomesDataGrd.DataContext = context.Incomes.Local;
                }
            }
            IncomesDataGrd.ItemsSource = context.IncomesOfUser(user);
        }
        
        private void Help_button(object sender, RoutedEventArgs e) =>        
            MessageBox.Show("Для просмотра пользователей нажмите на значок с изображением пользователя \n" +
                "Для добавления/редактирования/удаления пользователей в открывшемся окне нажмите на кнопки " +
                "+/-/редактировать соответсвенно (указанные опреции доступны только администратору) \n"+
                "Для посторения диаграммы нажмите на значок с изображением диаграммы\n"+
                "Для добавления/удаления/редактирования доходов и расходов нажмите на кнопки " +
                "+/-/редактировать под соответсвующей таблицей\n" );
        
        private void Users_Button_Click(object sender, RoutedEventArgs e) =>        
           new UsersWindow(user).ShowDialog();
        
        private void Diagramm_Click(object sender, RoutedEventArgs e) =>        
            new DiagrammWindow(user).ShowDialog();        

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>        
            context.Dispose();       
    }
}


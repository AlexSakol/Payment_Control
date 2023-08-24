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
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для EditPaymentWindow.xaml
    /// </summary>
    public partial class EditPaymentWindow : Window
    {
        EntityContext context = new EntityContext();
        Payment payment;
        public EditPaymentWindow()
        {
            InitializeComponent();
            foreach (var item in context.PaymentCategories) PaymentCategoryComboBox.Items.Add(item);
            this.Closing += Window_Closing;
        }

        public EditPaymentWindow(Payment payment): this()
        {
            this.payment = payment;
            grd.DataContext = payment;
            PaymentCategoryComboBox.SelectedIndex = payment.PaymentCategoryId - 1;
        }
        
        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentNameBox.Text == null || Regex.IsMatch(PaymentNameBox.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                  || PaymentNameBox.Text.Length < 3)
                return;
            if (PaymentDatePicker.SelectedDate < new DateTime(2000, 01, 01)
                || PaymentDatePicker.SelectedDate > new DateTime(2100, 01, 01))
                return;
            decimal price = 0;
            if (!decimal.TryParse(PaymentPriceBox.Text, out price) ||
            price < 0.01m || price > (decimal)Math.Pow(10, 8)) return;
            if (PaymentCategoryComboBox.SelectedIndex < 0) return;
            payment.PaymentCategoryId = PaymentCategoryComboBox.SelectedIndex + 1;
            DialogResult = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
            context.Dispose();
    }
}

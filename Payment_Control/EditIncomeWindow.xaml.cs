using Payment_Control.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для EditIncomeWindow.xaml
    /// </summary>
    public partial class EditIncomeWindow : Window
    {
        EntityContext context = new EntityContext();
        Income income;
        public EditIncomeWindow()
        {
            InitializeComponent();
            foreach (var item in context.IncomeCategories) IncomeCategoryComboBox.Items.Add(item);            
            this.Closing += Window_Closing;            
        }
        public EditIncomeWindow(Income income) : this()
        {
            this.income = income;
            grd.DataContext = income;
            IncomeCategoryComboBox.SelectedIndex = income.IncomeCategoryId - 1;
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        
        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (IncomeNameBox.Text == null || Regex.IsMatch(IncomeNameBox.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                   || IncomeNameBox.Text.Length < 3)
                return;
            if (IncomeDatePicker.SelectedDate < new DateTime(2000, 01, 01)
                || IncomeDatePicker.SelectedDate > new DateTime(2100, 01, 01))
                return;
            decimal price = 0;
            if (!decimal.TryParse(IncomePriceBox.Text, out price) 
                || price < 0.01m || price > (decimal)Math.Pow(10, 8)) return;
            if (IncomeCategoryComboBox.SelectedIndex < 0) return;
            income.IncomeCategoryId = IncomeCategoryComboBox.SelectedIndex + 1;
            DialogResult = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
           context.Dispose();      

    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            foreach (var item in context.IncomeCategories) IncomeCategory.Items.Add(item);            
            this.Closing += Window_Closing;            
        }
        public EditIncomeWindow(Income income) : this()
        {
            this.income = income;
            grd.DataContext = income;
            IncomeCategory.SelectedIndex = income.IncomeCategoryId - 1;
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
        
        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (IncomeName.Text == null || Regex.IsMatch(IncomeName.Text.Replace(" ", ""), @"[A-Za-z\W\d]")
                   || IncomeName.Text.Length < 3)
                return;
            if (IncomeDate.SelectedDate < new DateTime(2000, 01, 01)
                || IncomeDate.SelectedDate > new DateTime(2100, 01, 01))
                return;
            decimal price = 0;
            if (!decimal.TryParse(IncomePrice.Text, out price) 
                || price < 0.01m || price > (decimal)Math.Pow(10, 8)) return;
            if (IncomeCategory.SelectedIndex < 0) return;
            income.IncomeCategoryId = IncomeCategory.SelectedIndex + 1;
            DialogResult = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
           context.Dispose();      

    }
}

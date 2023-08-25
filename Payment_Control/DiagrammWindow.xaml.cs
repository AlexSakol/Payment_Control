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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Payment_Control.Models;

namespace Payment_Control
{
    /// <summary>
    /// Логика взаимодействия для DiagrammWindow.xaml
    /// </summary>
    public partial class DiagrammWindow : Window
    {
        public PieSeries<decimal>[] PaymentSeries { get; set; }
        public PieSeries<decimal>[] IncomeSeries { get; set; }

        User user;
        public DiagrammWindow() => InitializeComponent();            
        
        public DiagrammWindow(User user): this() => this.user = user;
        
        private void Button_Create_Diagramm_Click(object sender, RoutedEventArgs e)
        {
            Payment[] payments;
            Income[] incomes;            
            using (EntityContext context = new EntityContext())
            {
                payments = context.Payments.Where(p => p.Date >= StartDatePicker.SelectedDate
                && p.Date <= EndDatePicker.SelectedDate && p.UserId == user.Id).ToArray();
                incomes = context.Incomes.Where(i => i.Date >= StartDatePicker.SelectedDate
                && i.Date <= EndDatePicker.SelectedDate && i.UserId == user.Id).ToArray();
                
            }
            IncomeSeries = new PieSeries<decimal>[incomes.Length];
            for (int i = 0; i < incomes.Length; i++)
            {
                IncomeSeries[i] =
                    new PieSeries<decimal>
                    {
                        Name = incomes[i].Name, 
                        Values = new decimal[] { incomes[i].Price }
                    };
            }
            IncomeDiagram.Series = IncomeSeries; 
            PaymentSeries = new PieSeries<decimal>[payments.Length];
            for(int i = 0; i < payments.Length; i++)
            {               
                PaymentSeries[i] =
                    new PieSeries<decimal>
                    {
                        Name = payments[i].Name,
                        Values = new decimal[] { payments[i].Price }
                    };
            }
            PaymentDiagram.Series = PaymentSeries;       
        }
    }
}


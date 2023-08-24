using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    public class PaymentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Payment> Payments { get; set; }
        public override string? ToString() => Name;
        
    }
}

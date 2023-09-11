//Категории  платежей

using System.Collections.ObjectModel;

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

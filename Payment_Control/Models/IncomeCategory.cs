//Категории доходов

using System.Collections.ObjectModel;

namespace Payment_Control.Models
{
    public class IncomeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Income> Incomes { get; set; }
        public override string? ToString() => Name;
        
    }
}

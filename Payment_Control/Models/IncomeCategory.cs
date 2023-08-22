using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    public class IncomeCategory
    {
        public int IncomeCategoryId { get; set; }
        public string IncomeCategoryName { get; set; }
        public ObservableCollection<Income> Incomes { get; set; }
        public override string? ToString() => IncomeCategoryName;
        
    }
}

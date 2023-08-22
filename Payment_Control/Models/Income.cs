using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Payment_Control.Models
{
    public class Income: IDataErrorInfo
    {
        public int IncomeId { get; set; }
        public string IncomeName { get; set; }
        public DateTime IncomeDate { get; set; }
        public decimal IncomePrice { get; set; }
        public bool IsReceived { get; set; }          
        public int IncomeCategoryId { get; set; }        
        public IncomeCategory IncomeCategory { get; set; }            
        public int UserId { get; set; }           
        public User User { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "IncomeName":
                        if (IncomeName != null)
                        {
                            if (Regex.IsMatch(IncomeName.Replace(" ", ""), @"[A-Za-z\W\d]"))
                                error = "Наименование не должно содержать латиницу, спецсимволы и цифры";
                            if (IncomeName.Length < 3)
                                error = "Наименование не может быть меньше 3-х символов";
                        }
                        else error = "Введите наименовнаие";
                        break;
                    case "IncomeDate":
                        if (IncomeDate < new DateTime(2000, 01, 01) || IncomeDate > new DateTime(2100, 01, 01))
                            error = "Дата должна быть в диапазоне от 01.01.2000 до 01.01.2100";
                        break;
                    case "IncomePrice":
                        if (IncomePrice < 0.01m || IncomePrice > (decimal)Math.Pow(10, 8))
                            error = "Сумма должна быть в диапазоне от 0.01 до 100000000";
                        break;                    
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();
        
    }
}

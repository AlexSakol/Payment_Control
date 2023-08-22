using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    public class Payment: IDataErrorInfo
    {
        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentPrice { get; set; }        
        public bool IsPayed { get; set; }
        public int PaymentCategoryId { get; set; } 
        public PaymentCategory PaymentCategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "PaymentName":
                        if (PaymentName != null)
                        {
                            if (Regex.IsMatch(PaymentName.Replace(" ", ""), @"[A-Za-z\W\d]"))
                                error = "Наименование не должно содержать латиницу, спецсимволы и цифры";
                            if (PaymentName.Length < 3)
                                error = "Наименование не может быть меньше 3-х символов";
                        }
                        else error = "Введите наименование";
                        break;
                    case "PaymentDate":
                        if (PaymentDate < new DateTime(2000, 01, 01) || PaymentDate > new DateTime(2100, 01, 01))
                            error = "Дата должна быть в диапазоне от 01.01.2000 до 01.01.2100";
                        break;
                    case "PaymentPrice":
                        if (PaymentPrice < 0.01m || PaymentPrice > (decimal)Math.Pow(10, 8))
                            error = "Сумма должна быть в диапазоне от 0.01 до 100000000";
                        break;
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Payment_Control.Models
{
    public class Payment: IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }        
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
                    case "Name":
                        if (Name != null)
                        {
                            if (Regex.IsMatch(Name.Replace(" ", ""), @"[A-Za-z\W\d]"))
                                error = "Наименование не должно содержать латиницу, спецсимволы и цифры";
                            if (Name.Length < 3)
                                error = "Наименование не может быть меньше 3-х символов";
                        }
                        else error = "Введите наименование";
                        break;
                    case "Date":
                        if (Date < new DateTime(2000, 01, 01) || Date > new DateTime(2100, 01, 01))
                            error = "Дата должна быть в диапазоне от 01.01.2000 до 01.01.2100";
                        break;
                    case "Price":
                        if (Price < 0.01m || Price > (decimal)Math.Pow(10, 8))
                            error = "Сумма должна быть в диапазоне от 0.01 до 100000000";
                        break;
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}

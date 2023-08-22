using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    public class User: IDataErrorInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }        
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "UserName":
                        if (UserName != null)
                        {
                            if (Regex.IsMatch(UserName.Replace(" ", ""), @"[A-Za-z\W\d]"))
                                error = "ФИО пользователя не должно содержать латиницу, спецсимволы и цифры";
                            if (UserName.Length < 3)
                                error = "ФИО не может быть меньше 3-х символов";
                        }
                        else error = "Введите ФИО";
                        break;
                    case "Login":
                        if (Login != null)
                        {
                            if (Regex.IsMatch(Login, @"[А-Яа-я\W\d]"))
                                error = "Логин не должен содержать криллицу, спецсимволы и цифры";
                            if (Login.Length < 3)
                                error = "Логин не может быть меньше 3-х символов";
                        }
                        else error = "Введите логин";
                        break;                    
                }
                return error;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}

﻿//Класс пользователей

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Payment_Control.Models
{
    public class User: IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
                    case "Name":
                        if (Name != null)
                        {
                            if (Regex.IsMatch(Name.Replace(" ", ""), @"[A-Za-z\W\d]"))
                                error = "ФИО пользователя не должно содержать латиницу, спецсимволы и цифры";
                            if (Name.Length < 3)
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

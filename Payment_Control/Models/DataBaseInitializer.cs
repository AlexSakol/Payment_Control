using System;
using System.Data.Entity;

namespace Payment_Control.Models
{
    internal class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    { 
        protected override void Seed(EntityContext context) // заполняет базу данных тестовыми данными 
        {
            #region Тестовые данные для пользователей
            Role[] roles = new Role[]
            {
                new Role(){Id = 1,Name = "Администратор"},
                new Role(){Id = 2, Name = "Пользователь"}
            };
            context.Roles.AddRange(roles);
            User[] users = new User[]
            {
                new User() {Id = 1, Name = "Admin", Login = "admin",
                    Password= "admin", Role = roles[0] },
                new User() {Id = 2, Name = "Иванов Иван", Login = "Ivan",
                    Password= "1111", Role = roles[1] },
                new User() {Id = 3, Name = "Мария Петрова", Login = "Masha",
                    Password= "1111", Role = roles[1]}
            };          
            context.Users.AddRange(users);
            #endregion
            #region Тестовые данные доходов
            IncomeCategory[] incomeCategories =
            {
                new IncomeCategory {Id = 1, Name = "Заработная плата" },
                new IncomeCategory {Id = 2, Name = "Банковские операции" },
                new IncomeCategory {Id = 3, Name = "Продажа вещей" },
                new IncomeCategory {Id = 4, Name = "Сдача в аренду", },
                new IncomeCategory {Id = 5, Name = "Прочее"}
            };
            context.IncomeCategories.AddRange(incomeCategories);
            Income[] incomes = new Income[]
            {
                new Income(){Id = 1, Name = "Аванс",  Date = new DateTime(2023, 05, 10),
                Price = 400m, IncomeCategory = incomeCategories[0],
                    IsReceived = true, User = users[0] },
                new Income(){Id = 2, Name = "Проценты на карту",  Date = new DateTime(2023, 05, 15),
                Price = 50m, IncomeCategory = incomeCategories[1],
                    IsReceived = true, User = users[0] },
                new Income(){Id = 3, Name = "Продажа телефона",  Date = new DateTime(2023, 05, 09),
                Price = 150m, IncomeCategory = incomeCategories[2],
                    IsReceived = true, User = users[0] },
                new Income(){Id = 4, Name = "Зарплата",  Date = new DateTime(2023, 05, 27),
                Price = 1000m, IncomeCategory = incomeCategories[0],
                    IsReceived = false, User = users[0] },
                new Income(){Id = 5, Name = "Аванс",  Date = new DateTime(2023, 05, 12),
                Price = 500m, IncomeCategory = incomeCategories[0], 
                    IsReceived = true, User = users[1] },
                new Income(){Id = 6, Name = "Проценты по вкладу",  Date = new DateTime(2023, 05, 10),
                Price = 10.98m, IncomeCategory = incomeCategories[1],
                    IsReceived = true, User = users[1] },
                new Income(){Id = 7, Name = "Арендная плата за квартиру",  Date = new DateTime(2023, 05, 15),
                Price = 450m, IncomeCategory = incomeCategories[3], 
                    IsReceived = true, User = users[1] },
                new Income(){Id = 8, Name = "Зарплата",  Date = new DateTime(2023, 05, 25),
                Price = 1200m, IncomeCategory = incomeCategories[0], 
                    IsReceived = false, User = users[1] },
                new Income(){Id = 9, Name = "Аванс",  Date = new DateTime(2023, 05, 05),
                Price = 800m, IncomeCategory = incomeCategories[0],
                    IsReceived = true, User = users[2] },
                new Income(){Id = 10, Name = "Продажа платья",  Date = new DateTime(2023, 05, 10),
                Price = 55.50m, IncomeCategory = incomeCategories[2],
                    IsReceived = true, UserId = users[2].Id },
                new Income(){Id = 11, Name = "Продажа ноутбука",  Date = new DateTime(2023, 05, 12),
                Price = 450m, IncomeCategory = incomeCategories[2], 
                    IsReceived = true, User = users[2] },
                new Income(){Id = 12, Name = "Зарплата",  Date = new DateTime(2023, 05, 20),
                Price = 800m, IncomeCategory = incomeCategories[0], 
                    IsReceived = false, User = users[2] }
            };
            context.Incomes.AddRange(incomes);
            #endregion
            #region Тестовые данные для расходов     
            PaymentCategory[] paymentCategories =
            {
                new PaymentCategory {Id = 1, Name = "Авто"},
                new PaymentCategory {Id = 2, Name = "Продукты" },
                new PaymentCategory{Id = 3 ,Name = "Одежда и обувь" },
                new PaymentCategory{Id = 4, Name = "Лекарства" },
                new PaymentCategory{Id = 5, Name = "Коммунальные платежи" },
                new PaymentCategory{Id = 6, Name = "Прочее" }
            };
            context.PaymentCategories.AddRange(paymentCategories);
            Payment[] payments = new Payment[]
            {
                new Payment(){Id = 1, Name = "Бензин", Date = new DateTime(2023, 05, 12),
                Price = 60m, PaymentCategory = paymentCategories[0], 
                    IsPayed = true, User = users[0] },
                new Payment(){Id = 2, Name = "Фрукты", Date = new DateTime(2023, 05, 05),
                Price = 30m, PaymentCategory = paymentCategories[1], 
                    IsPayed = true, User = users[0] },
                new Payment(){Id = 3, Name = "Ботинки", Date = new DateTime(2023, 05, 23),
                Price = 100m, PaymentCategory = paymentCategories[2], 
                    IsPayed = true, User = users[0] },
                new Payment(){Id = 4, Name = "Шины", Date = new DateTime(2023, 05, 11),
                Price = 400m, PaymentCategory = paymentCategories[0], 
                    IsPayed = true, User = users[1] },
                new Payment(){Id = 5, Name = "Аспирин", Date = new DateTime(2023, 05, 03),
                Price = 0.5m, PaymentCategory = paymentCategories[3],
                    IsPayed = true, User = users[1] },
                new Payment(){Id = 6,Name = "Электроэнергия", Date = new DateTime(2023, 05, 25),
                Price = 30m, PaymentCategory = paymentCategories[4], 
                    IsPayed = false, User = users[1] },
                new Payment(){Id = 7, Name = "Помада", Date = new DateTime(2023, 05, 22),
                Price = 1.99m, PaymentCategory = paymentCategories[5],
                    IsPayed = true, User = users[2] },
                new Payment(){Id = 8, Name = "Туфли", Date = new DateTime(2023, 05, 15),
                Price = 120m, PaymentCategory = paymentCategories[2], 
                    IsPayed = true, User = users[2] },
                new Payment(){Id = 9, Name = "Водоснабжение", Date = new DateTime(2023, 05, 25),
                Price = 30m, PaymentCategory = paymentCategories[4],
                    IsPayed = false, User = users[2] }
            };
            context.Payments.AddRange(payments);
            #endregion
        }
    }
}

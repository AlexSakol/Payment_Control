using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    internal class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    { 
        protected override void Seed(EntityContext context) // заполняет базу данных тестовыми данными 
        {
            #region Тестовые данные для пользователей
            Role[] roles = new Role[]
            {
                new Role(){RoleId = 1,RoleName = "Администратор"},
                new Role(){RoleId = 2, RoleName = "Пользователь"}
            };
            context.Roles.AddRange(roles);
            User[] users = new User[]
            {
                new User() {UserId = 1, UserName = "Admin", Login = "admin",
                    Password= "admin", Role = roles[0] },
                new User() {UserId = 2, UserName = "Иванов Иван", Login = "Ivan",
                    Password= "1111", Role = roles[1] },
                new User() {UserId = 3, UserName = "Мария Петрова", Login = "Masha",
                    Password= "1111", Role = roles[1]}
            };          
            context.Users.AddRange(users);
            #endregion
            #region Тестовые данные доходов
            IncomeCategory[] incomeCategories =
            {
                new IncomeCategory {IncomeCategoryId = 1, IncomeCategoryName = "Заработная плата" },
                new IncomeCategory {IncomeCategoryId = 2, IncomeCategoryName = "Банковские операции" },
                new IncomeCategory {IncomeCategoryId = 3, IncomeCategoryName = "Продажа вещей" },
                new IncomeCategory {IncomeCategoryId = 4, IncomeCategoryName = "Сдача в аренду", },
                new IncomeCategory {IncomeCategoryId = 5, IncomeCategoryName = "Прочее"}
            };
            context.IncomeCategories.AddRange(incomeCategories);
            Income[] incomes = new Income[]
            {
                new Income(){IncomeId = 1, IncomeName = "Аванс",  IncomeDate = new DateTime(2023, 05, 10),
                IncomePrice = 400m, IncomeCategory = incomeCategories[0],
                    IsReceived = true, User = users[0] },
                new Income(){IncomeId = 2, IncomeName = "Проценты на карту",  IncomeDate = new DateTime(2023, 05, 15),
                IncomePrice = 50m, IncomeCategory = incomeCategories[1],
                    IsReceived = true, User = users[0] },
                new Income(){IncomeId = 3, IncomeName = "Продажа телефона",  IncomeDate = new DateTime(2023, 05, 09),
                IncomePrice = 150m, IncomeCategory = incomeCategories[2],
                    IsReceived = true, User = users[0] },
                new Income(){IncomeId = 4, IncomeName = "Зарплата",  IncomeDate = new DateTime(2023, 05, 27),
                IncomePrice = 1000m, IncomeCategory = incomeCategories[0],
                    IsReceived = false, User = users[0] },
                new Income(){IncomeId = 5, IncomeName = "Аванс",  IncomeDate = new DateTime(2023, 05, 12),
                IncomePrice = 500m, IncomeCategory = incomeCategories[0], 
                    IsReceived = true, User = users[1] },
                new Income(){IncomeId = 6, IncomeName = "Проценты по вкладу",  IncomeDate = new DateTime(2023, 05, 10),
                IncomePrice = 10.98m, IncomeCategory = incomeCategories[1],
                    IsReceived = true, User = users[1] },
                new Income(){IncomeId = 7, IncomeName = "Арендная плата за квартиру",  IncomeDate = new DateTime(2023, 05, 15),
                IncomePrice = 450m, IncomeCategory = incomeCategories[3], 
                    IsReceived = true, User = users[1] },
                new Income(){IncomeId = 8, IncomeName = "Зарплата",  IncomeDate = new DateTime(2023, 05, 25),
                IncomePrice = 1200m, IncomeCategory = incomeCategories[0], 
                    IsReceived = false, User = users[1] },
                new Income(){IncomeId = 9, IncomeName = "Аванс",  IncomeDate = new DateTime(2023, 05, 05),
                IncomePrice = 800m, IncomeCategory = incomeCategories[0],
                    IsReceived = true, User = users[2] },
                new Income(){IncomeId = 10, IncomeName = "Продажа платья",  IncomeDate = new DateTime(2023, 05, 10),
                IncomePrice = 55.50m, IncomeCategory = incomeCategories[2],
                    IsReceived = true, UserId = users[2].UserId },
                new Income(){IncomeId = 11, IncomeName = "Продажа ноутбука",  IncomeDate = new DateTime(2023, 05, 12),
                IncomePrice = 450m, IncomeCategory = incomeCategories[2], 
                    IsReceived = true, User = users[2] },
                new Income(){IncomeId = 12, IncomeName = "Зарплата",  IncomeDate = new DateTime(2023, 05, 20),
                IncomePrice = 800m, IncomeCategory = incomeCategories[0], 
                    IsReceived = false, User = users[2] }
            };
            context.Incomes.AddRange(incomes);
            #endregion
            #region Тестовые данные для расходов     
            PaymentCategory[] paymentCategories =
            {
                new PaymentCategory {PaymentCategoryId = 1, PaymentCategoryName = "Авто"},
                new PaymentCategory {PaymentCategoryId = 2, PaymentCategoryName = "Продукты" },
                new PaymentCategory{PaymentCategoryId = 3 ,PaymentCategoryName = "Одежда и обувь" },
                new PaymentCategory{PaymentCategoryId = 4, PaymentCategoryName = "Лекарства" },
                new PaymentCategory{PaymentCategoryId = 5, PaymentCategoryName = "Коммунальные платежи" },
                new PaymentCategory{PaymentCategoryId = 6, PaymentCategoryName = "Прочее" }
            };
            context.PaymentCategories.AddRange(paymentCategories);
            Payment[] payments = new Payment[]
            {
                new Payment(){PaymentId = 1, PaymentName = "Бензин", PaymentDate = new DateTime(2023, 05, 12),
                PaymentPrice = 60m, PaymentCategory = paymentCategories[0], 
                    IsPayed = true, User = users[0] },
                new Payment(){PaymentId = 2, PaymentName = "Фрукты", PaymentDate = new DateTime(2023, 05, 05),
                PaymentPrice = 30m, PaymentCategory = paymentCategories[1], 
                    IsPayed = true, User = users[0] },
                new Payment(){PaymentId = 3, PaymentName = "Ботинки", PaymentDate = new DateTime(2023, 05, 23),
                PaymentPrice = 100m, PaymentCategory = paymentCategories[2], 
                    IsPayed = true, User = users[0] },
                new Payment(){PaymentId = 4, PaymentName = "Шины", PaymentDate = new DateTime(2023, 05, 11),
                PaymentPrice = 400m, PaymentCategory = paymentCategories[0], 
                    IsPayed = true, User = users[1] },
                new Payment(){PaymentId = 5, PaymentName = "Аспирин", PaymentDate = new DateTime(2023, 05, 03),
                PaymentPrice = 0.5m, PaymentCategory = paymentCategories[3],
                    IsPayed = true, User = users[1] },
                new Payment(){PaymentId = 6,PaymentName = "Электроэнергия", PaymentDate = new DateTime(2023, 05, 25),
                PaymentPrice = 30m, PaymentCategory = paymentCategories[4], 
                    IsPayed = false, User = users[1] },
                new Payment(){PaymentId = 7, PaymentName = "Помада", PaymentDate = new DateTime(2023, 05, 22),
                PaymentPrice = 1.99m, PaymentCategory = paymentCategories[5],
                    IsPayed = true, User = users[2] },
                new Payment(){PaymentId = 8, PaymentName = "Туфли", PaymentDate = new DateTime(2023, 05, 15),
                PaymentPrice = 120m, PaymentCategory = paymentCategories[2], 
                    IsPayed = true, User = users[2] },
                new Payment(){PaymentId = 9, PaymentName = "Водоснабжение", PaymentDate = new DateTime(2023, 05, 25),
                PaymentPrice = 30m, PaymentCategory = paymentCategories[4],
                    IsPayed = false, User = users[2] }
            };
            context.Payments.AddRange(payments);
            #endregion
        }
    }
}

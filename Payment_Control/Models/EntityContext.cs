using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
   public class EntityContext: DbContext
    {
        public EntityContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DataBaseInitializer());
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentCategory> PaymentCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }        

        //нахождение расходов пользователя
        public List<Income> IncomesOfUser(User user) => Incomes.Where(i => i.UserId == user.Id)
            .ToList();

        // нахождение доходов пользователя
        public List<Payment> PaymentsOfUser(User user) => Payments.Where(p => p.UserId == user.Id)
            .ToList();
        
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Control.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public override string? ToString() => Name;
        
    }
}

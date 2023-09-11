using System.Collections.ObjectModel;


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

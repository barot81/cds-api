namespace Zhealthcare.Service.Models
{
    public class UserContext
    {
        public UserContext(string name)
        => Name = name;
        
        public string Name { get; set; }
    }
}

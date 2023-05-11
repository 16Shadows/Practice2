namespace WebAPP.Models
{
    public class AccountViewModel
    {
        public string Name { get; set; }

        public AccountViewModel(string name)
        {
            Name = name;
        }
    }
}

namespace SolicitacaoDeMateriais.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<RequestProductService>? Requests { get; set; } = new List<RequestProductService>();
        public virtual ICollection<StockExit>? StockExits { get; set; } = new List<StockExit>();

    }
}

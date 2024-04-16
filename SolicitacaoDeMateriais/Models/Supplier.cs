namespace SolicitacaoDeMateriais.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string StateRegistration { get; set; }
        public string CountyRegistration { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        public virtual ICollection<Service>? Services { get; set; } = new List<Service>();
       
    }
}

using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string StateRegistration { get; set; }
        public string CountyRegistration { get; set; }
      
    }
}

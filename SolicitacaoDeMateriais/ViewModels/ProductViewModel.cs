using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; } 
        public int SupplirName { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string EANcode { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
    }
}

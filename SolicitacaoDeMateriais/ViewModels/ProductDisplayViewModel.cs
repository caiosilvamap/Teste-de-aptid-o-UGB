using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class ProductDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SupplierName { get; set; }
        public string EANcode { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
     
    }
}

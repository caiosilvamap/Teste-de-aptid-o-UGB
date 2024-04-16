using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class StockDisplayViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductEANcode { get; set; }
        public string ProductSupplierName { get; set; }
        public int MinimumStock { get; set; }
        public int Quantity { get; set; }
    }
}

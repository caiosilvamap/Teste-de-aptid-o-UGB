using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("SupplierObj")]
        public int SupplierId { get; set; }
        public string Manufacturer { get; set; }
        public string EANcode { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
        public virtual Supplier? SupplierObj { get; set; }
        public virtual ICollection<RequestProductService>? Requests { get; set; } = new List<RequestProductService>();
        public virtual ICollection<StockExit>? StockExits { get; set; } = new List<StockExit>();
        public virtual ICollection<StockEntry>? StockEntrys { get; set; } = new List<StockEntry>();
        public virtual ICollection<Stock>? Stocks { get; set; } = new List<Stock>();
    }
}

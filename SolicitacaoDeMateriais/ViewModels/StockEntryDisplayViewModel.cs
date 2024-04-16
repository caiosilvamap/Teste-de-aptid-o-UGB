using System.Globalization;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class StockEntryDisplayViewModel
    {
        public int Id { get; set; }
        public string ProductEANcode { get; set; }
        public string ProductName { get; set; }
        public string ProductSupplierName { get; set; }
        public string NF { get; set; }
        public int Quantity { get; set; }
        public string StorageStock { get; set; }
        public DateTime CreationDate { get; set; } 
    }
}

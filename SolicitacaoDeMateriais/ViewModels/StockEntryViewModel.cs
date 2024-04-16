using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class StockEntryViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string NF { get; set; }
        public int Quantity { get; set; }
        public string StorageStock { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow.AddHours(-3);
    }

}

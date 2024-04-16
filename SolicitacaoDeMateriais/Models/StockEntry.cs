using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class StockEntry
    {
        public int Id { get; set; }
        [ForeignKey("ProductObj")]
        public int ProductId { get; set; }
        public string NF {  get; set; }
        public int Quantity { get; set; }
        public string StorageStock { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow.AddHours(-3);
        public virtual Product? ProductObj { get; set; }
    }
}

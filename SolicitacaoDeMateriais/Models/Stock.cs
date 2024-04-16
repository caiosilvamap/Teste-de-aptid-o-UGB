using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class Stock
    {
        public int Id { get; set; }
        [ForeignKey("ProductObj")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product? ProductObj { get; set; }

    }
}

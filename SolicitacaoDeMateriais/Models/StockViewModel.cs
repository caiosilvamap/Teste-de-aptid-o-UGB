using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class StockViewModel
    {
        public int Id { get; set; }     
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}

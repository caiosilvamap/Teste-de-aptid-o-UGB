using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class StockExit
    {
        public int Id { get; set; }
        [ForeignKey("ProductObj")]
        public int ProductId { get; set; }
        [ForeignKey("UserObj")]
        public int UserId { get; set; }
        [ForeignKey("DepartmentObj")]
        public int DepartmentId { get; set; }
        public int RequisitionQuantity {  get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.AddHours(-3);
        public virtual Product? ProductObj { get; set; }
        public virtual User? UserObj { get; set; }
        public virtual Department? DepartmentObj { get;set; }
     


    }
}

using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class RequestProductServiceViewModel
    {
        public int Id { get; set; }
        public string? RequestCode { get; set; }
        [ForeignKey("ProductObj")]
        public int? ProductId { get; set; }
        [ForeignKey("UserObj")]
        public int UserId { get; set; }
        [ForeignKey("ServiceObj")]
        public int? ServiceId { get; set; }
        [ForeignKey("DepartmentObj")]
        public int DepartmentId { get; set; }
        public int? ProductQuantity { get; set; }
        public string? Observation { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Product? ProductObj { get; set; }
        public virtual User? UserObj { get; set; }
        public virtual Service? ServiceObj { get; set; }
        public virtual Department? DepartmentObj { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("SupplierObj")]
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public int Deadline { get; set; }
        public virtual Supplier? SupplierObj { get; set; }
        public virtual ICollection<RequestProductService>? Requests { get; set; } = new List<RequestProductService>();
    }
}

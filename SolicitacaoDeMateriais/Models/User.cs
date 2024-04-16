using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.Models
{
    public class User
    {
        public int Id { get; set; }
        [ForeignKey("DepartmentObj")]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public bool Active { get; set; }
        public virtual Department? DepartmentObj { get; set; }
        public virtual ICollection<RequestProductService>? Requests { get; set; } = new List<RequestProductService>();
        public virtual ICollection<StockExit>? StockExits { get; set; } = new List<StockExit>();
    }
}

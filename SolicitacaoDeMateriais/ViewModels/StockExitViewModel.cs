using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class StockExitViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }  
        public int DepartmentId { get; set; }
        public int RequisitionQuantity { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow.AddHours(-3);
 
    }
}

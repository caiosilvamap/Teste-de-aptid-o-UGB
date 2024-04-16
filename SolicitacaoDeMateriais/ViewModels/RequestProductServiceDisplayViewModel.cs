using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class RequestProductServiceDisplayViewModel
    {
        public int Id { get; set; }
        public string? RequestCode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductEANcode { get; set; }
        public int? ProductQuantity { get; set; }
        public string? ProductManufacturer { get; set; }
        public string DepartmentName { get; set; }
        public string UserName { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceSupplier { get; set; }
        public string? Observation { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}

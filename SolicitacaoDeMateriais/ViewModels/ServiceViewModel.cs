using SolicitacaoDeMateriais.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public int Deadline { get; set; }

    }
}

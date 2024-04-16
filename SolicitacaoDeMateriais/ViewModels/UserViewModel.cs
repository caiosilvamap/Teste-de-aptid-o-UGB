using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public bool Active { get; set; }
        public virtual Department? DepartmentObj { get; set; }

    }
}

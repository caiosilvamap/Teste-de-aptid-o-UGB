using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.ViewModels
{
    public class UserDisplayViewModel
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Registration { get; set; }
            public bool Active { get; set; }
            public string DepartmentName { get; set; }
    }
}

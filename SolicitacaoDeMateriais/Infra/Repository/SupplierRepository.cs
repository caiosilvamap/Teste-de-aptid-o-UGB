using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context)
        {
        }
    }
}

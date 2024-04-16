using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class RequestProductServiceRepository : Repository<RequestProductService>, IRequestProductServiceRepository
    {
        public RequestProductServiceRepository(DataContext context) : base(context)
        {
        }
    }
}

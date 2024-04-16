using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class StockExitRepository : Repository<StockExit>, IStockExitRepository
    {
        public StockExitRepository(DataContext context) : base(context)
        {
        }
    }
}

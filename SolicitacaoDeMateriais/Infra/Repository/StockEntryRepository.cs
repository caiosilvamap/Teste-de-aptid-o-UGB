using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class StockEntryRepository : Repository<StockEntry>, IStockEntryRepository
    {
        public StockEntryRepository(DataContext context) : base(context)
        {
        }
    }
}

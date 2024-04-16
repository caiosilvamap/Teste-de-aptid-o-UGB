using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(DataContext context) : base(context)
        {
        }
    }
}

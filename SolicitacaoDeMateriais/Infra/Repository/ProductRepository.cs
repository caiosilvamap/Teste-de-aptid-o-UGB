using SolicitacaoDeMateriais.Infra.Data;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;

namespace SolicitacaoDeMateriais.Infra.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public StockController(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Stock> stockListModel = _stockRepository.FindAllAsync().Result.ToList();
                List<StockDisplayViewModel> stockListView = _mapper.Map<List<StockDisplayViewModel>>(stockListModel);
                return View(stockListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar Stock de produtos", ex);
            }

        }
    }
}

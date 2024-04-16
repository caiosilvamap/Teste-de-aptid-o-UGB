using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Infra.Repository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;
using System.Linq;

namespace SolicitacaoDeMateriais.Controllers
{
    public class StockEntryController : Controller
    {
        private readonly IStockEntryRepository _stockEntryRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public StockEntryController(IStockEntryRepository stockEntryRepository, IMapper mapper, IProductRepository productRepository, IStockRepository stockRepository)
        {
            _stockEntryRepository = stockEntryRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<StockEntry> stockEntryListModel = _stockEntryRepository.FindAllAsync().Result.ToList();
                List<StockEntryDisplayViewModel> stockEntryListView = _mapper.Map<List<StockEntryDisplayViewModel>>(stockEntryListModel);
                return View(stockEntryListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar entrada de produtos", ex);
            }
        }

        public IActionResult CreateView()
        {
            ViewBag.product = _productRepository.FindAllAsync().Result.ToList();
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {
            try
            {
                ViewBag.product = _productRepository.FindAllAsync().Result.ToList();

                StockEntry stockEntryModel = await _stockEntryRepository.FindIdNoTrackingAsync(id);
                StockEntryViewModel stockEntryView = _mapper.Map<StockEntryViewModel>(stockEntryModel);

                if (stockEntryView == null)
                {
                    NotFound();
                }

                return View(stockEntryView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar entrada de produto", ex);
            }
        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                StockEntry stockEntryModel = await _stockEntryRepository.FindIdNoTrackingAsync(id);
                StockEntryViewModel stockEntryView = _mapper.Map<StockEntryViewModel>(stockEntryModel);

                if (stockEntryView == null)
                {
                    NotFound();
                }

                ViewBag.stockEntryId = stockEntryView.Id;
                return View(stockEntryView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar entrada de produto", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(StockEntryViewModel stockEntryView)
        {
            try
            {
                StockEntry stockEntryDb = _stockEntryRepository.FindIdNoTrackingAsync(stockEntryView.Id).Result;

                

                if (stockEntryView.ProductId != stockEntryDb.ProductId || stockEntryView.Quantity != stockEntryDb.Quantity )
                {
                    var stockEditedId = _stockRepository.WhereAsync(x => x.ProductId == stockEntryView.ProductId).Result.FirstOrDefault().Id;

                    List<StockEntry> stockEntryListDb = _stockEntryRepository.WhereAsync(x => x.ProductId == stockEntryView.ProductId).Result.ToList();

                    int totalStockQuantity = (stockEntryListDb.Sum(entry => entry.Quantity) + stockEntryView.Quantity) - stockEntryDb.Quantity;

                    Stock stock = new();
                    {
                        stock.Id = stockEditedId;
                        stock.Quantity = totalStockQuantity;
                        stock.ProductId = stockEntryView.ProductId;
                    }

                    await _stockRepository.EditAsync(stock);
                }

                if (ModelState.IsValid)
                {
                    StockEntry stockEntryModel = _mapper.Map<StockEntry>(stockEntryView);
                    await _stockEntryRepository.EditAsync(stockEntryModel);
                    return RedirectToAction("Index");
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao editar", ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(StockEntryViewModel stockEntryView)
        {
            try
            {
                

                List<StockEntry> stockEntryListDb = _stockEntryRepository.WhereAsync(x => x.ProductId == stockEntryView.ProductId).Result.ToList();          

                if (stockEntryListDb.Any() && ModelState.IsValid)
                {
                    int totalStockQuantity = stockEntryListDb.Sum(entry => entry.Quantity) + stockEntryView.Quantity;

                    var stockEditedId = _stockRepository.WhereAsync(x => x.ProductId == stockEntryView.ProductId).Result.FirstOrDefault().Id;

                    Stock stock = new();
                    {
                        stock.Id = stockEditedId;
                        stock.Quantity = totalStockQuantity;
                        stock.ProductId = stockEntryView.ProductId;
                    }

                    await _stockRepository.EditAsync(stock);                 
                } 
                else
                {
                    Stock stock = new();
                    {
                        stock.Quantity = stockEntryView.Quantity;
                        stock.ProductId = stockEntryView.ProductId;
                    }
                    await _stockRepository.CreateAsync(stock);
                }

                if (ModelState.IsValid)
                {
                    StockEntry stockEntryModel = _mapper.Map<StockEntry>(stockEntryView);

                   
                    await _stockEntryRepository.CreateAsync(stockEntryModel);
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao adicionar: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                StockEntry stockEntryDelete = await _stockEntryRepository.FindIdNoTrackingAsync(id);


                if (stockEntryDelete != null)
                {
                    await _stockEntryRepository.DeleteAsync(stockEntryDelete);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao Excluir: {ex.Message}");
            }
        }
    }
}


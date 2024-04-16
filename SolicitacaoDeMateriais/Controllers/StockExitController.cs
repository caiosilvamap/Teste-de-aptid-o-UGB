using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Infra.Repository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class StockExitController : Controller
    {
        private readonly IStockEntryRepository _stockEntryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStockExitRepository _stockExitRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public StockExitController(IStockEntryRepository stockEntryRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IStockExitRepository stockExitRepository,
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            IStockRepository stockRepository)
        {
            _stockEntryRepository = stockEntryRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _stockExitRepository = stockExitRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _stockRepository = stockRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<StockExit> stockExitListModel = _stockExitRepository.FindAllAsync().Result.ToList();
                List<StockExitDisplayViewModel> stockExitListView = _mapper.Map<List<StockExitDisplayViewModel>>(stockExitListModel);
                return View(stockExitListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar saída de produtos", ex);
            }
        }

        public IActionResult CreateView()
        {
            List<Product> productList = _productRepository.FindAllAsync().Result.ToList();

            ViewBag.product = productList.Where(product =>
            _stockEntryRepository.WhereAsync(entry => entry.ProductId == product.Id).Result.Any());

            ViewBag.user = _userRepository.FindAllAsync().Result.ToList();

            ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {
            try
            {
                List<Product> productList = _productRepository.FindAllAsync().Result.ToList();

                ViewBag.product = productList.Where(product =>
                _stockEntryRepository.WhereAsync(entry => entry.ProductId == product.Id).Result.Any());

                ViewBag.user = _userRepository.FindAllAsync().Result.ToList();

                ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();

                StockExit stockExitModel = await _stockExitRepository.FindIdNoTrackingAsync(id);
                StockExitViewModel stockExitView = _mapper.Map<StockExitViewModel>(stockExitModel);

                if (stockExitView == null)
                {
                    NotFound();
                }

                return View(stockExitView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar saída de produto", ex);
            }
        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                StockExit stockExitModel = await _stockExitRepository.FindIdNoTrackingAsync(id);
                StockExitViewModel stockExitView = _mapper.Map<StockExitViewModel>(stockExitModel);

                if (stockExitView == null)
                {
                    NotFound();
                }

                ViewBag.stockExitId = stockExitView.Id;
                return View(stockExitView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar saída de produto", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(StockExitViewModel stockExitView)
        {
            try
            {
                StockExit stockExitDb = _stockExitRepository.FindIdNoTrackingAsync(stockExitView.Id).Result;

                List<StockEntry> stockEntryListDb = _stockEntryRepository.WhereAsync(x => x.ProductId == stockExitView.ProductId).Result.ToList();
                var quantityEntry = stockEntryListDb.Sum(x => x.Quantity);
                var stockQuantity = quantityEntry - stockExitView.RequisitionQuantity;

                if(stockExitView.ProductId != stockExitDb.ProductId || stockExitView.RequisitionQuantity != stockExitDb.RequisitionQuantity && stockQuantity >= 0)
                {
                    var stockEditedId = _stockRepository.WhereAsync(x => x.ProductId == stockExitView.ProductId).Result.FirstOrDefault().Id;
        
                    Stock stock = new();
                    {
                        stock.Id = stockEditedId;
                        stock.Quantity = stockQuantity;
                        stock.ProductId = stockExitView.ProductId;
                    }

                    await _stockRepository.EditAsync(stock);

                }

                if (ModelState.IsValid && stockQuantity >= 0)
                {
                    StockExit stockExitModel = _mapper.Map<StockExit>(stockExitView);
                    await _stockExitRepository.EditAsync(stockExitModel);
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
        public async Task<IActionResult> Create(StockExitViewModel stockExitView)
        {
            try
            {
                List<Stock> stockListDb = _stockRepository.WhereAsync(x => x.ProductId == stockExitView.ProductId).Result.ToList();
                var quantityStock = stockListDb.Sum(x => x.Quantity);

                if (ModelState.IsValid && quantityStock >= stockExitView.RequisitionQuantity)
                {
                    var stockEditedId = _stockRepository.WhereAsync(x => x.ProductId == stockExitView.ProductId).Result.FirstOrDefault().Id;

                    Stock stock = new();
                    {
                        stock.Id = stockEditedId;
                        stock.Quantity = quantityStock - stockExitView.RequisitionQuantity;
                        stock.ProductId = stockExitView.ProductId;
                    }

                    await _stockRepository.EditAsync(stock);

                    StockExit stockExitModel = _mapper.Map<StockExit>(stockExitView);
                    await _stockExitRepository.CreateAsync(stockExitModel);
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
                StockExit stockExitDelete = await _stockExitRepository.FindIdNoTrackingAsync(id);


                if (stockExitDelete != null)
                {
                    await _stockExitRepository.DeleteAsync(stockExitDelete);
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


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Infra.Repository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductRepository productRepository, ISupplierRepository supplierRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            try
            {

                List<Product> productListModel = _productRepository.FindAllAsync().Result.ToList();
                List<ProductDisplayViewModel> productListView = _mapper.Map<List<ProductDisplayViewModel>>(productListModel);

                return View(productListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar produtos", ex);
            }

        }

        public IActionResult CreateView()
        {
            ViewBag.supplier = _supplierRepository.FindAllAsync().Result.ToList();
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {

            try
            {
                ViewBag.supplier = _supplierRepository.FindAllAsync().Result.ToList();

                Product productModel = await _productRepository.FindIdNoTrackingAsync(id);
                ProductViewModel productView = _mapper.Map<ProductViewModel>(productModel);

                if (productView == null)
                {
                    NotFound();
                }

                return View(productView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar produto", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                Product productModel = await _productRepository.FindIdNoTrackingAsync(id);
                ProductViewModel productView = _mapper.Map<ProductViewModel>(productModel);

                if (productView == null)
                {
                    NotFound();
                }

                ViewBag.productName = productView.Name;
                return View(productView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar produto", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productView)
        {
            try
            {
                Product productModel = _mapper.Map<Product>(productView);

                if (ModelState.IsValid)
                {
                    await _productRepository.EditAsync(productModel);
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
        public async Task<IActionResult> Create(ProductViewModel productView)
        {
            try
            {
                Product productModel = _mapper.Map<Product>(productView);

                if (ModelState.IsValid)
                {
                    await _productRepository.CreateAsync(productModel);
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
                Product productDelete = await _productRepository.FindIdNoTrackingAsync(id);


                if (productDelete != null)
                {
                    await _productRepository.DeleteAsync(productDelete);
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

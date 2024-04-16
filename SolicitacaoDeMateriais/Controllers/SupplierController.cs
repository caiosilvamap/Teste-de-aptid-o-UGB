using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierController(IMapper mapper, ISupplierRepository supplierRepository)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<Supplier> supplierListModel = _supplierRepository.FindAllAsync().Result.ToList();
                List<SupplierViewModel> supplierListView = _mapper.Map<List<SupplierViewModel>>(supplierListModel);

                return View(supplierListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar fornecedores", ex);
            }

        }

        public IActionResult CreateView()
        {
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {

            try
            {
                Supplier supplierModel = await _supplierRepository.FindIdNoTrackingAsync(id);
                SupplierViewModel supplierView = _mapper.Map<SupplierViewModel>(supplierModel);

                if (supplierView == null)
                {
                    NotFound();
                }

                return View(supplierView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar fornecedor", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                Supplier supplierModel = await _supplierRepository.FindIdNoTrackingAsync(id);
                SupplierViewModel supplierView = _mapper.Map<SupplierViewModel>(supplierModel);

                if (supplierView == null)
                {
                    NotFound();
                }

                ViewBag.supplierName = supplierView.Name;
                return View(supplierView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar fornecedor", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierViewModel supplierView)
        {
            try
            {
                Supplier supplierModel = _mapper.Map<Supplier>(supplierView);

                if (ModelState.IsValid)
                {
                    await _supplierRepository.EditAsync(supplierModel);
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
        public async Task<IActionResult> Create(SupplierViewModel supplierView)
        {
            try
            {
                Supplier supplierModel = _mapper.Map<Supplier>(supplierView);

                if (ModelState.IsValid)
                {
                    await _supplierRepository.CreateAsync(supplierModel);
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
                Supplier supplierDelete = await _supplierRepository.FindIdNoTrackingAsync(id);


                if (supplierDelete != null)
                {
                    await _supplierRepository.DeleteAsync(supplierDelete);
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

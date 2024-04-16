using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Infra.Repository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public ServiceController(IServiceRepository serviceRepository, ISupplierRepository supplierRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Service> serviceListModel = _serviceRepository.FindAllAsync().Result.ToList();
                List<ServiceDisplayViewModel> serviceListView = _mapper.Map<List<ServiceDisplayViewModel>>(serviceListModel);
                return View(serviceListView);
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

                Service serviceModel = await _serviceRepository.FindIdNoTrackingAsync(id);
                ServiceViewModel serviceView = _mapper.Map<ServiceViewModel>(serviceModel);

                if (serviceView == null)
                {
                    NotFound();
                }

                return View(serviceView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar serviço", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                Service serviceModel = await _serviceRepository.FindIdNoTrackingAsync(id);
                ServiceViewModel serviceView = _mapper.Map<ServiceViewModel>(serviceModel);

                if (serviceView == null)
                {
                    NotFound();
                }

                ViewBag.serviceName = serviceView.Name;
                return View(serviceView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar serviço", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceViewModel serviceView)
        {
            try
            {
                Service serviceModel = _mapper.Map<Service>(serviceView);

                if (ModelState.IsValid)
                {
                    await _serviceRepository.EditAsync(serviceModel);
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
        public async Task<IActionResult> Create(ServiceViewModel serviceView)
        {
            try
            {
                Service serviceModel = _mapper.Map<Service>(serviceView);

                if (ModelState.IsValid)
                {
                    await _serviceRepository.CreateAsync(serviceModel);
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
                Service serviceDelete = await _serviceRepository.FindIdNoTrackingAsync(id);


                if (serviceDelete != null)
                {
                    await _serviceRepository.DeleteAsync(serviceDelete);
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

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class RequestProductServiceController : Controller
    {

        private readonly IServiceRepository _serviceRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRequestProductServiceRepository _requestProductServiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;


        public RequestProductServiceController(IServiceRepository serviceRepository,
            ISupplierRepository supplierRepository,
            IMapper mapper,
            IRequestProductServiceRepository requestProductServiceRepository,
            IProductRepository productRepository,
            IDepartmentRepository departmentRepository,
            IUserRepository userRepository)
        {
            _serviceRepository = serviceRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _requestProductServiceRepository = requestProductServiceRepository;
            _productRepository = productRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<RequestProductService> requestProdServListModel = _requestProductServiceRepository.FindAllAsync().Result.ToList();
                List<RequestProductServiceDisplayViewModel> requestProdServListView = _mapper.Map<List<RequestProductServiceDisplayViewModel>>(requestProdServListModel);
                return View(requestProdServListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar solicitações", ex);
            }
        }

        public IActionResult CreateRequestServiceView()
        {
            ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();
            ViewBag.user = _userRepository.FindAllAsync().Result.ToList();
            ViewBag.service = _serviceRepository.FindAllAsync().Result.ToList();
            return View();
        }

        public IActionResult CreateRequestProductView()
        {
            ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();
            ViewBag.user = _userRepository.FindAllAsync().Result.ToList();
            ViewBag.product = _productRepository.FindAllAsync().Result.ToList();
            return View();
        }

        public async Task<IActionResult> EditRequestServiceView(int id)
        {
            try
            {
                ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();
                ViewBag.user = _userRepository.FindAllAsync().Result.ToList();
                ViewBag.service = _serviceRepository.FindAllAsync().Result.ToList();

                RequestProductService requestProdServListModel = await _requestProductServiceRepository.FindIdNoTrackingAsync(id);
                RequestProductServiceViewModel requestProdServListView = _mapper.Map<RequestProductServiceViewModel>(requestProdServListModel);

                if (requestProdServListView == null)
                {
                    NotFound();
                }

                return View(requestProdServListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar solicitação", ex);
            }

        }

        public async Task<IActionResult> EditRequestProductView(int id)
        {
            try
            {
                ViewBag.department = _departmentRepository.FindAllAsync().Result.ToList();
                ViewBag.user = _userRepository.FindAllAsync().Result.ToList();
                ViewBag.product = _productRepository.FindAllAsync().Result.ToList();


                RequestProductService requestProdServListModel = await _requestProductServiceRepository.FindIdNoTrackingAsync(id);
                RequestProductServiceViewModel requestProdServListView = _mapper.Map<RequestProductServiceViewModel>(requestProdServListModel);

                if (requestProdServListView == null)
                {
                    NotFound();
                }

                return View(requestProdServListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar solicitação", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                RequestProductService requestProdServListModel = await _requestProductServiceRepository.FindIdNoTrackingAsync(id);
                RequestProductServiceViewModel requestProdServListView = _mapper.Map<RequestProductServiceViewModel>(requestProdServListModel);

                if (requestProdServListView == null)
                {
                    NotFound();
                }

                ViewBag.requestCode = requestProdServListView.RequestCode;
                return View(requestProdServListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar solicitação", ex);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(RequestProductServiceViewModel requestProdServListView)
        {
            try
            {
                RequestProductService requestProdServListModel = _mapper.Map<RequestProductService>(requestProdServListView);
                var requestProdServDB = _requestProductServiceRepository.FindIdNoTrackingAsync(requestProdServListModel.Id).Result;
                requestProdServListModel.CreationDate = requestProdServDB.CreationDate;
                requestProdServListModel.RequestCode = requestProdServDB.RequestCode;

                if (ModelState.IsValid)
                {
                    await _requestProductServiceRepository.EditAsync(requestProdServListModel);
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
        public async Task<IActionResult> Create(RequestProductServiceViewModel requestProdServListView)
        {
            try
            {

                RequestProductService requestProdServListModel = _mapper.Map<RequestProductService>(requestProdServListView);

                requestProdServListModel.CreationDate = DateTime.UtcNow.AddHours(-3);

                var departmentName = _departmentRepository.FindIdNoTrackingAsync(requestProdServListView.DepartmentId).Result.Name;

                var countRequestProdServ = _requestProductServiceRepository.FindAllAsync().Result.Count() + 1;

                var year = requestProdServListModel.CreationDate.Year;

                if (requestProdServListView.ProductId != null)
                {
                    requestProdServListModel.RequestCode = $"PROD.{departmentName.Substring(0, Math.Min(3, departmentName.Length)).ToUpper()}.{year}.{countRequestProdServ}";
                }
                else
                {
                    requestProdServListModel.RequestCode = $"SERV.{departmentName.Substring(0, Math.Min(3, departmentName.Length)).ToUpper()}.{year}.{countRequestProdServ}";
                }



                if (ModelState.IsValid)
                {
                    await _requestProductServiceRepository.CreateAsync(requestProdServListModel);
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
                RequestProductService requestProdServListDelete = await _requestProductServiceRepository.FindIdNoTrackingAsync(id);


                if (requestProdServListDelete != null)
                {
                    await _requestProductServiceRepository.DeleteAsync(requestProdServListDelete);
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


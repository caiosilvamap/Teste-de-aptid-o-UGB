using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController( IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<Department> departmentListModel = _departmentRepository.FindAllAsync().Result.ToList();
                List<DepartmentViewModel> departmentListView = _mapper.Map<List<DepartmentViewModel>>(departmentListModel);

                return View(departmentListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar departamentos", ex);
            }

        }

        public IActionResult CreateView()
        {
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {
            ViewBag.department = _departmentRepository.WhereAsync(x => x.Active).Result.ToList();

            try
            {
                Department departmentModel = await _departmentRepository.FindIdNoTrackingAsync(id);
                DepartmentViewModel departmentView = _mapper.Map<DepartmentViewModel>(departmentModel);

                if (departmentView == null)
                {
                    NotFound();
                }

                return View(departmentView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar departamento", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                Department departmentModel = await _departmentRepository.FindIdNoTrackingAsync(id);
                DepartmentViewModel departmentView = _mapper.Map<DepartmentViewModel>(departmentModel);

                if (departmentView == null)
                {
                    NotFound();
                }
                ViewBag.DepartmentName = departmentView.Name;
                return View(departmentView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar departamento", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentView)
        {
            try
            {
                Department departmentModel = _mapper.Map<Department>(departmentView);

                if (ModelState.IsValid)
                {
                    await _departmentRepository.EditAsync(departmentModel);
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
        public async Task<IActionResult> Create(DepartmentViewModel departmentView)
        {
            try
            {
                Department departmentModel = _mapper.Map<Department>(departmentView);

                if (ModelState.IsValid)
                {
                    await _departmentRepository.CreateAsync(departmentModel);
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao adicionar departamento: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Department departmentDelete = await _departmentRepository.FindIdNoTrackingAsync(id);


                if (departmentDelete != null)
                {
                    await _departmentRepository.DeleteAsync(departmentDelete);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao Excluir departamento: {ex.Message}");
            }
        }
    }
}

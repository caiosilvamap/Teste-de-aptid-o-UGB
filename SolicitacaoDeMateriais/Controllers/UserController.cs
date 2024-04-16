using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolicitacaoDeMateriais.Infra.InterfacesRepository;
using SolicitacaoDeMateriais.Models;
using SolicitacaoDeMateriais.ViewModels;

namespace SolicitacaoDeMateriais.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            try
            {
                List<User> userListModel = _userRepository.FindAllAsync().Result.ToList();
                List<UserDisplayViewModel> userListView = _mapper.Map<List<UserDisplayViewModel>>(userListModel);

                return View(userListView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar usuários", ex);
            }

        }

        public IActionResult CreateView()
        {
            ViewBag.department = _departmentRepository.WhereAsync(x => x.Active).Result.ToList();
            return View();
        }

        public async Task<IActionResult> EditView(int id)
        {
            

            try
            {
                ViewBag.department = _departmentRepository.WhereAsync(x => x.Active).Result.ToList();
                User userModel = await _userRepository.FindIdNoTrackingAsync(id);
                UserViewModel userView = _mapper.Map<UserViewModel>(userModel);

                if (userView == null)
                {
                    NotFound();
                }

                return View(userView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar usuário", ex);
            }

        }

        public async Task<IActionResult> DeleteView(int id)
        {
            try
            {
                User userModel = await _userRepository.FindIdNoTrackingAsync(id);
                UserViewModel userView = _mapper.Map<UserViewModel>(userModel);

                if (userView == null)
                {
                    NotFound();
                }
                ViewBag.UserName = userView.Name;
                return View(userView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ApplicationException("Erro ao buscar usuário", ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userView)
        {
            try
            {
                User userModel = _mapper.Map<User>(userView);

                if (ModelState.IsValid)
                {
                    await _userRepository.EditAsync(userModel);
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
        public async Task<IActionResult> Create(UserViewModel userView)
        {
            try
            {
                User userModel = _mapper.Map<User>(userView);

                if (ModelState.IsValid)
                {
                    await _userRepository.CreateAsync(userModel);
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao adicionar usuário: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                User userDelete = await _userRepository.FindIdNoTrackingAsync(id);


                if (userDelete != null)
                {
                    await _userRepository.DeleteAsync(userDelete);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Erro ao Excluir usuário: {ex.Message}");
            }
        }
    }
}

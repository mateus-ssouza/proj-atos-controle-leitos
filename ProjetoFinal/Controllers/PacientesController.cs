using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using ProjetoFinal.Services.Exceptions;

namespace ProjetoFinal.Controllers
{
    public class PacientesController : Controller
    {
        private readonly PacienteService _pacienteService;
        private readonly EnderecoService _enderecoService;

        public PacientesController(PacienteService pacienteService, EnderecoService enderecoServico)
        {
            _pacienteService = pacienteService;
            _enderecoService = enderecoServico;
        }

        public IActionResult Index()
        {
            var list = _pacienteService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var viewModel = new PacienteViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PacienteViewModel pacienteViewModel)
        {
            var paciente = pacienteViewModel.Paciente;

            var endereco = pacienteViewModel.Endereco;
            endereco.Paciente = pacienteViewModel.Paciente;

            _pacienteService.Insert(paciente);
            _enderecoService.Insert(endereco);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _pacienteService.FindById(id.Value);
            
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _pacienteService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _pacienteService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _pacienteService.FindById(id.Value);
            
            if (obj == null)
            {
                return NotFound();
            }

            PacienteViewModel viewModel = new PacienteViewModel
            {
                Paciente = obj,
                Endereco = obj.Endereco
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Paciente obj)
        {
            if (id != obj.Id)
            {
                return BadRequest();
            }

            try
            {
                var paciente = _pacienteService.FindById(id);

                if (obj == null)
                {
                    return NotFound();
                }

                paciente.Endereco = obj.Endereco;

                _pacienteService.Update(paciente);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}

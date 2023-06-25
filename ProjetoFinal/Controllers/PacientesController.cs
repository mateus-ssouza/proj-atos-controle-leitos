using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using ProjetoFinal.Services.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid)
            {
                return View(pacienteViewModel);
            }

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
                return RedirectToAction(nameof(Error), 
                    new {message = "Id não fornecido"});
            }

            var obj = _pacienteService.FindById(id.Value);
            
            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não encontrado" });
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
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = _pacienteService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = _pacienteService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                     new { message = "Id não encontrado" });
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
        public IActionResult Edit(int id, PacienteViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (id != viewModel.Paciente.Id)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem" });
            }

            try
            {
                var obj = _pacienteService.FindById(id);

                _pacienteService.UpdateData(obj, viewModel);
                _pacienteService.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error),
                    new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}

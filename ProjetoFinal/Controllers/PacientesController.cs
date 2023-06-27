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

        public async Task<IActionResult> Index()
        {
            var list = await _pacienteService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            var viewModel = new PacienteViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            var paciente = pacienteViewModel.Paciente;

            var endereco = pacienteViewModel.Endereco;
            endereco.Paciente = pacienteViewModel.Paciente;

            await _pacienteService.InsertAsync(paciente);
            await _enderecoService.InsertAsync(endereco);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), 
                    new {message = "Id não fornecido"});
            }

            var obj = await _pacienteService.FindByIdAsync(id.Value);
            
            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _pacienteService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _pacienteService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _pacienteService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                     new { message = "Id não encontrado" });
            }

            var viewModel = new PacienteViewModel
            {
                Paciente = obj,
                Endereco = obj.Endereco
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PacienteViewModel viewModel)
        {

            if (id != viewModel.Paciente.Id)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem" });
            }

            try
            {
                var obj = await _pacienteService.FindByIdAsync(id);
                _pacienteService.UpdateData(obj, viewModel);
                await _pacienteService.UpdateAsync(obj);
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

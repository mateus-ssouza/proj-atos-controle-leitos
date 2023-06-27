using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using System.Diagnostics;

namespace ProjetoFinal.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly SolicitacaoService _solicitacaoService;
        private readonly PacienteService _pacienteService;

        public SolicitacoesController(SolicitacaoService solicitacaoService, PacienteService pacienteService)
        {
            _solicitacaoService = solicitacaoService;
            _pacienteService = pacienteService;
        }

        public async Task<IActionResult> Index()
        {
            var solicitacoes = await _solicitacaoService.FindAllAsync();
            return View(solicitacoes);
        }

        public async Task<IActionResult> Create()
        {
            var pacientes = await _pacienteService.FindPacientesSemSolicitacao();
            var viewModel = new SolicitacaoViewModel
            {
                Pacientes = pacientes
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Solicitacao solicitacao)
        {
            await _solicitacaoService.InsertAsync(solicitacao);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _solicitacaoService.FindByIdAsync(id.Value);

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
            await _solicitacaoService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _solicitacaoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não encontrado" });
            }

            return View(obj);
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

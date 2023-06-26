using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;

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
    }
}

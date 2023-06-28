using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using System.Diagnostics;

namespace ProjetoFinal.Controllers
{
    public class LeitosController : Controller
    {
        private readonly LeitoService _leitoService;
        private readonly SolicitacaoService _solicitacaoService;

        public LeitosController(LeitoService leitoService, SolicitacaoService solicitacaoService)
        {
            _leitoService = leitoService;
            _solicitacaoService = solicitacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var leitos = await _leitoService.FindAllAsync();
            return View(leitos);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leito leito)
        {
            await _leitoService.InsertAsync(leito);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _leitoService.FindByIdAsync(id.Value);

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
            await _leitoService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var obj = await _leitoService.FindByIdAsync(id.Value);

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

            var obj = await _leitoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error),
                     new { message = "Id não encontrado" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Leito leito)
        {

            if (id != leito.Id)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem" });
            }

            try
            {
                await _leitoService.UpdateAsync(leito);
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

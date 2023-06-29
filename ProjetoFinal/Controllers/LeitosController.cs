using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Enums;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using ProjetoFinal.Services.Exceptions;
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
            try
            {
                await _leitoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Possui uma solicitação vinculada, operação não é permitida." });
            }
            
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
                var obj = await _leitoService.FindByIdAsync(id);
                _leitoService.UpdateData(obj, leito);
                await _leitoService.UpdateAsync(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error),
                    new { message = e.Message });
            }
        }

        public async Task<IActionResult> Relocate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Id não fornecido" });
            }

            var leito = await _leitoService.FindByIdAsync(id.Value);

            if (leito == null)
            {
                return RedirectToAction(nameof(Error),
                     new { message = "Id não encontrado" });
            }

            var solicitacao = await _solicitacaoService.FindByIdAsync(leito.Solicitacao.Id);

            if (solicitacao == null)
            {
                return RedirectToAction(nameof(Error),
                     new { message = "Id não encontrado" });
            }

            List<Leito> leitos;

            if (solicitacao.TipoLeito == TipoLeito.CLINICO)
            {
                leitos = await _leitoService.FindLeitosClinicosDisponiveis();
            }
            else
            {
                leitos = await _leitoService.FindLeitosCirurgicosDisponiveis();
            }

            var viewModel = new SolicitacaoViewModel
            {
                Solicitacao = solicitacao,
                Leitos = leitos
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Relocate(int id, SolicitacaoViewModel viewModel)
        {
            /*if (id != viewModel.Solicitacao.IdLeito)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem " + id + " " + viewModel.Solicitacao.IdLeito });
            }*/

            try
            {
                var solicitacao = await _solicitacaoService.FindByIdLeitoAsync(id);

                _solicitacaoService.TransferirSolicitacao(solicitacao, viewModel);

                var novoLeitoDaSolicitacao = await _leitoService.FindByIdAsync((int)solicitacao.IdLeito);
                var antigoLeitoDaSolicitacao = await _leitoService.FindByIdAsync(id);

                _leitoService.MudarStatusLeito(novoLeitoDaSolicitacao);
                _leitoService.MudarStatusLeito(antigoLeitoDaSolicitacao);

                await _solicitacaoService.UpdateAsync(solicitacao);
                await _leitoService.UpdateAsync(novoLeitoDaSolicitacao);
                await _leitoService.UpdateAsync(antigoLeitoDaSolicitacao);

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

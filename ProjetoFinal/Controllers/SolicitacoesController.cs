using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Enums;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using System.Diagnostics;

namespace ProjetoFinal.Controllers
{
    public class SolicitacoesController : Controller
    {
        private readonly SolicitacaoService _solicitacaoService;
        private readonly PacienteService _pacienteService;
        private readonly LeitoService _leitoService;

        public SolicitacoesController(SolicitacaoService solicitacaoService, PacienteService pacienteService, LeitoService leitoService)
        {
            _solicitacaoService = solicitacaoService;
            _pacienteService = pacienteService;
            _leitoService = leitoService;
        }

        public async Task<IActionResult> Index()
        {
            var solicitacoes = await _solicitacaoService.FindAllAsync();
            return View(solicitacoes);
        }

        public async Task<IActionResult> Create()
        {
            var pacientes = await _pacienteService.FindPacientesSemSolicitacaoAtiva();
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

            TempData["AlertMessage"] = "Solicitação criada com sucesso!";
            TempData["AlertType"] = "success";

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
            var obj = await _solicitacaoService.FindByIdAsync(id);

            if (obj.Status == SolicitacaoStatus.REGULADO)
            {
                var leito = await _leitoService.FindByIdAsync((int)obj.IdLeito);
                leito.Status = StatusLeito.LIVRE;
                await _leitoService.UpdateAsync(leito);
            }

            await _solicitacaoService.RemoveAsync(id);

            TempData["AlertMessage"] = "Solicitação removida com sucesso!";
            TempData["AlertType"] = "success";

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

        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, Solicitacao solicitacao)
        {

            if (id != solicitacao.Id)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem" });
            }

            try
            {
                var obj = await _solicitacaoService.FindByIdAsync(id);
                _solicitacaoService.UpdateData(obj, solicitacao);
                await _solicitacaoService.UpdateAsync(obj);

                TempData["AlertMessage"] = "Dados da solicitação atulizado com sucesso!";
                TempData["AlertType"] = "success";

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error),
                    new { message = e.Message });
            }
        }

        public async Task<IActionResult> Reserve(int? id)
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

            List<Leito> leitos;

            if (obj.TipoLeito == TipoLeito.CLINICO)
            {
                leitos = await _leitoService.FindLeitosClinicosDisponiveis();
            }
            else
            {
                leitos = await _leitoService.FindLeitosCirurgicosDisponiveis();
            }

            var viewModel = new SolicitacaoViewModel
            {
                Solicitacao = obj,
                Leitos = leitos
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int id, SolicitacaoViewModel viewModel)
        {
            if (id != viewModel.Solicitacao.Id)
            {
                return RedirectToAction(nameof(Error),
                    new { message = "Ids não correspondem" });
            }

            try
            {
                var obj = await _solicitacaoService.FindByIdAsync(id);
                _solicitacaoService.RegularSolicitacao(obj, viewModel);

                int idLeito = (int)obj.IdLeito;
                var leito = await _leitoService.FindByIdAsync(idLeito);

                _leitoService.MudarStatusLeito(leito);

                await _solicitacaoService.UpdateAsync(obj);
                await _leitoService.UpdateAsync(leito);

                TempData["AlertMessage"] = "Reserva no leito efetuada com sucesso!";
                TempData["AlertType"] = "success";

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

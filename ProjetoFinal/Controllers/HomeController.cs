using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services;
using System.Diagnostics;
using ProjetoFinal.Models.Enums;

namespace ProjetoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LeitoService _leitoService;

        public HomeController(ILogger<HomeController> logger, LeitoService leitoService)
        {
            _logger = logger;
            _leitoService = leitoService;
        }

        public async Task<IActionResult> Index()
        {      
            // Lista de leitos cadastrados
            List<Leito> leitos = await _leitoService.FindAllAsync();

            // Contagem e operações
            int totalLeitos = leitos.Count;
            int leitosOcupados = leitos.Count(l => l.Status == StatusLeito.OCUPADO);
            double porcentagemOcupacao = (double)leitosOcupados / totalLeitos * 100;

            // Views para serem exibidas na tela
            ViewBag.TotalLeitos = totalLeitos;
            ViewBag.LeitosOcupados = leitosOcupados;
            ViewBag.PorcentagemOcupacao = porcentagemOcupacao;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
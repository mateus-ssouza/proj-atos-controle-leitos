using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Services;

namespace ProjetoFinal.Controllers
{
    public class LeitosController : Controller
    {
        private readonly LeitoService _leitoService;

        public LeitosController(LeitoService leitoService)
        {
            _leitoService = leitoService;
        }

        public async Task<IActionResult> Index()
        {
            var leitos = await _leitoService.FindAllAsync();
            return View(leitos);
        }
    }
}

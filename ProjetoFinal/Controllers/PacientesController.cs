using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Services;

namespace ProjetoFinal.Controllers
{
    public class PacientesController : Controller
    {
        private readonly PacienteService _pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public IActionResult Index()
        {
            var list = _pacienteService.FindAll();
            return View(list);
        }
    }
}

using GerenciamentoFrotaVeiculo.WebUI.Models;
using GerenciamentoFrotaVeiculo.WebUI.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GerenciamentoFrotaVeiculo.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IColaboradorService _colaboradorService;

        public HomeController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService ?? throw new ArgumentNullException(nameof(colaboradorService));
        }

        public IActionResult Index()
        {
            var colaborador = _colaboradorService.GetAllAsync();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

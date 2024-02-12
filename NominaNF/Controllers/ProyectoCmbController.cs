using Microsoft.AspNetCore.Mvc;
using NominaNF.Data.Contrato;
using NominaNF.Data.Implementacion;
using NominaNF.Models;

namespace NominaNF.Controllers
{
    public class ProyectoCmbController : Controller
    {
        private readonly IProyectoCmbData<Proyecto> _proyectoCmbData;

       
        public ProyectoCmbController(IProyectoCmbData<Proyecto> proyectoCmbData)
        {
            _proyectoCmbData = proyectoCmbData;

        }

        [HttpGet]
        public async Task<IEnumerable<Proyecto>> ObtieneProyectosCmb()
        {
            var proyectos = await _proyectoCmbData.ObtieneProyectosCmb();
            return proyectos;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NominaNF.Data.Contrato;
using NominaNF.Data.Implementacion;
using NominaNF.Models;

namespace NominaNF.Controllers
{
    public class UbicacionCmbController : Controller
    {
        private readonly IUbicacionCmbData<Ubicacion> _ubicacionCmbData;

       
        public UbicacionCmbController(IUbicacionCmbData<Ubicacion> ubicacionCmbData)
        {
            _ubicacionCmbData = ubicacionCmbData;

        }

        [HttpGet]
        public async Task<IEnumerable<Ubicacion>> ObtieneUbicacionesCmb()
        {
            var ubicaciones = await _ubicacionCmbData.ObtieneUbicacionesCmb();
            return ubicaciones;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

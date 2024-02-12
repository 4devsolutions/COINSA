using Microsoft.AspNetCore.Mvc;
using NominaNF.Data.Contrato;
using NominaNF.Data.Implementacion;
using NominaNF.Models;

namespace NominaNF.Controllers
{
    public class PuestoCmbController : Controller
    {
        private readonly IPuestoCmbData<Puesto> _puestoCmbData;

       
        public PuestoCmbController(IPuestoCmbData<Puesto> puestoCmbData)
        {
            _puestoCmbData = puestoCmbData;

        }

        [HttpGet]
        public async Task<IEnumerable<Puesto>> ObtienePuestosCmb()
        {
            var puestos = await _puestoCmbData.ObtienePuestosCmb();
            return puestos;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

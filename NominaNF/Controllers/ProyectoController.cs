using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using NominaNF.Controllers;
using NominaNF.Repositorios.Contrato;
using NominaNF.Models;
using System.Reflection;

namespace NominaNF.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly IGenericRepository<Proyecto> _proyectoRepository;


        // GET: /HelloWorld/Welcome/ 
        //public string Welcome(int id)
        //{
        //    return "This is the Welcome action method...${id}" + id.ToString();
        //}
    
        
        public ProyectoController(IGenericRepository<Proyecto> proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ListaProyectos()
        {
            List<Proyecto> _lista = await _proyectoRepository.Consultar();
            return StatusCode(StatusCodes.Status200OK, _lista);


        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody]Proyecto modelo)
        {
            bool _resultado = await _proyectoRepository.Agregar(modelo);
            
            if(_resultado)
               return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
               return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Proyecto modelo)
        {
            bool _resultado = await _proyectoRepository.Modificar(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int IdProyecto)
        {
            bool _resultado = await _proyectoRepository.Eliminar(IdProyecto);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        
        
    }
}

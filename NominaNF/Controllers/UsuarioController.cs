using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using NominaNF.Controllers;
using NominaNF.Models;
using System.Reflection;
using NominaNF.Data.Contrato;
using NominaNF.ViewModels;

namespace NominaNF.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IGenericData<Usuario> _UsuarioData;
       
        
        public UsuarioController(IGenericData<Usuario> UsuarioData)
        {
            _UsuarioData = UsuarioData;
           
            
        }

        public async Task<IActionResult> Index()
        {
         
            return View();

        }


        [HttpGet]
        public async Task<IActionResult> ObtieneUsuarios()
        {
            List<Usuario> _lista = await _UsuarioData.Obtiene();
            return StatusCode(StatusCodes.Status200OK, _lista);


        }

        [HttpPost]
        public async Task<IActionResult> AgregaUsuario([FromBody]Usuario modelo)
        {
            bool _resultado = await _UsuarioData.Agrega(modelo);
            
            if(_resultado)
               return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
               return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditaUsuario([FromBody] Usuario modelo)
        {
            bool _resultado = await _UsuarioData.Edita(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminaUsuario([FromBody] Usuario modelo)
        {
            bool _resultado = await _UsuarioData.Elimina(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        
        
    }
}

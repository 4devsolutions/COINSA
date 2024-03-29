﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using NominaNF.Controllers;
using NominaNF.Models;
using System.Reflection;
using NominaNF.Data.Contrato;

namespace NominaNF.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly IGenericData<Proyecto> _proyectoRepository;

        
        public ProyectoController(IGenericData<Proyecto> proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ObtieneProyectos()
        {
            List<Proyecto> _lista = await _proyectoRepository.Obtiene();
            return StatusCode(StatusCodes.Status200OK, _lista);

        }

        [HttpPost]
        public async Task<IActionResult> AgregaProyecto([FromBody]Proyecto modelo)
        {
            bool _resultado = await _proyectoRepository.Agrega(modelo);
            
            if(_resultado)
               return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
               return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> EditaProyecto([FromBody] Proyecto modelo)
        {
            bool _resultado = await _proyectoRepository.Edita(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> EliminaProyecto([FromBody] Proyecto modelo)
        {
            bool _resultado = await _proyectoRepository.Elimina(modelo);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        
        
    }
}

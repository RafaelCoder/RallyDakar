using Microsoft.AspNetCore.Mvc;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.Controllers {
    [ApiController]
    [Route("api/pilotos")]
    public class PilotoController : ControllerBase {
        IPilotoRepositorio _pilotoRepositorio;

        public PilotoController(IPilotoRepositorio pilotoRepositorio) {
            _pilotoRepositorio = pilotoRepositorio;
        }

        [HttpGet]
        public IActionResult ObterTodos() {
            try {
                var pilotos = _pilotoRepositorio.ObterTodos();
                if (!pilotos.Any())
                    return NotFound();
                return Ok(pilotos);
            } catch (Exception ex) {
                //return BadRequest(ex.ToString());
                //_logger.Info(ex.toString());
                //return BadRequest("Mensagem generica");
                return StatusCode(500, "Houve um erro interno bla bla bla");

            }
        }

        [HttpGet("{id}", Name = "Obter")]
        public IActionResult Obter(int id) {
            try {
                var piloto = _pilotoRepositorio.Obter(id);
                if (piloto == null)
                    return NotFound();
                return Ok(piloto);
            } catch (Exception ex) {
                return StatusCode(500, "Houve um erro interno bla bla bla");
            }
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody] Piloto piloto) {
            try {
                if (_pilotoRepositorio.Existe(piloto.Id))
                    return StatusCode(409, "Já existe piloto com a mesma identificacção");

                _pilotoRepositorio.Adicionar(piloto);

                return CreatedAtRoute("Obter", new { id = piloto.Id }, piloto);
            } catch (Exception ex) {
                return StatusCode(500, "Houve um erro interno bla bla bla");
            }
        }

        [HttpPut]
        public IActionResult AtualizarPiloto([FromBody] Piloto piloto) {
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmentePiloto(int id) {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarPiloto(int id) {
            return Ok();
        }
    }
}

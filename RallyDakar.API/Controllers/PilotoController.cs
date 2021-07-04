using Microsoft.AspNetCore.JsonPatch;
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
        public IActionResult Atualizar([FromBody] Piloto piloto) {
            try {
                if (!_pilotoRepositorio.Existe(piloto.Id))
                    return NotFound();

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            } catch (Exception ex) {
                return StatusCode(500, "Houve um erro interno bla bla bla");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<Piloto> patchPiloto) {
            try {
                if (!_pilotoRepositorio.Existe(id))
                    return NotFound();

                var piloto = _pilotoRepositorio.Obter(id);

                patchPiloto.ApplyTo(piloto);

                _pilotoRepositorio.Atualizar(piloto);

                return NoContent();
            } catch (Exception ex) {
                return StatusCode(500, "Houve um erro interno bla bla bla");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id) {
            try {
                var piloto = _pilotoRepositorio.Obter(id);

                if (piloto==null)
                    return NotFound();
                
                _pilotoRepositorio.Deletar(piloto);

                return NoContent();
            } catch (Exception ex) {
                return StatusCode(500, "Houve um erro interno bla bla bla");
            }
        }
    }
}

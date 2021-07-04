﻿using Microsoft.AspNetCore.Mvc;
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
            return Ok(_pilotoRepositorio.ObterTodos());
        }

        [HttpPost]
        public IActionResult AdicionarPiloto([FromBody] Piloto piloto) {
            _pilotoRepositorio.Adicionar(piloto);
            return Ok();
        }
    }
}
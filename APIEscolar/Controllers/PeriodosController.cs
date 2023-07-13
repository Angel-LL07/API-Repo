﻿using API.Dominio;
using API.Persistencia;
using APIEscolar.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEscolar.Controllers
{
    [Route("api/periodos")]
    [ApiController]
    public class PeriodosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PeriodosController( IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ObtenerPeriodos()
        {
            var lista = await _unitOfWork.PeriodoEscolarRepository.ObtenerTodosAsync();
            var periodos = new List<PeriodoEscolarVM>();

            foreach (var newlist in lista)
            {
                periodos.Add(_mapper.Map<PeriodoEscolarVM>(newlist));
            }
            return Ok(periodos);
        }
        [HttpGet("{PeriodoId:int}", Name = "ObtenerPorId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ObtenerPorId(int PeriodoId)
        {
            var usuario = await _unitOfWork.PeriodoEscolarRepository.ObtenerPorIdAsync(PeriodoId);
            if (usuario == null)
            {
                return NotFound();
            }
            var muestra = _mapper.Map<PeriodoEscolarVM>(usuario);
            return Ok(muestra);

        }

        [HttpPost("AgregarPeriodo")]
        [ProducesResponseType(201,Type = typeof(PeriodoEscolarVM))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarPeriodo([FromBody] PeriodosCreacionVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(" ", "Todos los campos son necesarios");
                 return BadRequest(ModelState);
            }
            var periodo = await _unitOfWork.PeriodoEscolarRepository.ObtenerAsync(match: x => x.Descripcion == model.Descripcion || x.Abreviacion == model.Abreviacion);
            if (periodo !=null) {
                ModelState.AddModelError(" ", $"El periodo {model.Descripcion} ya existe");
                return BadRequest(ModelState);
            }
            var registro = _mapper.Map<PeriodoEscolar>(model);
            try
            {
               
                await _unitOfWork.PeriodoEscolarRepository.AgregarAsin(registro);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(" ", "Hubo un error");
                return BadRequest(ModelState);
            }


            return CreatedAtRoute("ObtenerPorId", new { PeriodoId = registro.Id }, registro);

        }

        [HttpPatch("{PeriodoId:int}",Name ="ActualizarPeriodo")]
        [ProducesResponseType(200, Type =typeof(PeriodoEscolarVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ActualizarPeriodo(int PeriodoId, [FromBody] PeriodoEscolarVM model)
        {
            var periodo = await _unitOfWork.PeriodoEscolarRepository.ObtenerPorIdAsync(PeriodoId);
            if(periodo == null)
            {
                ModelState.AddModelError(" ", "Periodo no encontrado");
                return BadRequest(ModelState);
            }
           var actualizado = _mapper.Map<PeriodoEscolar>(periodo);
            try
            {
                await _unitOfWork.PeriodoEscolarRepository.ActualizarAsync(actualizado, actualizado.Id);
                await _unitOfWork.SaveAsync();
            }
            catch
            {
                ModelState.AddModelError(" ", "Ocurrio un error");
                return BadRequest(ModelState);
            }


            return NoContent();
        }
    }
}

using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Empleado;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly ILogger<EmpleadosController> _logger;
        private readonly IApplication<Empleado> _empleado;
        private readonly IMapper _mapper;

        public EmpleadosController(ILogger<EmpleadosController> logger, IApplication<Empleado> empleado, IMapper mapper)
        {
            _logger = logger;
            _empleado = empleado;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<EmpleadoResponseDto>>(_empleado.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var empleado = _empleado.GetById(id.Value);
            if (empleado is null)
                return NotFound();
            return Ok(_mapper.Map<EmpleadoResponseDto>(empleado));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EmpleadoRequestDto empleadoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var empleado = _mapper.Map<Empleado>(empleadoRequestDto);
            _empleado.Save(empleado);
            return Ok(empleado.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, EmpleadoRequestDto empleadoRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
                return BadRequest();
            var empleadoBack = _empleado.GetById(id.Value);
            if (empleadoBack is null)
                return NotFound();
            empleadoBack = _mapper.Map<Empleado>(empleadoRequestDto);
            _empleado.Save(empleadoBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var empleadoBack = _empleado.GetById(id.Value);
            if (empleadoBack is null)
                return NotFound();
            _empleado.Delete(empleadoBack.Id);
            return Ok();
        }
    }
}

using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.TipoMadera;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposMaderasController : ControllerBase
    {
        private readonly ILogger<TiposMaderasController> _logger;
        private readonly IApplication<TipoMadera> _tipoMadera;
        private readonly IMapper _mapper;
        public TiposMaderasController(ILogger<TiposMaderasController> logger, IApplication<TipoMadera> tipoMadera, IMapper mapper)
        {
            _logger = logger;
            _tipoMadera = tipoMadera;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<TipoMaderaResponseDto>>(_tipoMadera.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var tipoMadera = _tipoMadera.GetById(id.Value);
            if (tipoMadera is null)
                return NotFound();
            return Ok(_mapper.Map<TipoMaderaResponseDto>(tipoMadera));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoMaderaRequestDto tipoMaderaRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var tipoMadera = _mapper.Map<TipoMadera>(tipoMaderaRequestDto);
            _tipoMadera.Save(tipoMadera);
            return Ok(tipoMadera.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, TipoMaderaRequestDto tipoMaderaRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }

            if (!ModelState.IsValid)
                return BadRequest();
            var tipoMaderaBack = _tipoMadera.GetById(id.Value);
            if (tipoMaderaBack is null)
                return NotFound();
            tipoMaderaBack = _mapper.Map<TipoMadera>(tipoMaderaRequestDto);
            _tipoMadera.Save(tipoMaderaBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
                return BadRequest();
            var tipoMaderaBack = _tipoMadera.GetById(id.Value);
            if (tipoMaderaBack is null)
                return NotFound();
            _tipoMadera.Delete(tipoMaderaBack.Id);
            return Ok();
        }
    }
}

using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Venta;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly ILogger<VentasController> _logger;
        private readonly IApplication<Venta> _venta;
        private readonly IMapper _mapper;
        public VentasController(ILogger<VentasController> logger, IApplication<Venta> venta, IMapper mapper)
        {
            _logger = logger;
            _venta = venta;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<VentaResponseDto>>(_venta.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var venta = _venta.GetById(id.Value);
            if (venta is null)
                return NotFound();
            return Ok(_mapper.Map<VentaResponseDto>(venta));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(VentaRequestDto ventaRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var venta = _mapper.Map<Venta>(ventaRequestDto);
            _venta.Save(venta);
            return Ok(venta.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, VentaRequestDto ventaRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
                return BadRequest();
            var ventaBack = _venta.GetById(id.Value);
            if (ventaBack is null)
                return NotFound();
            ventaBack = _mapper.Map<Venta>(ventaRequestDto);
            _venta.Save(ventaBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            var ventaBack = _venta.GetById(id.Value);
            if (ventaBack is null)
                return NotFound();
            _venta.Delete(ventaBack.Id);
            return Ok();
        }
    }
}

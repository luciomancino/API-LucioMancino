using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Proveedor;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly ILogger<ProveedoresController> _logger;
        private readonly IApplication<Proveedor> _proveedor;
        private readonly IMapper _mapper;


        public ProveedoresController(ILogger<ProveedoresController> logger, IApplication<Proveedor> proveedor, IMapper mapper)
        {
            _logger = logger;
            _proveedor = proveedor;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<ProveedorResponseDto>>(_proveedor.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var proveedor = _proveedor.GetById(id.Value);
            if (proveedor is null)
                return NotFound();
            return Ok(_mapper.Map<ProveedorResponseDto>(proveedor));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProveedorRequestDto proveedorRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var proveedor = _mapper.Map<Proveedor>(proveedorRequestDto);
            _proveedor.Save(proveedor);
            return Ok(proveedor.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, ProveedorRequestDto proveedorRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
                return BadRequest();
            var proveedorBack = _proveedor.GetById(id.Value);
            if (proveedorBack is null)
                return NotFound();
            proveedorBack = _mapper.Map<Proveedor>(proveedorRequestDto);
            _proveedor.Save(proveedorBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            var proveedorBack = _proveedor.GetById(id.Value);
            if (proveedorBack is null)
                return NotFound();
            _proveedor.Delete(proveedorBack.Id);
            return Ok();
        }
    }
}

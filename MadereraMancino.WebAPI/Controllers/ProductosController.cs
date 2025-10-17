using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Producto;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly IApplication<Producto> _producto;
        private readonly IMapper _mapper;

        public ProductosController(ILogger<ProductosController> logger, IApplication<Producto> producto, IMapper mapper)
        {
            _logger = logger;
            _producto = producto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<ProductoResponseDto>>(_producto.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var producto = _producto.GetById(id.Value);
            if (producto is null)
                return NotFound();
            return Ok(_mapper.Map<ProductoResponseDto>(producto));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoRequestDto productoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var producto = _mapper.Map<Producto>(productoRequestDto);
            _producto.Save(producto);
            return Ok(producto.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, ProductoRequestDto productoRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
                return BadRequest();
            var productoBack = _producto.GetById(id.Value);
            if (productoBack is null)
                return NotFound();
            productoBack = _mapper.Map<Producto>(productoRequestDto);
            _producto.Save(productoBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            var productoBack = _producto.GetById(id.Value);
            if (productoBack is null)
                return NotFound();
            _producto.Delete(productoBack.Id);
            return Ok();
        }
    }
}

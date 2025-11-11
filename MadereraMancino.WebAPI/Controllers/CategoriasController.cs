using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Categoria;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly IApplication<Categoria> _categoria;
        private readonly IMapper _mapper;

        public CategoriasController(ILogger<CategoriasController> logger, IApplication<Categoria> categoria, IMapper mapper)
        {
            _logger = logger;
            _categoria = categoria;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<CategoriaResponseDto>>(_categoria.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var categoria = _categoria.GetById(id.Value);
            if (categoria is null)
                return NotFound();
            return Ok(_mapper.Map<CategoriaResponseDto>(categoria));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaRequestDto categoriaRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var categoria = _mapper.Map<Categoria>(categoriaRequestDto);
            _categoria.Save(categoria);
            return Ok(categoria.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int? id, CategoriaRequestDto categoriaRequestDto)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            Categoria categoriaBack = _categoria.GetById(id.Value);
            if (categoriaBack is null)
            { return NotFound(); }
            categoriaBack= _mapper.Map<Categoria>(categoriaRequestDto);
            _categoria.Save(categoriaBack);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (!id.HasValue)
            { return BadRequest(); }
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var categoriaBack = _categoria.GetById(id.Value);
            if (categoriaBack is null)
                return NotFound();
            _categoria.Delete(categoriaBack.Id);
            return Ok();
        }
    }
}

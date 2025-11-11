using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Categoria;
using MadereraMancino.Entities;
using MadereraMancino.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IApplication<Categoria> _categoria;
        private readonly IMapper _mapper;

        public CategoriasController(ILogger<CategoriasController> logger, UserManager<User> userManager, IApplication<Categoria> categoria, IMapper mapper)
        {
            _logger = logger;
            _categoria = categoria;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> GetAll()
        {
            var id = User.FindFirst("Id").Value.ToString();
            var user = _userManager.FindByIdAsync(id).Result;
            if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                await _userManager.IsInRoleAsync(user, "Cliente"))
            {
                var name = User.FindFirst("name");
                var a = User.Claims;
                return Ok(_mapper.Map<IList<CategoriaResponseDto>>(_categoria.GetAll()));
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var idUser = User.FindFirst("Id")?.Value;
            var user = await _userManager.FindByIdAsync(idUser);

            if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                await _userManager.IsInRoleAsync(user, "Cliente"))
            {
                var categoria = _categoria.GetById(id.Value);
                if (categoria is null)
                    return NotFound();
                return Ok(_mapper.Map<CategoriaResponseDto>(categoria));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(CategoriaRequestDto categoriaRequestDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(); }
            var categoria = _mapper.Map<Categoria>(categoriaRequestDto);
            _categoria.Save(categoria);
            return Ok(categoria.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

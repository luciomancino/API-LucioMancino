using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Producto;
using MadereraMancino.Entities;
using MadereraMancino.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MadereraMancino.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProductosController> _logger;
        private readonly IApplication<Producto> _producto;
        private readonly IMapper _mapper;

        public ProductosController(ILogger<ProductosController> logger, UserManager<User> userManager, IApplication<Producto> producto, IMapper mapper)
        {
            _logger = logger;
            _producto = producto;
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
                return Ok(_mapper.Map<IList<ProductoResponseDto>>(_producto.GetAll()));
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
                var producto = _producto.GetById(id.Value);
                if (producto is null)
                    return NotFound();
                return Ok(_mapper.Map<ProductoResponseDto>(producto));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(ProductoRequestDto productoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var producto = _mapper.Map<Producto>(productoRequestDto);
            _producto.Save(producto);
            return Ok(producto.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

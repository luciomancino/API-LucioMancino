using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Proveedor;
using MadereraMancino.Entities;
using MadereraMancino.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProveedoresController> _logger;
        private readonly IApplication<Proveedor> _proveedor;
        private readonly IMapper _mapper;


        public ProveedoresController(ILogger<ProveedoresController> logger, UserManager<User> userManager, IApplication<Proveedor> proveedor, IMapper mapper)
        {
            _logger = logger;
            _proveedor = proveedor;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var id = User.FindFirst("Id").Value.ToString();
                var user = _userManager.FindByIdAsync(id).Result;
                if (await _userManager.IsInRoleAsync(user, "Administrador") ||
                    await _userManager.IsInRoleAsync(user, "Cliente"))
                {
                    var name = User.FindFirst("name");
                    var a = User.Claims;
                    return Ok(_mapper.Map<IList<ProveedorResponseDto>>(_proveedor.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Proveedores.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> GetById(int? id)
        {
            try
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
                    var proveedor = _proveedor.GetById(id.Value);
                    if (proveedor is null)
                        return NotFound();
                    return Ok(_mapper.Map<ProveedorResponseDto>(proveedor));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Proveedor por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(ProveedorRequestDto proveedorRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var proveedor = _mapper.Map<Proveedor>(proveedorRequestDto);
                _proveedor.Save(proveedor);
                return Ok(proveedor.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el Proveedor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? id, ProveedorRequestDto proveedorRequestDto)
        {
            try
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
                return Ok(proveedorBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el Proveedor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Borrar(int? id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el Proveedor.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}

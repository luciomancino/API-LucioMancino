using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Venta;
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
    public class VentasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<VentasController> _logger;
        private readonly IApplication<Venta> _venta;
        private readonly IMapper _mapper;
        public VentasController(ILogger<VentasController> logger, UserManager<User> userManager, IApplication<Venta> venta, IMapper mapper)
        {
            _logger = logger;
            _venta = venta;
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
                    return Ok(_mapper.Map<IList<VentaResponseDto>>(_venta.GetAll()));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las Ventas.");
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
                    var venta = _venta.GetById(id.Value);
                    if (venta is null)
                        return NotFound();
                    return Ok(_mapper.Map<VentaResponseDto>(venta));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la Venta por Id.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(VentaRequestDto ventaRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var venta = _mapper.Map<Venta>(ventaRequestDto);
                _venta.Save(venta);
                return Ok(venta.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la Venta.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? id, VentaRequestDto ventaRequestDto)
        {
            try
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
                return Ok(ventaBack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la Venta.");
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
                var ventaBack = _venta.GetById(id.Value);
                if (ventaBack is null)
                    return NotFound();
                _venta.Delete(ventaBack.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar la Venta.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}

using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Empleado;
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
    public class EmpleadosController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EmpleadosController> _logger;
        private readonly IApplication<Empleado> _empleado;
        private readonly IMapper _mapper;

        public EmpleadosController(ILogger<EmpleadosController> logger, UserManager<User> userManager, IApplication<Empleado> empleado, IMapper mapper)
        {
            _logger = logger;
            _empleado = empleado;
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
                return Ok(_mapper.Map<IList<EmpleadoResponseDto>>(_empleado.GetAll()));
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
                var empleado = _empleado.GetById(id.Value);
                if (empleado is null)
                    return NotFound();
                return Ok(_mapper.Map<EmpleadoResponseDto>(empleado));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(EmpleadoRequestDto empleadoRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var empleado = _mapper.Map<Empleado>(empleadoRequestDto);
            _empleado.Save(empleado);
            return Ok(empleado.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

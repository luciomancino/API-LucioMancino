using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.TipoMadera;
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
    public class TiposMaderasController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TiposMaderasController> _logger;
        private readonly IApplication<TipoMadera> _tipoMadera;
        private readonly IMapper _mapper;
        public TiposMaderasController(ILogger<TiposMaderasController> logger, UserManager<User> userManager, IApplication<TipoMadera> tipoMadera, IMapper mapper)
        {
            _logger = logger;
            _tipoMadera = tipoMadera;
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
                return Ok(_mapper.Map<IList<TipoMaderaResponseDto>>(_tipoMadera.GetAll()));
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
                var tipoMadera = _tipoMadera.GetById(id.Value);
                if (tipoMadera is null)
                    return NotFound();
                return Ok(_mapper.Map<TipoMaderaResponseDto>(tipoMadera));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(TipoMaderaRequestDto tipoMaderaRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var tipoMadera = _mapper.Map<TipoMadera>(tipoMaderaRequestDto);
            _tipoMadera.Save(tipoMadera);
            return Ok(tipoMadera.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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

using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Cliente;
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
    public class ClientesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ClientesController> _logger;
        private readonly IApplication<Cliente> _cliente;
        private readonly IMapper _mapper;

        public ClientesController(ILogger<ClientesController> logger, UserManager<User> userManager, IApplication<Cliente> cliente, IMapper mapper)
        {
            _logger = logger;
            _cliente = cliente;
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
                return Ok(_mapper.Map<IList<ClienteResponseDto>>(_cliente.GetAll()));
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
                Cliente cliente = _cliente.GetById(id.Value);
                if (cliente is null)
                    return NotFound();
                return Ok(_mapper.Map<ClienteResponseDto>(cliente));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Crear(ClienteRequestDto clienteRequesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var cliente = _mapper.Map<Cliente>(clienteRequesDto);
            _cliente.Save(cliente);
            return Ok(cliente.Id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Editar(int? id, ClienteRequestDto clienteRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var clienteBack = _cliente.GetById(id.Value);
            if (clienteBack is null)
                return NotFound();
            clienteBack= _mapper.Map<Cliente>(clienteRequestDto);
            _cliente.Save(clienteBack);
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
            var clienteBack = _cliente.GetById(id.Value);
            if (clienteBack is null)
                return NotFound();
            _cliente.Delete(clienteBack.Id);
            return Ok();
        }
    }
}

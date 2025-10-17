using AutoMapper;
using MadereraMancino.Application;
using MadereraMancino.Application.Dtos.Cliente;
using MadereraMancino.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadereraMancino.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IApplication<Cliente> _cliente;
        private readonly IMapper _mapper;

        public ClientesController(ILogger<ClientesController> logger, IApplication<Cliente> cliente, IMapper mapper)
        {
            _logger = logger;
            _cliente = cliente;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<ClienteResponseDto>>(_cliente.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Cliente cliente = _cliente.GetById(id.Value);
            if (cliente is null)
                return NotFound();
            return Ok(_mapper.Map<ClienteResponseDto>(cliente));
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteRequestDto clienteRequesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var cliente = _mapper.Map<Cliente>(clienteRequesDto);
            _cliente.Save(cliente);
            return Ok(cliente.Id);
        }

        [HttpPut("{id}")]
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

using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs;
using OrderManager.Services;
using System.Net;

namespace OrderManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService) => _clientService = clientService;

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Client>>>> GetClients()
        {
            var response = await _clientService.GetClients();
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<ServiceResponse<Client>>> GetClient(Guid clientId)
        {
            var response = await _clientService.GetClient(clientId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Client>>> AddClient(ClientDTO newClient)
        {
            var response = await _clientService.AddClient(newClient);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Client>>> UpdateClient(ClientDTO updatedClient)
        {
            var response = await _clientService.UpdateClient(updatedClient);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<Client>>> DeleteClient(Guid clientId)
        {
            var response = await _clientService.DeleteClient(clientId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

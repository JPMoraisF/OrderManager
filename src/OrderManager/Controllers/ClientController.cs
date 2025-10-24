using Microsoft.AspNetCore.Mvc;
using OrderManager.Models;
using OrderManager.Models.DTOs.ClientDto;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(
        IClientService service) : ControllerBase
        
    {
        /// <summary>
        /// Gets all the clients
        /// </summary>
        /// <returns>A list of ClientDto containing client information.</returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ClientDTO>>>> GetAll()
        {
            var response = await service.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Gets information about a single client.
        /// </summary>
        /// <param name="id">The clientId, in GUID format</param>
        /// <returns>The single client information.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ClientDetailsDto>>> GetById(Guid id)
        {
            var response = await service.GetByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new client
        /// </summary>
        /// <param name="clientDto">The client object</param>
        /// <returns>The newly created client information.</returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ClientDTO>>> Create(ClientCreateDto clientDto)
        {
            var response = await service.CreateAsync(clientDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates a new client
        /// </summary>
        /// <param name="id">The client id, in GUID format to be updated.</param>
        /// <param name="clientDto">The client dto object with the information to be edited.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ClientDTO>>> Update(Guid id, ClientUpdateDto clientDto)
        {
            var response = await service.UpdateAsync(id, clientDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a client based on the client Guid. A client can only be deleted once there's no work orders marked as OPEN
        /// </summary>
        /// <param name="clientId">The client id in GUID format</param>
        /// <returns>A boolean indicating if the client was deleted or not</returns>
        [HttpDelete("{clientId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(Guid clientId)
        {
            var response = await service.DeleteAsync(clientId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

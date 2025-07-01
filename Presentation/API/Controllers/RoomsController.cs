using Application.Abstractions.Services;
using Application.DTOs.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            _service = service;
        }

        /*[HttpPost]
        public async Task<IActionResult> Create(CreateRoomDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpGet("soft-deleted")]
        public async Task<IActionResult> GetAllSoftDeleted()
        {
            var response = await _service.GetAllSoftDeletedAsync();
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetByIdAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }
        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> GetByIdSoftDelete(string id)
        {
            var response= await _service.GetByIdSoftDeletedAsync(id); 
            return StatusCode((int)response.ResponseStatusCode, response);

        }
        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var response = await _service.SoftDeleteAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRoomDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            return StatusCode((int)response.ResponseStatusCode, response);
        }*/

    }
}

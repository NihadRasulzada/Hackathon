using Api.Extensions;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return this.HandleResponse(response);
        }

        [HttpGet("soft-deleted")]
        public async Task<IActionResult> GetAllSoftDeleted()
        {
            var response = await _service.GetAllSoftDeletedAsync();
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteAsync(id);
            return this.HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetByIdAsync(id);
            return this.HandleResponse(response);
        }
        [HttpGet("soft-delete/{id}")]
        public async Task<IActionResult> GetByIdSoftDelete(string id)
        {
            var response = await _service.GetByIdSoftDeletedAsync(id);
            return this.HandleResponse(response);
        }
        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var response = await _service.SoftDeleteAsync(id);
            return this.HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRoomDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            return this.HandleResponse(response);
        }

    }
}

using Application.Abstractions.Services;
using Application.DTOs.ReservationDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDTOs dto)
        {
            var response = await _service.CreateReservationAsync(dto);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpGet("soft-deleted")]
        public async Task<IActionResult> GetAllSoftDeleted()
        {
            var response = await _service.GetAllSoftDeletedReservationsAsync();
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteReservationAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetReservationByIdAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var response = await _service.SoftDeleteReservationAsync(id);
            return StatusCode((int)response.ResponseStatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateReservationDTOs dto)
        {
            var response = await _service.UpdateReservationAsync(id, dto);
            return StatusCode((int)response.ResponseStatusCode, response);
        }


    }
}

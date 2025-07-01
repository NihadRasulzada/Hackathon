using Api.Extensions;
using Application.Abstractions.Services;
using Application.DTOs.ServiceDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesService _service;

        public ServiceController(IServicesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceDTOs dto)
        {
            var response = await _service.CreateServiceAsync(dto);
            return this.HandleResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllServicesAsync();
            return this.HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetServiceByIdAsync(id);
            return this.HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateServiceDTOs dto)
        {
            var response = await _service.UpdateServiceAsync(id, dto);
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.DeleteServiceAsync(id);
            return this.HandleResponse(response);
        }

        [HttpGet("soft-deleted")]
        public async Task<IActionResult> GetAllSoftDeleted()
        {
            var response = await _service.GetAllSoftDeletedServicesAsync();
            return this.HandleResponse(response);
        }

        [HttpPut("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            var response = await _service.SoftDeleteServiceAsync(id);
            return this.HandleResponse(response);
        }

        [HttpGet("soft-delete/{id}")]
        public async Task<IActionResult> GetByIdSoftDeleted(string id)
        {
            var response = await _service.GetServiceByIdAsyncSoftDelete(id);
            return this.HandleResponse(response);
        }
    }
}

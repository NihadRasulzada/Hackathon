using Api.Extensions;
using Application.Abstractions.Services;
using Application.DTOs.ReservationServiceDTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReservationServicesController : ControllerBase
{
    private readonly IReservationServicesService _service;

    public ReservationServicesController(IReservationServicesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationServiceDTOs dto)
    {
        var response = await _service.CreateReservationServiceAsync(dto);
        return this.HandleResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAllReservationServicesAsync();
        return this.HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _service.GetReservationServiceByIdAsync(id);
        return this.HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _service.DeleteReservationServiceAsync(id);
        return this.HandleResponse(response);
    }
}

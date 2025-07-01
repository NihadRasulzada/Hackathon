using Api.Extensions;
using Application.Abstractions.Services;
using Application.DTOs.ReservationDTOs;
using Application.DTOs.ServiceDTOs;
using Application.ResponceObject;
using Microsoft.AspNetCore.Mvc;

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
        Response response = await _service.CreateReservationAsync(dto);
        return this.HandleResponse(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAllReservationsAsync();
        return this.HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _service.GetReservationByIdAsync(id);
        return this.HandleResponse(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateReservationDTOs dto)
    {
        var response = await _service.UpdateReservationAsync(id, dto);
        return this.HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _service.DeleteReservationAsync(id);
        return this.HandleResponse(response);
    }

    [HttpGet("soft-deleted")]
    public async Task<IActionResult> GetAllSoftDeleted()
    {
        var response = await _service.GetAllSoftDeletedReservationsAsync();
        return this.HandleResponse(response);
    }

    [HttpPut("soft-delete/{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        var response = await _service.SoftDeleteReservationAsync(id);
        return this.HandleResponse(response);
    }

    [HttpGet("soft-delete/{id}")]
    public async Task<IActionResult> GetByIdSoftDelete(string id)
    {
        var response = await _service.GetReservationByIdAsyncSoftDelete(id);

        return this.HandleResponse(response);
    }

}

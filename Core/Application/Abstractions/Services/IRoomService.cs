using Application.DTOs.Rooms;
using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface IRoomService
    {

        Task<Response<IEnumerable<RoomItemDto>>> GetAllAsync();
        Task<Response<GetRoomDto>> GetByIdAsync(string id);
        Task<Response> CreateAsync(CreateRoomDto roomDto);
        Task<Response> UpdateAsync(string id, UpdateRoomDto roomDto);
        Task<Response> DeleteAsync(string id); // hard delete
        Task<Response> SoftDeleteAsync(string id);
        Task<Response<IEnumerable<GetRoomDto>>> GetAllSoftDeletedAsync();
        Task<Response<GetRoomDto>> GetByIdSoftDeletedAsync(string id);
    }
}

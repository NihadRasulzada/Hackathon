using Application.DTOs.Rooms;
using Application.ResponceObject;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IRoomService
    {
        
        Task<Response<IEnumerable<RoomItemDto>>> GetAllAsync();
        Task<Response<GetRoomDto>> GetByIdAsync(string id);
        Task<Response> CreateAsync(CreateRoomDto roomDto);
        Task<Response> UpdateAsync(string id,UpdateRoomDto roomDto);
        Task<Response> DeleteAsync(string id); // hard delete
        Task<Response> SoftDeleteAsync(string id);
        Task<Response<IEnumerable<GetRoomDto>>> GetAllSoftDeletedAsync();
        Task<Response<GetRoomDto>> GetByIdSoftDeletedAsync(string id);
    }
}

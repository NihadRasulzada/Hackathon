using Application.Abstractions.Services;
using Application.DTOs.Rooms;
using Application.Repositories;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RoomService : IRoomService
    {

 
        private readonly IReadRepository<Room> _readRepository;
        private readonly IWriteRepository<Room> _writeRepository;
        private readonly IMapper _mapper;

        public RoomService(IReadRepository<Room> readRepository,IWriteRepository<Room> writeRepository, IMapper mapper)
        {
     
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateAsync(CreateRoomDto roomDto)
        {
            Room? room=_mapper.Map<Room>(roomDto);
            if(room == null)
            {
                return new Response<GetRoomDto>(ResponseStatusCode.NotFound, "Otaq əlavə edilə bilmədi");
            }
            bool result=await _writeRepository.AddAsync(room);
            if (!result)
                return new Response<GetRoomDto>(ResponseStatusCode.Error, "Otaq əlavə edilə bilmədi");
        
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Otaq uğurla əlavə edildi.");

        }

        public async Task<Response> DeleteAsync(string id)
        {
            var room = await _readRepository.GetByIdAsync(id);
            if (room == null)
                return new Response(ResponseStatusCode.NotFound, "Otaq tapılmadı.");

            _writeRepository.Remove(room);
            int affectedRows = await _writeRepository.SaveAsync();

            if (affectedRows <= 0)
                return new Response(ResponseStatusCode.Error, "Otaq silinə bilmədi.");

            return new Response(ResponseStatusCode.Success, "Otaq uğurla silindi.");
        }

        public async Task<Response<IEnumerable<RoomItemDto>>> GetAllAsync()
        {
            var rooms = await _readRepository.GetAll().ToListAsync();
            var roomItemDtos = _mapper.Map<List<RoomItemDto>>(rooms);

            return new Response<IEnumerable<RoomItemDto>>(ResponseStatusCode.Success, roomItemDtos);

        }

        public async Task<Response<IEnumerable<GetRoomDto>>> GetAllSoftDeletedAsync()
        {
            var deletedRooms = await _readRepository
                .GetWhere(r => r.IsDeleted == false)
                .ToListAsync();

            var roomDtos = _mapper.Map<List<GetRoomDto>>(deletedRooms);

            return new Response<IEnumerable<GetRoomDto>>(ResponseStatusCode.Success, roomDtos);

        }

        public async Task<Response<GetRoomDto>> GetByIdAsync(string id)
        {
            var room = await _readRepository
                .GetWhere(r => r.Id == id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (room == null)
                return new Response<GetRoomDto>(ResponseStatusCode.NotFound, $"ID-si {id} olan otaq tapılmadı.");

            var dto = _mapper.Map<GetRoomDto>(room);
            return new Response<GetRoomDto>(ResponseStatusCode.Success, dto);

        }

        public async Task<Response<GetRoomDto>> GetByIdSoftDeletedAsync(string id)
        {
            var room = await _readRepository
                 .GetWhere(r => r.Id == id && r.IsDeleted)
                 .FirstOrDefaultAsync();
            if (room == null)
            {
                return new Response<GetRoomDto>(ResponseStatusCode.NotFound, "Soft delete edilmiş otaq tapılmadı.");
            }

            var roomDto = _mapper.Map<GetRoomDto>(room);
            return new Response<GetRoomDto>(ResponseStatusCode.Success, roomDto);

        }

        public async Task<Response> SoftDeleteAsync(string id)
        {
            var room = await _readRepository
                .GetWhere(r => r.Id == id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (room == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan otaq tapılmadı və ya artıq silinib.");

            room.IsDeleted = true;

            _writeRepository.Update(room);
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Otaq uğurla silindi.");

        }

        public async Task<Response> UpdateAsync(string id,UpdateRoomDto roomDto)
        {
            var room = await _readRepository
               .GetWhere(r => r.Id == id && r.IsDeleted == false)
               .FirstOrDefaultAsync();

            if (room == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan otaq tapılmadı.");

            _mapper.Map(roomDto, room);

            _writeRepository.Update(room);
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Otaq uğurla yeniləndi.");

        }

        
    }
}

using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Rooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Rooms
{
    public class RoomService:IRoomService
    {
        public IQQInvRepository<Room> room;
        public RoomService(IQQInvRepository<Room> _room)
        {
            room = _room;
        }
        public List<RoomDto> GetAll()
        {
            var list = room.Query();
            List<RoomDto> roomList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Room, RoomDto>(c));

            return roomList;
        }
    }
}
using LABMANAGE.Data;
using LABMANAGE.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.ysl_Sign_In
{
    public class RoomNameService : IRoomNameService
    {
        public IQQInvRepository<Room> tableRoom;
        public RoomNameService(IQQInvRepository<Room> _tableRoom)
        {
            tableRoom = _tableRoom;
        }
        public List<Room> Rname()
        {
            var list = tableRoom.Query();//获取所有实验室信息
            List<Room> userList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Room, Room>(c));
            return userList;
        }

    }
}
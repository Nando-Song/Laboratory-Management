using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.WYH.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.WYH
{
    public class GetUserService:IGetUserService
    {
        public IQQInvRepository<User> qqRepository;
        public GetUserService(IQQInvRepository<User> _qqRepository)
        {
            qqRepository = _qqRepository;
        }

        public List<UserDto> GetUser(int id)
        {
            var list = qqRepository.Query().Where(c => c.ID == id);
            List<UserDto> UserList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, UserDto>(c));
            return UserList;
        }
    }
}
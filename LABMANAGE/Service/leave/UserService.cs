using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.leave.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.leave
{
    public class UserService : IUserService
    {
        public IQQInvRepository<User> UserRepository;
        public UserService(IQQInvRepository<User> _UserRepository)
        {
            UserRepository = _UserRepository;
        }
        public List<Dto.UserBaseDto> GetAll()
        {
            var list =UserRepository.Query();
            List<UserBaseDto> qqList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, UserBaseDto>(c));
            UserBaseDto tt = qqList.FirstOrDefault();
            //Leave ss = AutoMapperHelp.ConvertModel<Leave, LeaveBaseDto>(tt);
            // 反向转换为Mode
            int count = list.Count();
            return qqList;

            throw new NotImplementedException();
        }
    }
}
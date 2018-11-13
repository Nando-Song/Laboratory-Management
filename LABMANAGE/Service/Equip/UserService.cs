using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Equip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Equip
{
    public class UserService:IUserService
    {
        public IQQInvRepository<User> UserRepository;
        public UserService(IQQInvRepository<User> _UserRepository)
        {
            UserRepository = _UserRepository;
        }
        List<EUserBaseDto> IUserService.GetAll()
        {
            var list = UserRepository.Query();
            List<EUserBaseDto> qqList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, EUserBaseDto>(c));
            EUserBaseDto tt = qqList.FirstOrDefault();
            //Leave ss = AutoMapperHelp.ConvertModel<Leave, LeaveBaseDto>(tt);
            // 反向转换为Mode
            int count = list.Count();
            return qqList;
        }
    }
}
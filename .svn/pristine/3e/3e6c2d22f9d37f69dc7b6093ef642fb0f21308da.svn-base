using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Login.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Login
{
    public class LoginService : ILoginService
    {
        public IQQInvRepository<User> userRepository;
        public LoginService(IQQInvRepository<User> _userRepository)
        {
            userRepository = _userRepository;
        }

        //public List<Dto.LoginBaseDto> GetAll()
        //{
        //    //var list = userRepository.Query();
        //    //List<LoginBaseDto> UserList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, LoginBaseDto>(c));
        //    //LoginBaseDto m = UserList.FirstOrDefault();
        //    // User n= AutoMapperHelp.ConvertModel<User, LoginBaseDto>(m);
        //    //  return UserList;

        //}
        public LoginBaseDto Get(string username, string password)
        {
            var query = userRepository.Query().Where(m => (m.Name == username || m.Email == username || m.Phone == username || m.Real_Name == username) && m.Password == password);
            if (query != null && query.Count() > 0)
            {
                var model = query.FirstOrDefault();
                return AutoMapperHelp.ConvertToDto<User, LoginBaseDto>(model);
            }
            return null;
        }
        public LoginBaseDto GetPwd(string username, string Realname, string phone, string Email)
        {
            var query = userRepository.Query().Where(m => m.Name == username && m.Real_Name == Realname && m.Phone == phone && m.Email == Email);
            //List<LoginBaseDto> UserList = query.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, LoginBaseDto>(c));             
            if (query != null && query.Count() > 0)
            {
                var model = query.FirstOrDefault();
                return AutoMapperHelp.ConvertToDto<User, LoginBaseDto>(model);
            }
            return null;
        }
    } 
}
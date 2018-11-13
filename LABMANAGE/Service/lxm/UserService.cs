using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.lxm.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.lxm
{
    public class UserService:IUserService
    {
        public IQQInvRepository<User> userRepository;

        public UserService(IQQInvRepository<User> _userRepository)
        {
            userRepository = _userRepository;
        }

        public List<UserBaseDto> GetAll(int ID)
        {

            var list = userRepository.Query().Where(m=>m.ID == ID);
            List<UserBaseDto> userList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, UserBaseDto>(c));
            UserBaseDto tt = userList.FirstOrDefault();

            User ss = AutoMapperHelp.ConvertModel<User, UserBaseDto>(tt);

            int count = list.Count();
            return userList;
        }
        public void Update(string img)
        {
            int id = 8;
            User evaSum = userRepository.Get(id);
            //evaSum.Teacher_evaluation = summary.Teacher_evaluation;
            evaSum.Image = img;
            userRepository.Update(evaSum);
        }
        public void UpdateMessage(string name, string motto, string Email, string Phone)
        {
            int id = 8;
            User evaSum = userRepository.Get(id);
            //evaSum.Teacher_evaluation = summary.Teacher_evaluation;
            if (name != null && name != "")
            {
                evaSum.Name = name;
            }
            if (motto != null && motto != "")
            {
                evaSum.Motto = motto;
            }
            if (Email != null && Email != "")
            {
                evaSum.Email = Email;
            }
            if (Phone != null && Phone != "")
            {
                evaSum.Phone = Phone;
            }
            userRepository.Update(evaSum);
        }
        public void Updatepwd(string password)
        {
            int id = 8;
            User evaSum = userRepository.Get(id);
            //evaSum.Teacher_evaluation = summary.Teacher_evaluation;
            evaSum.Password = password;

            userRepository.Update(evaSum);
        }
    }
}
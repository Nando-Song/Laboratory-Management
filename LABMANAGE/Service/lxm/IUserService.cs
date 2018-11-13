using LABMANAGE.Service.lxm.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.lxm
{
    public interface IUserService
    {
        List<UserBaseDto> GetAll(int ID);
        void Update(string img);
        void UpdateMessage(string name, string motto, string Email, string Phone);
        void Updatepwd(string password);
    }
}

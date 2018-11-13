using LABMANAGE.Service.Register.Dto;
using LABMANAGE.Service.UserManage.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.UserManage
{
    public interface IUserManService
    {
        List<UserManDto> getUserManage(string userName, int pageSize, int curPage, string userRole, bool selectIsTea, int roomID, out long recordCount);
        bool UserCheck(int userId);
        bool UserDel(int userId);
        bool UpdateRole(RegisterDto UserInfo);
    }
}

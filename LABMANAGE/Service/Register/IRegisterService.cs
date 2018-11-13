using LABMANAGE.Service.Register.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.Register
{
    public interface IRegisterService
    {
        List<RegisterDto> GetNameInfo(string Name);
        List<RegisterDto> GetPhoneInfo(string Phone);
        List<RegisterDto> GetEmailInfo(string Email);
        bool InsertInfo(RegisterDto UserInfo);
    }
}

using LABMANAGE.Service.ysl_Sign_In.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.ysl_Sign_In
{
    public interface IUserDataService
    {

        List<User_name_uidDto> getUser();

        int get_uid(string name);
    }
}

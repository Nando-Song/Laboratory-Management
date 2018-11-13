using LABMANAGE.Service.Equip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.Equip
{
    public interface IUserService
    {
        List<EUserBaseDto> GetAll();
    }
}

using LABMANAGE.Service.Equip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.Equip
{
    public interface IEquipService
    {
        List<EquipBaseDto> GetEquipList(string nickName, int curPage, int PageSize, out long recordCount);
        bool InsertInfo(EquipBaseDto Equipinfo);
        bool Update(EquipBaseDto Equip);
        EquipBaseDto GetEquipPer(int id);
    }
}

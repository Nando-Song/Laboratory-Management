using LABMANAGE.Service.leave.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.leave
{
    public interface ILeaveService
    {
        List<LeaveBaseDto> GetLeaveList(string nickName, string nickTime, int curPage, int PageSize,string room, out long recordCount);
        List<LeaveBaseDto> GetAll();
        bool InsertInfo(LeaveBaseDto Leaveinfo);
        bool Update(LeaveBaseDto summary);
        LeaveBaseDto GetLeavePer(int id);
    }
}

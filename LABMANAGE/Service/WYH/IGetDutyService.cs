using LABMANAGE.Service.WYH.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.WYH
{
    public interface IGetDutyService
    {
        /// <summary>
        /// 获取值日表
        /// </summary>
        /// <returns></returns>
        List<DutyDto> GetDuty();
        /// <summary>
        /// 插入值日信息
        /// </summary>
        /// <param name="DutyStart"></param>
        /// <param name="DutyEnd"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        bool InsertDtoData(DateTime DutyStart, DateTime DutyEnd, int User_ID);
        /// <summary>
        /// 删除值日信息
        /// </summary>
        /// <param name="DutyStart"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        bool DelDtoData(DateTime DutyStart, int User_ID);
    }
}

using LABMANAGE.Service.WYH.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.WYH
{
    public interface IShowNoticeService
    {
        /// <summary>
        /// 获取公告表
        /// </summary>
        /// <returns></returns>
        List<NoticeDto> GetNotice();
        /// <summary>
        /// 插入（更新）公告信息
        /// </summary>
        /// <param name="oNoticeTitle"></param>
        /// <param name="oNoticeText"></param>
        /// <param name="oNowTime"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        bool InsertNoticeData(string oNoticeTitle, string oNoticeText, DateTime oNowTime, int UserId);
    }
}

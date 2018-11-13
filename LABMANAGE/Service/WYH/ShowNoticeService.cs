using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.WYH.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.WYH
{
    public class ShowNoticeService : IShowNoticeService
    {
        
        public IQQInvRepository<Notice> LabRepository;
        public ShowNoticeService(IQQInvRepository<Notice> _LabRepository)
        {
            LabRepository = _LabRepository;
        }

        public List<NoticeDto> GetNotice()
        {
            var list = LabRepository.Query().Where(c=> c.Enabled == 1);
            List<NoticeDto> NoticeList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Notice, NoticeDto>(c));
            return NoticeList;
        }


        public bool InsertNoticeData(string oNoticeTitle, string oNoticeText, DateTime oNowTime, int UserId)
        {
            Notice aTable = new Notice();
            aTable.Enabled = 1;
            aTable.Text = oNoticeText;
            aTable.Title = oNoticeTitle;
            aTable.Time = oNowTime;
            aTable.User_ID = UserId;

            var list = LabRepository.Query().Where(c=> c.Enabled == 1);
            List<Notice> NoticeList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Notice, Notice>(c));
            if (NoticeList.Count != 0)
            {
                NoticeList[0].Enabled = 0;
                LabRepository.Update(NoticeList[0]);
            }
            LabRepository.Add(aTable);
            return true;
        }
    }
}
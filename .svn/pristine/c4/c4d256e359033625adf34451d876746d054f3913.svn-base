using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.WYH.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LABMANAGE.Service.WYH
{
    public class GetDutyService : IGetDutyService
    {

        public IQQInvRepository<Duty> LabRepository;
        public GetDutyService(IQQInvRepository<Duty> _LabRepository)
        {
            LabRepository = _LabRepository;
        }

        public List<DutyDto> GetDuty()
        {
            var list = LabRepository.Query();
            List<DutyDto> DutyList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Duty, DutyDto>(c));
            return DutyList;
        }
        //public List<DutyNeedDto> GetDutyData()
        //{
        //    DutyNeedDto DutyNeedData = new DutyNeedDto();
        //    List<DutyDto> DutyList = GetDuty();
        //    int userId = DutyList[0].User_ID;
        //    List<UserDto> UserList = UserData.GetUser(userId);
        //    DutyNeedData.Real_Name = UserList[0].Real_Name;
        //    DutyNeedData.DutyStart = DutyList[0].DutyStart;
        //    DutyNeedData.DutyEnd = DutyList[0].DutyEnd;
        //    //DutyNeedData.Url = DutyList[0].Url;
        //    List<DutyNeedDto> returnList = new List<DutyNeedDto>();
        //    returnList.Add(DutyNeedData);
        //    return returnList;
        //}
        public bool InsertDtoData(DateTime DutyStart, DateTime DutyEnd, int User_ID)
        {
            Duty oDuty = new Duty();
            oDuty.DutyStart = DutyStart;
            oDuty.DutyEnd = DutyEnd;
            oDuty.User_ID = User_ID;
            //var list = LabRepository.Query().Where(c => c.Enabled == 1);
            //List<Notice> NoticeList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Notice, Notice>(c));
            //NoticeList[0].Enabled = 0;
            //LabRepository.Update(NoticeList[0]);
            LabRepository.Add(oDuty);
            return true;
        }

        public bool DelDtoData(DateTime DutyStart, int User_ID)
        {
            Duty oDuty = new Duty();
            oDuty.DutyStart = DutyStart;
            oDuty.User_ID = User_ID;
            var List = LabRepository.Query().Where(c => c.User_ID == oDuty.User_ID && c.DutyStart == oDuty.DutyStart);
            List<Duty> DelList = List.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Duty, Duty>(c));
            //NoticeList[0].Enabled = 0;
            //LabRepository.Update(NoticeList[0]);
            if (DelList.Count < 1)
            {
                //没有这条数据
                return false;
            }
            else
            {
                LabRepository.Delete(DelList[0].ID);
                return true;
            } 
        }

    }
}
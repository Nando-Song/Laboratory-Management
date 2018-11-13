using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.leave.Dto;
using LABMANAGE.UntityCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.leave
{
    public class LeaveService : ILeaveService
    {
        public IQQInvRepository<Leave> LeaveRepository;
        public LeaveService(IQQInvRepository<Leave> _LeaveRepository)
        {
            LeaveRepository = _LeaveRepository;
        }
        #region  查询所有请假信息
        public List<Dto.LeaveBaseDto> GetAll()
        {
            var list = LeaveRepository.Query();
            List<LeaveBaseDto> qqList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Leave, LeaveBaseDto>(c));
            return qqList;
        }
        #endregion

        #region 请假申请
        public bool InsertInfo(LeaveBaseDto Leaveinfo)
        {
            try
            {
                Leave uList = AutoMapperHelp.ConvertModel<Leave, LeaveBaseDto>(Leaveinfo);
                Leave insertList = LeaveRepository.Add(uList);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion  
        #region 根据条件获取分页后，该页的信息
        public List<LeaveBaseDto> GetLeaveList(string nickName, string nickTime, int curPage, int PageSize,string room, out long recordCount)
        {   
            var query = LeaveRepository.Query();//.OrderByDescending(a=>a.Time);


            if (@LoginBase.RoleCode.Contains("R003"))//登陆者为学生
            {
                try
                {
                    int a = Convert.ToInt32(@LoginBase.ID);
                    query = query.Where(m => m.User_ID == a);
                }
                catch { }
            }
            else if (!String.IsNullOrEmpty(room) && room != "undefined" && room != "--请选择实验室--")
            {
                query = query.Where(m => m.User.Room_ID.ToString() == room);
            }
          
            if (!String.IsNullOrEmpty(nickName) && nickName != "undefined" && nickName != "请输入真实姓名")
            {
                query = query.Where(m => m.User.Real_Name == nickName);
            }
            //查询某个时间段
            
            if (!String.IsNullOrEmpty(nickTime) && nickTime != "undefined" && nickTime != "请选择查询时间段")
            {
                var StartTime = Convert.ToDateTime(nickTime.Split('-')[0]);
                var EndTime = Convert.ToDateTime(nickTime.Split('-')[1]);
                try
                {
                    query = query.Where(m => (m.Start_Time < StartTime && m.End_Time >= StartTime) || (m.Start_Time >= StartTime && m.Start_Time <= EndTime));
                }
                catch { };
                
            }

            recordCount = query.Count();
            query = query.OrderByDescending(m => m.ID).Skip((curPage - 1) * PageSize).Take(PageSize);
            List<LeaveBaseDto> LeaveList = query.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Leave, LeaveBaseDto>(c));

            return LeaveList;
        }

        #endregion
        #region 请假审核
        public bool Update(LeaveBaseDto leave)
        {
            try
            {
                Leave evaLeave = LeaveRepository.Get(leave.ID);
                evaLeave.IsExamine = leave.IsExamine;
                evaLeave.Pass = leave.Pass;
                LeaveRepository.Update(evaLeave);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 
        #region 查询单条数据
        public LeaveBaseDto GetLeavePer(int id)
        {
            var list = LeaveRepository.Query().Where(c => c.ID == id);
            List<LeaveBaseDto> qqList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Leave, LeaveBaseDto>(c));
            LeaveBaseDto tt = qqList.FirstOrDefault();
            return tt;
        }
        #endregion
    }
}
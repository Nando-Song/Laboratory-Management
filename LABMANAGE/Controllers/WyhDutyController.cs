using LABMANAGE.Service.WYH;
using LABMANAGE.Service.WYH.Dto;
using LABMANAGE.Service.ysl_Sign_In;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using LABMANAGE.UntityCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.Controllers
{
    public class WyhDutyController : Controller
    {
        /// <summary>
        /// 用来获取公告
        /// </summary>
        public IShowNoticeService show { get; set; }
        /// <summary>
        /// 用来获取用户信息
        /// </summary>
        public IGetUserService UserData { get; set; }
        /// <summary>
        /// 用来获取值日信息
        /// </summary>
        public IGetDutyService DutyData { get; set; }
        /// <summary>
        /// LIST转JSON类型
        /// </summary>
        public ISign_InService ISIS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IUserDataService IUDS { get; set; }
        //IShowNotice show = new ShowNotice();
        //
        // GET: /WyhDuty/
        public ActionResult Index()
        {
            try
            {
                int UID = int.Parse(LoginBase.ID);
                List<User_name_uidDto> all_user = IUDS.getUser();
                this.TempData["list"] = all_user;
                this.TempData["list2"] = all_user;
                return View();
            }
            catch
            {
                return View("../Login/Login");
            }
        }


        #region 插入值日信息
        /// <summary>
        /// 插入值日信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public JsonResult InsertDuty(string text)
        {
            try
            {
                DateTime DutyStart = Convert.ToDateTime(Request.Form[0]);
                DateTime DutyEnd = Convert.ToDateTime(Request.Form[1]);
                int User_ID = int.Parse(Request.Form[2]);
                DutyData.InsertDtoData(DutyStart, DutyEnd, User_ID);
                return Json("true");
            }
            catch
            {
                return Json("false");
            }
        } 
        #endregion
        
        #region 删除值日信息
        /// <summary>
        /// 删除值日信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public JsonResult DelDuty(string text)
        {
            try {
                DateTime DutyStart = Convert.ToDateTime(Request.Form[0]);
                int User_ID = int.Parse(Request.Form[1]);
                if (DutyData.DelDtoData(DutyStart, User_ID))
                {
                    return Json("true");
                }
                else
                {
                    return Json("false");
                }
            }
            catch
            {
                return Json("false");
            }
            
        } 
        #endregion

        #region 获取值日信息
        /// <summary>
        /// 获取值日信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDutyData()
        {
            //List<DutyNeedDto>   
            List<DutyDto> DutyList = DutyData.GetDuty();
            List<DutyNeedDto> returnList = new List<DutyNeedDto>();
            foreach (DutyDto ss in DutyList)
            {
                DutyNeedDto DutyNeedData = new DutyNeedDto();
                int userId = ss.User_ID;
                ss.DutyEnd = ss.DutyEnd.AddDays(1);
                List<UserDto> UserList = UserData.GetUser(userId);
                DutyNeedData.title = UserList[0].Real_Name;
                DutyNeedData.start = ss.DutyStart.ToString("yyyy-MM-dd");
                DutyNeedData.end = ss.DutyEnd.ToString("yyyy-MM-dd");
                
                //DutyNeedData.Url = ss.Url;
                returnList.Add(DutyNeedData);
            }

            return Json(ISIS.Obj2Json(returnList), JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region 插入公告信息
        /// <summary>
        /// 插入公告信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public JsonResult InsertDto(string text)
        {
            string oNoticeTitle = Request.Form[0].ToString();
            string oNoticeText = Request.Form[1].ToString();
            DateTime oNowTime = Convert.ToDateTime(Request.Form[2]);
            int UserId = int.Parse(LoginBase.ID);
            show.InsertNoticeData(oNoticeTitle, oNoticeText, oNowTime, UserId);
            return Json("true");
        } 
        #endregion

        #region 获取公告信息
        /// <summary>
        /// 获取公告信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public JsonResult ShowNotice(string text)
        {
            NoticeNeedDto NoticeNeedData = new NoticeNeedDto();
            List<NoticeDto> NoticeList = show.GetNotice();
            if (NoticeList.Count != 0)
            {
                int userId = NoticeList[0].User_ID;
                List<UserDto> UserList = UserData.GetUser(userId);
                NoticeNeedData.Name = UserList[0].Name;
                NoticeNeedData.Text = NoticeList[0].Text;
                NoticeNeedData.Time = NoticeList[0].Time;
                NoticeNeedData.Title = NoticeList[0].Title;
            }
            else
            {
                NoticeNeedData.Name = "无";
                NoticeNeedData.Text = "暂无公告";
                NoticeNeedData.Time = DateTime.Now;
                NoticeNeedData.Title = "暂无公告";
            }
            
            List<NoticeNeedDto> returnList = new List<NoticeNeedDto>();
            returnList.Add(NoticeNeedData);
            return Json(returnList);
        }
        #endregion


	}
}
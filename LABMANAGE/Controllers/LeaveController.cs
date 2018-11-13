﻿using LABMANAGE.SendEmail;
using LABMANAGE.Service.leave;
using LABMANAGE.Service.leave.Dto;
using LABMANAGE.Service.Rooms;
using LABMANAGE.Service.Rooms.Dto;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using LABMANAGE.Unitity;
using LABMANAGE.UntityCode;
using Newtonsoft.Json;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.Controllers
{
    public class LeaveController : Controller
    {
        public ILeaveService leaveService { get; set; }
        public IUserService userService { get; set; }
        public IRoomService RoomService { get; set; }

        public ISign_InService signService{get;set;}
        //
        // GET: /Leave/
        public ActionResult Index()
        {
            //1.根据Session 确定其身份,
            //2.如果是教师，获取所有学生的信息；如果是学生，则只获取该学生的信息
            //3.根据角色展示页面
 
            List<RoomDto> roomList = new List<RoomDto>();
            roomList = RoomService.GetAll();
            TempData["roomList"] = roomList;
            List<UserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LeaveBaseDto leaveBaseDto)
        {

            string VerfiyEmail = null;
            List<UserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
            int id = 0;
            try
            {
                id = Convert.ToInt32(@LoginBase.ID);
            }
            catch { return Content("<script>alert('尚未登录');window.location.href='../Login';</script>"); }
            leaveBaseDto.User_ID = id;
            
            try
            {
                int Valid = Convert.ToInt32(Request.Form["valid"]);
                 
                foreach (var item in UserModel)
                {
                    if (item.ID == Valid)
                    {
                        leaveBaseDto.Verify_ID = item.ID;
                        VerfiyEmail = item.Email;
                    }
                }
            }
            catch { return Content("<script>alert('请选择正确的审核用户！');window.location.href='Index';</script>"); }
            if (VerfiyEmail == null)
            {
                return Content("<script>alert('审核用户不存在！');window.location.href='AskLeave?id=" + id + "&flag=false';</script>");
            }
                
        
            string Time = Request.Form["time"];
            string StartTime, EndTime;
            StartTime = EndTime = null;
            if (Time != null)
            {
                StartTime = Time.Split('-')[0].Trim().ToString();
                EndTime = Time.Split('-')[1].Trim().ToString();
                leaveBaseDto.Start_Time = Convert.ToDateTime(StartTime);
                leaveBaseDto.End_Time = Convert.ToDateTime(EndTime);
            }
            System.DateTime NowTime = System.DateTime.Now;
            leaveBaseDto.Time = NowTime;
            leaveBaseDto.Pass = 0;
            leaveBaseDto.IsExamine = 0;

            try
            {
                if (leaveService.InsertInfo(leaveBaseDto))
                {
                    string senderServerIp = ConfigHelp.GetConfigValue("smtptype");   //使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
                    string toMailAddress = VerfiyEmail;              //要发送对象的邮箱
                    string fromMailAddress = ConfigHelp.GetConfigValue("useraddress");//你的邮箱
                    string subjectInfo = "请假申请";                  //主题
                    string bodyInfo = "<p>" + "有学生请假，请前往“请假管理”进行审核" + "</p>";//以Html格式发送的邮件
                    string mailUsername = ConfigHelp.GetConfigValue("user");              //登录邮箱的用户名
                    string mailPassword = ConfigHelp.GetConfigValue("pass"); //对应的登录邮箱的第三方密码（你的邮箱不论是163还是qq邮箱，都需要自行开通stmp服务）
                    string mailPort = "25";                      //发送邮箱的端口号
                    //创建发送邮箱的对象
                    Email myEmail = new Email(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, true, false);

                    //添加附件
                    //email.AddAttachments(attachPath);
                    //string message;
                    if (myEmail.Send())
                    {
                        return RedirectPermanent("/Leave/AskLeave?id=" + Convert.ToInt32(LoginBase.ID) + "&flag=true");
                    }
                    else
                    {
                        //return Content("<script>alert('邮件发送失败')window.location.href='Index';</script>");
                    }
                }
            }
            catch { }
            return RedirectPermanent("/Leave/AskLeave?id=" + Convert.ToInt32(LoginBase.ID) + "&flag=true");
        }

        public JsonResult GetLeaveList(string nickName, string nickTime, int curPage,string RoomID)  //获取分页列表
        {
            List<UserBaseDto> UserList = userService.GetAll();
            long recordCount = 0;
           
            int pageSize = Convert.ToInt32(ConfigHelp.GetConfigValue("pageSize"));
            List<LeaveBaseDto> LeaveList = leaveService.GetLeaveList(nickName.Trim(), nickTime, curPage, pageSize, RoomID, out recordCount);
   
            foreach (var item in LeaveList)
            {
                foreach (var item1 in UserList)
                {
                    if (item1.ID == item.User_ID)
                    {
                        item.Real_Name = item1.Real_Name;
                    }
                }
            }
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = recordCount, pageSize = pageSize, lists = LeaveList });
            return Json(jsonStr);
        }

        public JsonResult Update(LeaveBaseDto leaveBaseDto) //更新
        {
            LeaveBaseDto evaleave = leaveService.GetLeavePer(leaveBaseDto.ID);
            evaleave.Pass = leaveBaseDto.Pass;
            evaleave.IsExamine = leaveBaseDto.IsExamine;
            bool IsUpdate = leaveService.Update(evaleave);
            signService.LeaveInsert(evaleave.Start_Time, evaleave.End_Time, evaleave.User_ID);
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = 1 });
            return Json(jsonStr);
        }
        public void GetValid()
        {
            List<UserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
        }
        #region 请假
        public ActionResult AskLeave(int Id, bool flag)
        {
            if (flag == true)
            {
                ViewBag.Mssg = "添加成功！";
            }
            return View();
        }
        #endregion
        public void Export()
        {
            List<LeaveBaseDto> LeaveSheet = leaveService.GetAll();
            //创建Excel文件对象
            HSSFWorkbook book = new HSSFWorkbook();
            //创建DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            //创建SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            //将属赋值给Excel对象
            book.DocumentSummaryInformation = dsi;
            book.SummaryInformation = si;
            //添加一个sheet
            //ISheet sheet = book.CreateSheet("new sheet");
            ISheet sheet = book.CreateSheet("Sheet1");

            // 要创建单元格首先要创建单元格所在的行
            IRow row1 = sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("用户ID");
            row1.CreateCell(1).SetCellValue("请假时间");
            row1.CreateCell(2).SetCellValue("请假原因");
            row1.CreateCell(3).SetCellValue("审核人");


            //将数据逐步写入sheet1各个行 
            for (int i = 1; i < LeaveSheet.Count; i++)
            {
                IRow rowtemp = sheet.CreateRow(i);
                rowtemp.CreateCell(0).SetCellValue(LeaveSheet[i].User_ID);
                rowtemp.CreateCell(1).SetCellValue(LeaveSheet[i].Time);
                string Time = LeaveSheet[i].Time.ToString();
                rowtemp.CreateCell(1).SetCellValue(Time);
                rowtemp.CreateCell(2).SetCellValue(LeaveSheet[i].Reason);
                rowtemp.CreateCell(3).SetCellValue(LeaveSheet[i].Verify_ID);
            }
            //保存位置，若该位置下没有文件，则会新建一个
            FileStream file = new FileStream(@"E:/test.xls", FileMode.Create);
            book.Write(file);
            file.Close();
        }

    }
}
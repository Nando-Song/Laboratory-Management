﻿using LABMANAGE.SendEmail;
using LABMANAGE.Service.Equip;
using LABMANAGE.Service.Equip.Dto;
using LABMANAGE.Unitity;
using LABMANAGE.UntityCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.Controllers
{
    public class EquipController : Controller
    {
        public IEquipService equipService { get; set; }
        public IUserService userService { get; set; }
        //
        // GET: /Equip/
        public ActionResult Index()
        {
            List<EUserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
            return View();
        }

        [HttpPost]
        public ActionResult Index(EquipBaseDto equipBaseDto)
        {
            string VerfiyEmail = null;
            List<EUserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
            int id = 0;
            try
            {
                id = Convert.ToInt32(@LoginBase.ID);
            }
            catch { return Content("<script>alert('尚未登录');window.location.href='../Login';</script>"); }
            equipBaseDto.User_ID = id;
            try
            {
                int Valid = Convert.ToInt32(Request.Form["valid"]);
                foreach (var item in UserModel)
                {
                    if (item.ID == Valid)
                    {
                        equipBaseDto.Verify_ID = item.ID;
                        VerfiyEmail = item.Email;
                    }
                }

            }
            catch { return Content("<script>alert('该审核用户不存在');window.location.href='Index';</script>"); }
            if (VerfiyEmail == null)
            {
                return Content("<script>alert('审核用户不存在！');window.location.href='AskEquip?id=" + id + "&flag=false';</script>");
            }
            System.DateTime NowTime = System.DateTime.Now;
            equipBaseDto.Time = NowTime;
            equipBaseDto.Pass = 0;
            equipBaseDto.IsExamine = 0;
            try
            {
                if (equipService.InsertInfo(equipBaseDto))
                {
                    string senderServerIp = ConfigHelp.GetConfigValue("smtptype");   //使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
                    string toMailAddress = VerfiyEmail;              //要发送对象的邮箱
                    string fromMailAddress = ConfigHelp.GetConfigValue("useraddress");//你的邮箱
                    string subjectInfo = "设备申请";                  //主题
                    string bodyInfo = "<p>" + "有新的设备申请，请前往“设备管理”进行审核" + "</p>";//以Html格式发送的邮件
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
                        return RedirectPermanent("/Equip/AskEquip?id=" + Convert.ToInt32(LoginBase.ID) + "&flag=true");
                    }
                    else
                    {
                        return Content("<script>alert('邮件发送失败');</script>");
                    }
                }
            }
            catch { }


            return RedirectPermanent("/Equip/AskEquip?id=" + Convert.ToInt32(LoginBase.ID) + "&flag=true");
        }


        public JsonResult GetEquipList(string nickName, int curPage)  //获取分页列表
        {
            List<LABMANAGE.Service.Equip.Dto.EUserBaseDto> UserList = userService.GetAll();
            int pageSize = Convert.ToInt32(ConfigHelp.GetConfigValue("pageSize"));
            long recordCount = 0;
            List<EquipBaseDto> EquipList = null;

            EquipList = equipService.GetEquipList(nickName.Trim(), curPage, pageSize, out recordCount);

            foreach (var item in EquipList)
            {
                foreach (var item1 in UserList)
                {
                    if (item1.ID == item.User_ID)
                    {
                        item.Real_Name = item1.Real_Name;
                    }
                }
            }
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = recordCount,pageSize = pageSize, lists = EquipList });
            return Json(jsonStr);
        }

        //更新
        public JsonResult Update(EquipBaseDto equipBaseDto) 
        {
            EquipBaseDto evaequip = equipService.GetEquipPer(equipBaseDto.ID);
            evaequip.Pass = equipBaseDto.Pass;
            evaequip.IsExamine = equipBaseDto.IsExamine;
            bool IsUpdate = equipService.Update(evaequip);
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = 1 });
            return Json(jsonStr);
        }

        public ActionResult AskEquip(int Id, bool flag)
        {
            if (flag == true)
                ViewBag.Mssg = "添加成功！";
            return View();
        }
        public void GetValid()
        {
            List<EUserBaseDto> UserModel = userService.GetAll();
            TempData["peoplelist"] = UserModel;
        }
	}
}
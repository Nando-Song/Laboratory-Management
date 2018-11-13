﻿using LABMANAGE.Filter;
using LABMANAGE.SendEmail;
using LABMANAGE.Service.Register;
using LABMANAGE.Service.Register.Dto;
using LABMANAGE.Service.Rooms;
using LABMANAGE.Service.Rooms.Dto;
using LABMANAGE.Service.UserManage;
using LABMANAGE.Service.UserManage.Dto;
using LABMANAGE.Unitity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace LABMANAGE.Controllers
{
    public class UserManageController : Controller
    {
        //
        // GET: /UserManage/
        public IUserManService UserManService { get; set; }
        public IRoomService roomService { get; set; }
        public ActionResult UserManage()
        {
            List<RoomDto> list = new List<RoomDto>();
            list = roomService.GetAll();

            TempData["list"] = list;
            return View();
        }
        [WebMethod]
        public JsonResult getUserManage(string userName, int pageSize, int curPage, string userRole, bool selectIsTea, int roomID) //获取用户，登陆的如果是教师只能获取学生，如果是管理员则可以获取教师和学生
        {
             try
             {               
                 long recordCount = 0;
                 List<UserManDto> adminList = new List<UserManDto>();
                 adminList = UserManService.getUserManage(userName, pageSize, curPage, userRole, selectIsTea, roomID, out recordCount);
                 string adminJson = JsonConvert.SerializeObject(new { recordCount = recordCount, adminList = adminList });
                 return Json(adminJson);
             }
             catch { return null; }
        } 
        [HttpPost]
        public ContentResult UserCheck(int userId, string email) //用户审核，用于审核刚注册的用户
        {
            try
            {              
                bool success = UserManService.UserCheck(userId);
                if (success) 
                {
                    string senderServerIp = ConfigHelp.GetConfigValue("smtptype");   //使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
                    string toMailAddress = email;              //要发送对象的邮箱
                    string fromMailAddress = ConfigHelp.GetConfigValue("useraddress");//你的邮箱
                    string subjectInfo = "审核通过消息";                  //主题
                    string bodyInfo = "<p>" + "注册已通过审核，请前往“登陆”页面进行登陆" + "</p>";//以Html格式发送的邮件
                    string mailUsername = ConfigHelp.GetConfigValue("user");              //登录邮箱的用户名
                    string mailPassword = ConfigHelp.GetConfigValue("pass"); //对应的登录邮箱的第三方密码（你的邮箱不论是163还是qq邮箱，都需要自行开通stmp服务）
                    string mailPort = "25";                      //发送邮箱的端口号
                    //string attachPath = "E:\\123123.txt; E:\\haha.pdf";

                    //创建发送邮箱的对象
                    Email myEmail = new Email(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, true, false);

                    if (myEmail.Send())
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content("false");
                    }                                  
                }
                else return Content("false");
            }
            catch { return Content("false"); }
        }
        [HttpPost]
        public ContentResult UserDel(int userId) //用户删除，当学生或教师离开实验室，可以将其有关信息删除
        {
            try
            {
                bool success = UserManService.UserDel(userId);
                if (success) return Content("true");
                else return Content("false");
            }
            catch { return Content("false"); }
        } 

        public IRegisterService registerService { get; set; }
        public ActionResult Add(bool flag)
        {
            if (flag) ViewBag.aMessage = "添加成功";

            List<RoomDto> list = new List<RoomDto>();
            list = roomService.GetAll();            
            SelectList selList = new SelectList(list, "ID", "Name");
            ViewData["Position"] = selList; 

            return View();
        }
        [HttpPost]
        public ActionResult Add(RegisterDto UserInfo) //添加用户，目前只是添加教师
        {     
            try
            { 
                //throw new Exception("用户自定义异常~~~~~");  
                //后台检查UserName实体中的所有属性是否合法  
                //使用ModelState的要求：传入给add方法的参数属性上必须要有特性标签  
                if (ModelState.IsValid == false)
                {
                    //如果ModelState.IsValid == false就表示UserInfo这个实体类对象中贴了特性标签的所有属性中至少有一个属性验证不通过（它会遍历UserInfo这个类对象的所有属性，检查属性上面的特性标签，然后判断用户输入的数据是否符合特性标签的约束条件，如果不符合就返回了fase）  

                    //加入程序员自定义的提示信息  
                    ModelState.AddModelError("", "实体验证不合法");

                    List<RoomDto> list = new List<RoomDto>();
                    list = roomService.GetAll();

                    SelectList selList = new SelectList(list, "ID", "Name");
                    ViewData["Position"] = selList;  
                    return View(UserInfo); //数据验证不通过后，重新跳到新增页面。  
                }

                //.............数据验证全部通过。这里则是将模型保存到数据库的操作  
                bool InsertCount = registerService.InsertInfo(UserInfo);
                if (InsertCount)
                {
                    //return Content("<script>alert('添加成功！');window.location.href='UserManage';</script>");
                    ViewBag.AddMessage = "添加成功！";
                }
                else
                {
                    //return Content("<script>alert('添加失败！');window.location.href='Add';</script>");
                    ViewBag.AddMessage = "添加失败！";
                }
            }
            catch(Exception ex)
            {
                //将异常的简述告诉用户（在视图页面用@Html.ValidationSummary(true)来显显示异常错误的值）  
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectPermanent("/UserManage/Add?flag=true");
        }
        public ActionResult Change(string name, bool flag) //修改用户信息
        {
            try
            {
                List<RegisterDto> oneList = new List<RegisterDto>();
                oneList = registerService.GetNameInfo(name);
                var List = oneList.FirstOrDefault();
                ViewBag.Name = List.Name;
                ViewBag.Real_Name = List.Real_Name;
                ViewBag.Phone = List.Phone.TrimEnd();
                ViewBag.Email = List.Email;
                ViewBag.Password = List.Password;
                ViewBag.confirm = List.confirm;
                ViewBag.ID = List.ID;
                ViewBag.Room_Name = List.Room_Name;
                ViewBag.Room_ID = List.Room_ID;

                if (flag) ViewBag.cMessage = "修改成功！";

                List<RoomDto> list = new List<RoomDto>();
                list = roomService.GetAll();
                TempData["list"] = list;
                //SelectList selList = new SelectList(list, "ID", "Name", List.Room_Name);
                //ViewData["Users"] = selList; 
            }
            catch { }
            return View();
        }
        [HttpPost]
        public ActionResult Change(RegisterDto UserInfo)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    //如果ModelState.IsValid == false就表示UserInfo这个实体类对象中贴了特性标签的所有属性中至少有一个属性验证不通过（它会遍历UserInfo这个类对象的所有属性，检查属性上面的特性标签，然后判断用户输入的数据是否符合特性标签的约束条件，如果不符合就返回了fase）  

                    //加入程序员自定义的提示信息  
                    ModelState.AddModelError("", "实体验证不合法");

                    //List<RoomDto> list = new List<RoomDto>();
                    //list = roomService.GetAll();
                    //SelectList selList = new SelectList(list, "ID", "Name", UserInfo.Room_Name);
                    //ViewData["Users"] = selList; 
                    return View(UserInfo); //数据验证不通过后，重新跳到新增页面。  
                }

                bool Update = UserManService.UpdateRole(UserInfo);
                if (Update)
                {
                    //return Content("<script>alert('修改成功！');window.location.href='UserManage';</script>");
                    ViewBag.ChaMessage = "修改成功！";
                }
                else
                {
                    //return Content("<script>alert('修改失败！');window.location.href='Change';</script>");
                    ViewBag.ChaMessage = "修改失败！";
                }
            }
            catch
            {
            }
            return RedirectPermanent("/UserManage/Change?name=" + UserInfo.Name + "&flag=true");
        }       
	}
}
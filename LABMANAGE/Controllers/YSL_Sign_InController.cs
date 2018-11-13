using LABMANAGE.Data;
using LABMANAGE.Service.ysl_Sign_In;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using LABMANAGE.UntityCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LABMANAGE.Controllers
{
    public class YSL_Sign_InController : Controller
    {
        public ISign_InService ISIS { get; set; }

        public IUserDataService IUDS { get; set; }

        public IReadExcelService IRES { get; set; }

        public IRoomNameService IRNS { get; set; }

        //
        // GET: /ysl_Sign_In/
        public ActionResult Sign_Show()
        {
            try
            {
                int UID = int.Parse(LoginBase.ID);
                List<User_name_uidDto> all_user = IUDS.getUser();
                this.TempData["list"] = all_user;
                this.TempData["user"] = LoginBase.RoleCode;
                return View();
            }
            catch
            {
                return View("../Login/Login");
            }
            
        }
        //获取自己的签到信息
        #region 获取默认（本人）签到信息
        public JsonResult Sign_json(string text)
        {
            int UID = int.Parse(LoginBase.ID);

            List<Sign_dateModel> JsonData = ISIS.Get_data(UID,1);//1web 2考勤机

            return Json(ISIS.Obj2Json(JsonData), JsonRequestBehavior.AllowGet);
        }
        #endregion

        //查看其它用户签到情况
        #region 获取用户签到信息（限学生）
        public JsonResult changeuser(string text)
        {
            int UID = int.Parse(Request.Form[0].ToString());
            int type = int.Parse(Request.Form[1].ToString());

            
            List<Sign_dateModel> JsonData = ISIS.Get_data(UID,type);

            return Json(ISIS.Obj2Json(JsonData), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 判断当前用户是否签到  && 是否可以签到
        public JsonResult Is_Sign(string text)
        {
            //不可签到时间段
            string _Day1 = "11:30";
            string Day1 = "12:30";
            string _Day2 = "17:30";
            string Day2 = "18:00";
            string _Day3 = "20:30";
            string Day3 = "22:00";
            TimeSpan _Day1_time = DateTime.Parse(_Day1).TimeOfDay;
            TimeSpan Day1_time = DateTime.Parse(Day1).TimeOfDay;
            TimeSpan _Day2_time = DateTime.Parse(_Day2).TimeOfDay;
            TimeSpan Day2_time = DateTime.Parse(Day2).TimeOfDay;
            TimeSpan _Day3_time = DateTime.Parse(_Day3).TimeOfDay;
            TimeSpan Day3_time = DateTime.Parse(Day3).TimeOfDay;
            DateTime t1 = DateTime.Now;
            TimeSpan dspNow = t1.TimeOfDay;
            //if ((dspNow > _Day1_time && dspNow < Day1_time) || (dspNow > _Day2_time && dspNow < Day2_time) || (dspNow > _Day3_time && dspNow < Day3_time))//不可签到
            //{
            //    return Json("错误时间", JsonRequestBehavior.AllowGet);
            //}
            
            int UID = 1 ;
            try
            {
                UID = int.Parse(LoginBase.ID);

                bool IsSign = ISIS.Is_Sign_In(UID);

                if (IsSign)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet); ;
            }
        }
        #endregion

        #region 用户签到
        public JsonResult User_Sign(string text)
        {
            int UID = int.Parse(LoginBase.ID);
            //
            //IP属性目前没有加，等待后续更改
            //
            //string IP = LoginBase.IP;
            string IP = "";
            bool flag = ISIS.userSign(UID, IP);
            return Json(flag, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 考勤数据导入

        [HttpPost]
        public string upFile(HttpPostedFileBase file)
        {
            string value = "";
            if(file==null)
            {
                value = "文件不能为空";
                return value;
            }
            if (string.Empty.Equals(file.FileName) || ".xlsx" != Path.GetExtension(file.FileName))
            {
                //throw new ArgumentException("当前文件格式不正确,请确保正确的Excel文件格式!");
                value = "当前文件格式不正确,请确保正确的Excel文件格式!";
                return value;
            }

            var severPath = this.Server.MapPath("/Excelfiles/"); //获取当前虚拟文件路径

            var savePath = Path.Combine(severPath, file.FileName); //拼接保存文件路径

            try
            {
                file.SaveAs(savePath);
                string name = IRES.inputdata(savePath);
                if(name==null)
                {
                    value = "未能成功导入，请检查是否文件有误";
                }
                else
                {
                    value = name + "导入成功";
                }
                
            }
            catch (Exception e)
            {
                //value = "Excel导入错误("+e+")，请联系管理员--------------kylin";
                value = "Excel导入错误，请确认上传的文件无误";
            }
            return value;
        }
        #endregion

        #region 获取树形菜单的信息
        public JsonResult get_tree(string text)
        {
            //object o = new object();
            string tree = "[{     'name': 'C#实验室'    ,'children': [{      'name': '邓春' ,'isuser':'1'    }]  }, {    'name': 'java实验室'    ,'children': [{      'name': '伊世林'      ,'isuser':'1'  }, {      'name': '王宇浩' ,'isuser':'1'    }]  }] ";
            List<Room> roomvalue = IRNS.Rname();
            string Tree = "[";
            for (int i = 0; i < roomvalue.Count(); i++)
            {
                string roomname = roomvalue[i].Name;
                if(i!=0)
                {
                    Tree += ",  ";
                }
                Tree += "{  'name':'" + roomname + "'  ,'children': [";
                int usercount = roomvalue[i].User.Count();
                List<User> user = roomvalue[i].User.ToList();
                bool flag = false;
                for(int j = 0; j<usercount; j++)
                {
                    if (user[j].U_Role != 3 || user[j].IsExamine==false)
                    {
                        continue;
                    }
                    
                    string uname = user[j].Real_Name;
                    string isuser = "1";
                    int uid = user[j].ID;

                    if(j!=0 && flag)
                    {
                        Tree += ",  ";
                    }
                    Tree += "{    'name': '" + uname + "' ,'isuser':'" + isuser + "' ,'UID':'" + uid + "'  }";

                    flag = true;
                }
                Tree += "]}";
            }
            Tree += "]";

            return Json(Tree);
        }

        #endregion

    }
}
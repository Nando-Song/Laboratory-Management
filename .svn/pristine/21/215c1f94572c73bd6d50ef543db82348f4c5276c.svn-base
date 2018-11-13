using LABMANAGE.Service.Sum;
using LABMANAGE.Service.Sum.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LABMANAGE.Unitity;
using Newtonsoft.Json;
using System.Web.Services;
using LABMANAGE.UntityCode;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using System.Web.Configuration;
using System.IO;
using System.Text;
using LABMANAGE.Service.Rooms;
using LABMANAGE.Service.Rooms.Dto;
namespace LABMANAGE.Controllers
{
    public class SummaryController : Controller
    {
        public ISumService SumService { get; set; }
        public IRoomService RoomService { get; set; }

        public ActionResult Index()
        {
            List<RoomDto> roomList = new List<RoomDto>();
            roomList = RoomService.GetAll();
            TempData["roomList"] = roomList;
            return View();
        }
        [WebMethod]
        public JsonResult GetSumList(string nickName, string nickTime, int curPage, string roomID)
        //public JsonResult GetSumList(string nickName, string nickTime, int curPage)
        {
            long recordCount = 0;
            int pageSize = Convert.ToInt32(ConfigHelp.GetConfigValue("pageSize"));
            List<SumBaseDto> sumList = SumService.GetSumList(nickName.Trim(), nickTime, curPage, pageSize, roomID, out recordCount);
            //List<SumBaseDto> sumList = SumService.GetSumList(nickName.Trim(), nickTime, curPage, pageSize, out recordCount);
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = recordCount, pageSize = pageSize, lists = sumList });
            return Json(jsonStr);
        }
      
        public ActionResult EvalSum(int Id, bool flag)
        {
            if (flag == true)
                ViewBag.Msgs = "提交成功！";
            ViewBag.ID = Id;
            string oldEval = SumService.GetOne(Id);
            string eval = SumService.HtmlEntitiesEncode(oldEval);
            eval = HttpUtility.HtmlDecode(eval);
            ViewBag.oldEval = eval.Replace("\r\n", "<br/>");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SumBaseDto summary)
        {
            bool isSuccess = SumService.Update(summary);
            if (isSuccess == true)
            {
                ViewBag.Msgs = "提交成功！";
            }
            else
            {
                ViewBag.Msgs = "提交失败！";
            }

            //return View("EvalSubmit");
            return RedirectPermanent("/Summary/EvalSum?Id=" + summary.TID + "&flag=true");
        }

        //个人总结
        public ActionResult Person(int id)
        {
            ViewBag.Msg = "";
            ViewBag.PersonId = id;
            return View();
        }
        [WebMethod]
        public JsonResult GetSumPer(string nickTime, int curPage, int id)
        {
            long recordCount = 0;
            int pageSize = Convert.ToInt32(ConfigHelp.GetConfigValue("pageSize"));
            List<SumBaseDto> sumList = SumService.GetSumPer(nickTime, curPage, pageSize, id, out recordCount);
            string jsonStr = JsonConvert.SerializeObject(new { recordCount = recordCount, pageSize = pageSize, lists = sumList });
            return Json(jsonStr);
        }

        //添加总结
        public ActionResult AddSum(int Id, bool flag)
        {
            if (flag == true)
                ViewBag.Mssg = "添加成功！";
            return View();
        }

        [HttpPost]
        public ActionResult Person(SumBaseDto summary)
        {
            bool isSuccess = SumService.InsertSum(summary);
            if (isSuccess == true)
            {
                ViewBag.msg = "添加成功!";
            }
            else
            {
                ViewBag.msg = "添加失败!";
            }
            return RedirectPermanent("/Summary/AddSum?id=" + Convert.ToInt32(LoginBase.ID) + "&flag=true");
        }

        //导出到excell
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="nickTime"></param>
        public FileResult Export(string nickName, string nickTime)
        {
            //try
            //{
            List<SumBaseDto> SumSheet = SumService.GetAll(nickName, nickTime);
            HSSFWorkbook book = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPTO SDK Example";
            si.Title = "学习或工作总结表";
            book.DocumentSummaryInformation = dsi;
            book.SummaryInformation = si;
            ISheet sheet = book.CreateSheet("sheet1");

            IRow row0 = sheet.CreateRow(0);
            row0.CreateCell(0).SetCellValue("用户ID");
            row0.CreateCell(1).SetCellValue("姓名");
            row0.CreateCell(2).SetCellValue("学习/工作内容");
            row0.CreateCell(3).SetCellValue("学习/工作情况简介");
            row0.CreateCell(4).SetCellValue("遇到的问题");
            row0.CreateCell(5).SetCellValue("是否解决");
            row0.CreateCell(6).SetCellValue("教师回复");
            row0.CreateCell(7).SetCellValue("总结时间");

            for (int i = 0; i < SumSheet.Count; i++)
            {
                IRow rowtemp = sheet.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(SumSheet[i].User_ID);
                rowtemp.CreateCell(1).SetCellValue(SumSheet[i].Real_Name);
                rowtemp.CreateCell(2).SetCellValue(SumSheet[i].Title);
                rowtemp.CreateCell(3).SetCellValue(SumSheet[i].Progress);

                if (SumSheet[i].Problem == null)
                {
                    rowtemp.CreateCell(4).SetCellValue("无");
                    rowtemp.CreateCell(5).SetCellValue("");
                }
                else
                {
                    rowtemp.CreateCell(4).SetCellValue(SumSheet[i].Problem);
                    if (SumSheet[i].IS_Solve == 0)
                        rowtemp.CreateCell(5).SetCellValue("已解决");
                    if (SumSheet[i].IS_Solve == 1)
                        rowtemp.CreateCell(5).SetCellValue("尚未解决");
                }
                rowtemp.CreateCell(6).SetCellValue(SumSheet[i].Teacher_evaluation);
                rowtemp.CreateCell(7).SetCellValue(SumSheet[i].Time.ToString());
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);

            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "学习或工作总结" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
            //String fileName = "学习或工作总结.xls ";
            //return View();
        }
	}
}
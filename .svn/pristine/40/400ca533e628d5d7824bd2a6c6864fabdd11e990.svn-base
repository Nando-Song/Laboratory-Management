using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.ysl_Sign_In
{
    public class ReadExcelService : IReadExcelService
    {
        public ISign_InService ISIS { get; set; }
        public IUserDataService IUDS { get; set; }


        public string inputdata(string path)
        {
            string user = LoadDataFromExcel(path);
            return user;
        }

        #region 存储数据
        public string LoadDataFromExcel(string Path)
        {
            string success_name = null;

            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + Path + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            
            //获取成员信息;
            DataTable dt1 = new DataTable();
            dt1 = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt1 == null)
            {
                return null;
            }
            String[] excelSheets = new String[dt1.Rows.Count];
            int i = 0;
            // 加入工作表名称到字符串数组 
            foreach (DataRow row in dt1.Rows)
            {
                string strSheetTableName = row["TABLE_NAME"].ToString();
                //过滤无效SheetName
                if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$"))
                {
                    excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                }
                i++;
            }

            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataTable dt = null;

            for(i = 0; i<dt1.Rows.Count; i++)
            {
                try
                {
                    strExcel = "select * from [" + excelSheets[i] + "$]";
                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    dt = new DataTable();
                    myCommand.Fill(dt);

                    string month = dt.Rows[0][16].ToString();
                    string name = excelSheets[i].Split('_')[0].ToString();
                    int uid = IUDS.get_uid(name);
                    for (int j = 1; j <= 16; j++)//前16天
                    {
                        string day_table = dt.Rows[j + 7][0].ToString().Split(' ')[0];
                        string day = month + "-" + day_table + " ";
                        string time_1 = dt.Rows[j + 7][1].ToString();
                        if (time_1 != null && time_1 != "")
                        {
                            string time = day + time_1;
                            ISIS.Ecxel_add(time, uid);
                        }
                        string time_2 = dt.Rows[j + 7][3].ToString();
                        if (time_2 != null && time_2 != "")
                        {
                            string time = day + time_2;
                            ISIS.Ecxel_add(time, uid);
                        }
                        string time_3 = dt.Rows[j + 7][5].ToString();
                        if (time_3 != null && time_3 != "")
                        {
                            string time = day + time_3;
                            ISIS.Ecxel_add(time, uid);
                        }
                    }
                    for (int j = 17; j <= 31; j++)//后16天
                    {
                        string day_table = dt.Rows[j - 9][9].ToString().Split(' ')[0];
                        string day = month + "-" + day_table + " ";
                        string time_1 = dt.Rows[j - 9][10].ToString();
                        if (time_1 != null && time_1 != "")
                        {
                            string time = day + time_1;
                            ISIS.Ecxel_add(time, uid);
                        }
                        string time_2 = dt.Rows[j - 9][12].ToString();
                        if (time_2 != null && time_2 != "")
                        {
                            string time = day + time_2;
                            ISIS.Ecxel_add(time, uid);
                        }
                        string time_3 = dt.Rows[j - 9][14].ToString();
                        if (time_3 != null && time_3 != "")
                        {
                            string time = day + time_3;
                            ISIS.Ecxel_add(time, uid);
                        }
                    }

                    if(success_name!=null)
                    {
                        success_name += "," + name;
                    }
                    else
                    {
                        success_name +=  name;

                    }
                    
                }//try
                catch{ }
            }
            return success_name;
        }

        #endregion
    }
}
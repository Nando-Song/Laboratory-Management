using LABMANAGE.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LABMANAGE.Service.Sum.Dto
{
    public class SumBaseDto
    {
        public int TID { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int User_ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Real_Name { get; set; }
        /// <summary>
        /// 标题（学习内容）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 学习进度
        /// </summary>
        public string Progress { get; set; }
        /// <summary>
        /// 遇到的问题
        /// </summary>
        public string Problem { get; set; }
        /// <summary>
        /// 是否解决
        /// </summary>
        public int IS_Solve { get; set; }
        /// <summary>
        /// 添加总结的时间
        /// </summary>
        public DateTime Time { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        /// <summary>
        /// 教师评价
        /// </summary>
        //[ConcurrencyCheck]
        [Timestamp]
        public string Teacher_evaluation { get; set; }
        //[JsonIgnore] 
        //[IgnoreDataMember] 
        //public User User { get; set; }


        /// <summary>
        /// 当前页
        /// </summary>
        //public string curpage { get; set; }
        ///// <summary>
        ///// 每页数据条数
        ///// </summary>
        //public string pageSize { get; set; }
        ///// <summary>
        ///// 数据总条数
        ///// </summary>
        //public string Count { get; set; }
        public string nickName { get; set; }
        public DateTime? nickTimes { get; set; }
        public DateTime? nickTime { get; set; }
    }
}
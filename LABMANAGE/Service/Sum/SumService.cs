﻿using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Sum.Dto;
using LABMANAGE.UntityCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LABMANAGE.Service.Sum
{
    public class SumService : ISumService
    {
        public IQQInvRepository<Summary> SumRepository;
        public SumService(IQQInvRepository<Summary> _SumRepository)
        {
            SumRepository = _SumRepository;
        }

        public List<SumBaseDto> GetSumList(string nickName, string nickTimes, int curPage, int PageSize, string roomID, out long recordCount)
        //public List<SumBaseDto> GetSumList(string nickName, string nickTimes, int curPage, int PageSize, out long recordCount)
        {
            var query = SumRepository.Query();
            if (!String.IsNullOrEmpty(nickName))
            {
                query = query.Where(m => m.User.Real_Name.Contains(nickName));
            }
            if (!String.IsNullOrEmpty(nickTimes))
            {
                string StartTime = nickTimes.Split('-')[0];
                DateTime startTime = Convert.ToDateTime(StartTime);
                string EndTime = nickTimes.Split('-')[1];
                DateTime endTime = Convert.ToDateTime(EndTime);
                query = query.Where(m=>m.Time >= startTime && m.Time <= endTime);
            }
            if (!String.IsNullOrEmpty(roomID))
            {
                query = query.Where(m => m.User.Room_ID.ToString() == roomID);
            }
            recordCount = query.Count();
            query = query.OrderByDescending(m => m.Time).Skip((curPage - 1) * PageSize).Take(PageSize);

            List<SumBaseDto> SumList = query.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Summary, SumBaseDto>(c));

            return SumList;
        }
        public List<SumBaseDto> GetAll(string nickName, string nickTime)
        {
            var query = SumRepository.Query();//.OrderByDescending(a => a.Time);
            if(!String.IsNullOrEmpty(nickName))
            {
                query = query.Where(m => m.User.Real_Name.Contains(nickName));
            }

            if(!String.IsNullOrEmpty(nickTime))
            {
                string StartTime = nickTime.Split('-')[0];
                DateTime startTime = Convert.ToDateTime(StartTime);
                string EndTime = nickTime.Split('-')[1];
                DateTime endTime = Convert.ToDateTime(EndTime);
                query = query.Where(m => m.Time >= startTime && m.Time <= endTime);
            }
            query = query.OrderByDescending(a=>a.Time);
            List<SumBaseDto> SumList = query.ToList().ConvertAll(c=>AutoMapperHelp.ConvertToDto<Summary, SumBaseDto>(c));

            return SumList;
        }
        public bool InsertSum(SumBaseDto summary)
        {
            try
            {
                Summary list = AutoMapperHelp.ConvertModel<Summary, SumBaseDto>(summary);
                var date = DateTime.Now;
                int dayweek = (int)date.DayOfWeek;
                if (dayweek == 0) dayweek = 7;
                DateTime startTime = date.AddDays(-(dayweek));
                DateTime endTime = date.AddDays(7 - dayweek);

                DateTime time = date;

                list.User_ID = Convert.ToInt32(LoginBase.ID);
                list.Time = time;
                list.Start_Time = startTime;
                list.End_Time = endTime;
                Summary sumDt = SumRepository.Add(list);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(SumBaseDto summary)
        {
            try
            {
                Summary evaSum = SumRepository.Get(summary.TID);
                //evaSum.Teacher_evaluation = summary.Teacher_evaluation;
                evaSum.Teacher_evaluation = System.Text.RegularExpressions.Regex.Replace(summary.Teacher_evaluation, @"<[^>]*>", "");
                SumRepository.Update(evaSum);
                return true;
            }
            catch
            {
                return false;
            }
            
            //var list = SumRepository.Query().Where(m => m.ID == id).ToList();
            //List<SumBaseDto> SumList = list.ConvertAll(c=>AutoMapperHelp.ConvertToDto<Summary, SumBaseDto>(c));
            ////RegisterDto tt = infoList.FirstOrDefault();
            ////User ss = AutoMapperHelp.ConvertModel<User, RegisterDto>(tt);
            //SumBaseDto sumEva = SumList.FirstOrDefault();
            //sumEva.Teacher_evaluation = summary.Teacher_evaluation;
            //Summary evaList = AutoMapperHelp.ConvertModel<Summary, SumBaseDto>(sumEva);
            //Summary sumDt = SumRepository.Update(evaList);
        }

        public string GetOne(int id)
        {
            Summary oneSum = SumRepository.Get(id);
            string oldEval = "";
            if (oneSum.Teacher_evaluation != null)
                oldEval = oneSum.Teacher_evaluation.ToString();
                 //oldEval = System.Text.RegularExpressions.Regex.Replace(oneSum.Teacher_evaluation, @"<[^>]*>", "");
            return oldEval;
        }
        
        public List<SumBaseDto>GetSumPer(string nickTime, int curPage, int PageSize, int id, out long recordCount)
        {
            var query = SumRepository.Query().Where(m=>m.User_ID == id);
            if (!String.IsNullOrEmpty(nickTime))
            {
                string StartTime = nickTime.Split('-')[0];
                DateTime startTime = Convert.ToDateTime(StartTime);
                string EndTime = nickTime.Split('-')[1];
                DateTime endTime = Convert.ToDateTime(EndTime);
                query = query.Where(m => m.Time >= startTime && m.Time <= endTime);
            }
            recordCount = query.Count();
            query = query.OrderByDescending(m => m.Time).Skip((curPage - 1) * PageSize).Take(PageSize);

            List<SumBaseDto> SumList = query.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Summary, SumBaseDto>(c));

            return SumList;
        }

        /// <summary>
        /// 转义字符 
        /// </summary>
        /// <param name="text">需要转换的html文本</param>
        /// <returns>HTMLEntities编码后的文本</returns>
        public string HtmlEntitiesEncode(string text)
        {
            // 获取文本字符数组
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();

            // 初始化输出结果
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));
            foreach(char c in chars)
            {
                // 将指定的 Unicode 字符的值转换为等效的 32 位有符号整数
                int value = Convert.ToInt32(c);

                // 内码为127以下的字符为标准ASCII编码，不需要转换，否则做 &#{数字}; 方式转换
                if(value > 127)
                {
                    result.AppendFormat("&#{0};", value);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
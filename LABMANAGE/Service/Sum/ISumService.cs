﻿using LABMANAGE.Service.Sum.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Sum
{
    public interface ISumService
    {
        List<SumBaseDto> GetAll(string nickName, string nickTime);
        List<SumBaseDto> GetSumList(string nickName, string nickTime, int curPage, int PageSize, string roomID, out long recordCount);
        //List<SumBaseDto> GetSumList(string nickName, string nickTime, int curPage, int PageSize, out long recordCount);
        bool InsertSum(SumBaseDto summary);

        bool Update(SumBaseDto summary);

        string GetOne(int id);
        List<SumBaseDto> GetSumPer(string nickTime, int curPage, int PageSize, int id, out long recordCount);

        string HtmlEntitiesEncode(string text);
    }
}
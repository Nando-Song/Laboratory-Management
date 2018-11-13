using LABMANAGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.WYH.Dto
{
    public class NoticeDto
    {

        public int User_ID { get; set; }
        public System.DateTime Time { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

    }
}
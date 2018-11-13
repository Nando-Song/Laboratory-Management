using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.ysl_Sign_In.Dto
{
    public class AttendanceDto
    {
        public int User_ID { get; set; }
        public int State { get; set; }//签到状态包括：正常1，迟到2，请假3
        public int Shift { get; set; }//签到次序：早上1，下午2，晚上3
        public DateTime Time { get; set; }
        public int Type { get; set; }//签到模式：网站签到1，指纹机签到2
    }
}
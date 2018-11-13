using LABMANAGE.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.leave.Dto
{
    public class LeaveBaseDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        //真实姓名
        public string Real_Name { get; set;}
        
        //申请时间
        public System.DateTime Time { get; set; }
        
        //假期开始时间
        public System.DateTime Start_Time { get; set; }
        //假期结束时间
        public System.DateTime End_Time { get; set; }
        //请假原因
        [DisplayName("请假原因")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "请假原因不能为空")]
        public string Reason { get; set; }
        //是否审核
        public int IsExamine { get; set; }
        //是否通过
        public int Pass { get; set; }
        //审核人ID
        public int Verify_ID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Equip.Dto
{
    public class EquipBaseDto
    {
        public int ID { get; set; }
        //真实姓名
        public string Real_Name { get; set; }
        public int User_ID { get; set; }
        //申请时间
        public DateTime Time { get; set; }
        //设备名称
        [DisplayName("设备名称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "设备名称不能为空")]
        public string Equipment_Name { get; set; }
        //申请数量
        [Display(Name = "申请数量")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "数量必须为数字")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "申请数量不能为空")]
        public string Number { get; set; }

        //是否审核
        public int IsExamine { get; set; }
        //是否通过
        public int Pass { get; set; }
        //审核人ID
        public int Verify_ID { get; set; }



    }
}
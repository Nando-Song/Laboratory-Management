using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LABMANAGE.Service.Register.Dto
{
    public class RegisterDto
    {
        [DisplayName("昵称")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "昵称不能为空")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "昵称的长度应该在1到20位之间")]
        [RegularExpression("^(\\w|-|[\u4E00-\u9FA5])*$", ErrorMessage = "昵称仅支持中英文、数字和下划线")] //正则表达式          
        [Remote("CheckName", "Register", HttpMethod = "get", AdditionalFields = "ID", ErrorMessage = "该昵称已经被注册过或者已有账号？")] //异步请求数据。到时候会将以UserName为key,UserName的值为value传递到Home控制下的CheckUserName方法中。在CheckUserName方法中只要用Request.Form["UserName"]就能取到UserName的值。注意CheckUserName方法的返回值只能是“false”或者"true"  
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "真实姓名不能为空")]
        public string Real_Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "邮箱不能为空")]
        [RegularExpression("[a-zA-Z0-9_\\-\\.]+@[a-zA-Z0-9]+(\\.(com))", ErrorMessage = "邮箱的格式不正确")]
        [Remote("CheckEmail", "Register", HttpMethod = "get", AdditionalFields = "ID", ErrorMessage = "该邮箱已经被注册过或者已有账号？")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码不能为空")]
        [RegularExpression("^1[34578]\\d{9}$", ErrorMessage = "手机号码格式不正确")]
        [Remote("CheckPhone", "Register", HttpMethod = "get", AdditionalFields = "ID", ErrorMessage = "该手机号已经被注册过或者已有账号？")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [RegularExpression("^[a-zA-Z]\\w{5,18}$", ErrorMessage = "密码以字母开头，长度在6-18之间，只能包含字符、数字和下划线")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "密码不一致")]  
        public string confirm{ get; set; }

        public string message { get; set; }

        public int U_Role { get; set; }

        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择实验室")]

        public int Room_ID { get; set; }

        public string Room_Name { get; set; }
    }
}
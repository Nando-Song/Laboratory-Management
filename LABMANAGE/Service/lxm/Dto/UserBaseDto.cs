﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.lxm.Dto
{
    public class UserBaseDto
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Real_Name { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int U_Role { get; set; }
        /// <summary>
        /// 头像位置
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 座右铭
        /// </summary>
        public string Motto { get; set; }
        /// <summary>
        /// 是否审核通过
        /// </summary>
        public bool IsExamine { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Register_Time { get; set; }
    }
}
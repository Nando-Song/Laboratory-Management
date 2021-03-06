﻿using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Login.Dto;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.ysl_Sign_In
{
    public class UserDataService : IUserDataService
    {

        public IQQInvRepository<User> tableUser;
        public UserDataService(IQQInvRepository<User> _tableUser)
        {
            tableUser = _tableUser;
        }

        #region 获取所有用户ID
        public List<User_name_uidDto> getUser()
        {
            var list = tableUser.Query().Where(c => c.U_Role == 3 && c.IsExamine==true);//获取所有学生的信息
            List<User_name_uidDto> userList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, User_name_uidDto>(c));
            return userList;
        }

        #endregion

        #region 获取用户ID
        public int get_uid(string name)
        {
            var list = tableUser.Query().Where(c => c.Real_Name == name && c.U_Role == 3);//获取学生的信息
            List<User_name_uidDto> userList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, User_name_uidDto>(c));
            return userList[0].ID;
        }

        #endregion
    }
}
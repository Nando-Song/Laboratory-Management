﻿using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Register.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Register
{
    public class RegisterService : IRegisterService
    {
        public IQQInvRepository<User> Information; 
        public RegisterService(IQQInvRepository<User> _information)
        {
            Information = _information;
        }

        public List<RegisterDto> GetNameInfo(string Name)
        {
            var list = Information.Query().Where(m=>m.Name == Name);
            List<RegisterDto> infoList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, RegisterDto>(c));

            return infoList;
        }

        public List<RegisterDto> GetPhoneInfo(string Phone)
        {
            var list = Information.Query().Where(m => m.Phone == Phone);
            List<RegisterDto> infoList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, RegisterDto>(c));

            return infoList;
        }

        public List<RegisterDto> GetEmailInfo(string Email)
        {
            var list = Information.Query().Where(m => m.Email == Email);
            List<RegisterDto> infoList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<User, RegisterDto>(c));

            return infoList;
        }

        public bool InsertInfo(RegisterDto UserInfo)
        {
            try
            {
                User uList = AutoMapperHelp.ConvertModel<User, RegisterDto>(UserInfo);
                //var roomList = room.Query().Where(m=>m.Name == UserInfo.Room_Name);
                uList.Room_ID = UserInfo.Room_ID;
                uList.Image = "../../images/user.jpg";
                uList.Motto = null;
                if (UserInfo.U_Role == 2) uList.IsExamine = true;
                else uList.IsExamine = false;
                uList.IP = null;
                uList.Register_Time = DateTime.Now;
                User insertList = Information.Add(uList);
                //int insertCount = insertList.Equipment.Count();
                //return insertCount;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<RegisterDto> GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
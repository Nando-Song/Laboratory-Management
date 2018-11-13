using AutoMapper;
using LABMANAGE.Data;
//using LABMANAGE.Service.Login.Dto;
using LABMANAGE.Service.QQ.Dto;
using LABMANAGE.Service.WYH.Dto;
using LABMANAGE.Service.Sum.Dto;
using LABMANAGE.Service.Register.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LABMANAGE.Service.Login.Dto;
using LABMANAGE.ViewModel;
using LABMANAGE.Service.UserManage.Dto;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using LABMANAGE.ViewModel;
using LABMANAGE.Service.ysl_Sign_In.Dto;
using LABMANAGE.Service.Login.Dto;
using LABMANAGE.Service.leave.Dto;
using LABMANAGE.Service.Equip.Dto;
using LABMANAGE.Service.UserManage.Dto;
using LABMANAGE.Service.Rooms.Dto;

namespace LABMANAGE.Service
{
    public class AutoMapperHelp
    {
        /// <summary>
        /// AutoMaper配置
        /// </summary>
        public static MapperConfiguration AutoMaperConfig = new MapperConfiguration(cfg =>
        {
            //cfg.CreateMap<QQBaseDto, QQINFO>();
            //cfg.CreateMap<QQINFO, QQBaseDto>();
            cfg.CreateMap<Notice, NoticeDto>();
            cfg.CreateMap<NoticeDto, Notice>();
            
            cfg.CreateMap<Duty, DutyDto>();
            cfg.CreateMap<DutyDto, Duty>();
            
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<UserDto, User>();
            
            cfg.CreateMap<SumBaseDto, Summary>();
            cfg.CreateMap<Summary, SumBaseDto>().ForMember(dest => dest.Real_Name,options =>options.MapFrom(src => src.User.Real_Name));
            
            cfg.CreateMap<RegisterDto, User>();
            cfg.CreateMap<User, RegisterDto>();

            cfg.CreateMap<UserManDto, User>(); 
            cfg.CreateMap<User, UserManDto>().ForMember(dest => dest.Room_Name, options => options.MapFrom(src => src.Room.Name));
            
            cfg.CreateMap<EUserBaseDto, User>();
            cfg.CreateMap<User, EUserBaseDto>();

            cfg.CreateMap<RoomDto, Room>();
            cfg.CreateMap<Room, RoomDto>();

            cfg.CreateMap<LoginBaseDto, User>();
            cfg.CreateMap<User, LoginBaseDto>().ForMember(m =>m.Code, n =>n.MapFrom(src => src.Role.Code));

            cfg.CreateMap<Menu, MenuModel>();
            cfg.CreateMap<MenuModel, Menu>();

            cfg.CreateMap<Attendance, AttendanceDto>();
            cfg.CreateMap<AttendanceDto, Attendance>();

             cfg.CreateMap<User, User_name_uidDto>();
            cfg.CreateMap<User_name_uidDto, User>();

            cfg.CreateMap<Leave, LeaveBaseDto>();
            cfg.CreateMap<LeaveBaseDto, Leave>();

            cfg.CreateMap<Equipment, EquipBaseDto>();
            cfg.CreateMap<EquipBaseDto, Equipment>();

            cfg.CreateMap<User, LABMANAGE.Service.lxm.Dto.UserBaseDto>();
            cfg.CreateMap<LABMANAGE.Service.lxm.Dto.UserBaseDto, User>();

            cfg.CreateMap<User,LABMANAGE.Service.leave.Dto.UserBaseDto>();
            cfg.CreateMap<LABMANAGE.Service.leave.Dto.UserBaseDto, User>();
        });
        public static IMapper AutoMaper = AutoMaperConfig.CreateMapper();

        /// <summary>
        /// 转换为数据模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T ConvertModel<T,S>(S result)
        {
            return AutoMaper.Map<T>(result);
        }

        /// <summary>
        /// 根据视图模型赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="result"></param>
        public static void CloneModel<T,S>(T model, S result)
        {
            AutoMaper.Map<T, S>(model, result);
        }


        /// <summary>
        /// 转换为视图模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static S ConvertToDto<T,S>(T model)
        {
            return AutoMaper.Map<S>(model);
        }
    }
}
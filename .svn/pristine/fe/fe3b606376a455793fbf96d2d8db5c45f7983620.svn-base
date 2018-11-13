using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service;
using LABMANAGE.Service.Login.Dto;
using LABMANAGE.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.UntityCode
{
    public class MenuHelp
    {

        public static List<MenuModel> GetMenuList(int level, int pid)
        {
            using (Lab_ManagementEntities lab = new Lab_ManagementEntities())
            {
                if (string.IsNullOrEmpty(LoginBase.RoleId))
                    return null;
                int roleId = Convert.ToInt32(LoginBase.RoleId);

                //var aa = lab.RoleMenu.Where(m => m.Role_ID == roleId).Select(m=>m.Menu_ID);
                //var bb = lab.Menu.Where(m => aa.Contains(m.ID));

                var menuList = lab.Menu.Where(m => m.RoleMenu.Select(n => n.Role_ID).Contains(roleId)).Where(m => m.PID == pid);
                List<MenuModel> menuDtoList = menuList.ToList().ConvertAll(m => AutoMapperHelp.ConvertToDto<Menu, MenuModel>(m));
                return menuDtoList;
            }
        }   
    }
}
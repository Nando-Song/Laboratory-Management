using LABMANAGE.Data;
using LABMANAGE.Repository;
using LABMANAGE.Service.Equip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Equip
{
    public class EquipService:IEquipService
    {
        public IQQInvRepository<Equipment> EquipRepository;
        public EquipService(IQQInvRepository<Equipment> _EquipRepository)
        {
            EquipRepository = _EquipRepository;
        }
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="curPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        List<EquipBaseDto> IEquipService.GetEquipList(string nickName,  int curPage, int PageSize, out long recordCount)
        {
            var query = EquipRepository.Query();
            if (!String.IsNullOrEmpty(nickName) && nickName != "undefined")
            {
                query = query.Where(m => m.Equipment_Name == nickName);
            }
            recordCount = query.Count();
            query = query.OrderByDescending(m => m.ID).Skip((curPage - 1) * PageSize).Take(PageSize);
            List<EquipBaseDto> EquipList = query.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Equipment, EquipBaseDto>(c));
            return EquipList;
        }

        /// <summary>
        /// 数据插入
        /// </summary>
        /// <param name="Leaveinfo"></param>
        /// <returns></returns>
        public bool InsertInfo(EquipBaseDto Leaveinfo)
        {
            try
            {
                Equipment uList = AutoMapperHelp.ConvertModel<Equipment, EquipBaseDto>(Leaveinfo);
                Equipment insertList = EquipRepository.Add(uList);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="Equip"></param>
        /// <returns></returns>
        public bool Update(EquipBaseDto Equip)
        {
            try
            {
                Equipment evaEquip = EquipRepository.Get(Equip.ID);
                evaEquip.IsExamine = Equip.IsExamine;
                evaEquip.Pass = Equip.Pass;
                EquipRepository.Update(evaEquip);
                return true;
            }
            catch
            {
                return false;
            }
        }

       /// <summary>
       /// 获取对应ID的单条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public EquipBaseDto GetEquipPer(int id)
        {
            var list = EquipRepository.Query().Where(c => c.ID == id);
            List<EquipBaseDto> qqList = list.ToList().ConvertAll(c => AutoMapperHelp.ConvertToDto<Equipment, EquipBaseDto>(c));
            EquipBaseDto tt = qqList.FirstOrDefault();
            return tt;
        }
    }
}
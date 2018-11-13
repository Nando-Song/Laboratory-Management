using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Service.ysl_Sign_In.Dto
{
    public interface ISign_InService
    {
        List<Sign_dateModel> Get_data(int UID, int type);

        bool Is_Sign_In(int UID);

        string Obj2Json<Sign_dateModel>( List<Sign_dateModel> data);

        bool userSign(int UID, string ip);

        int time_Shift(string time);

        void Ecxel_add(string time, int uid);

        void LeaveInsert(DateTime star, DateTime end, int uid);
    }
}

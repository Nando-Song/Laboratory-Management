﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LABMANAGE.Service.Equip.Dto
{
    public class EUserBaseDto
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Real_Name { get; set; }
        public string Email { get; set; }
        //public string Phone { get; set; }
        //public string Password { get; set; }
        public int U_Role { get; set; }
        public int Room_ID { get; set; }
        //public string Image { get; set;}
        //public string Motto { get; set; }
        public int IsExamine { get; set; }
    }
}
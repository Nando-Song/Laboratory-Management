//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LABMANAGE.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public System.DateTime Time { get; set; }
        public string Equipment_Name { get; set; }
        public int Pass { get; set; }
        public int Verify_ID { get; set; }
        public string Number { get; set; }
        public int IsExamine { get; set; }
    
        public virtual User User { get; set; }
    }
}

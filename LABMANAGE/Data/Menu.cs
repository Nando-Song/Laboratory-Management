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
    
    public partial class Menu
    {
        public Menu()
        {
            this.RoleMenu = new HashSet<RoleMenu>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<int> PID { get; set; }
        public string ionic { get; set; }
    
        public virtual ICollection<RoleMenu> RoleMenu { get; set; }
    }
}

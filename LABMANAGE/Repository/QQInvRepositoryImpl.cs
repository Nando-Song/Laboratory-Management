
using LABMANAGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Repository
{
    public class QQInvRepositoryImpl<TEntity> : BaseEFRepositoryImpl<TEntity>, IQQInvRepository<TEntity> where TEntity : class,new()
    {
        public QQInvRepositoryImpl(Lab_ManagementEntities dbContext)
        {
            base.DbContext = dbContext;
        }
    }
}

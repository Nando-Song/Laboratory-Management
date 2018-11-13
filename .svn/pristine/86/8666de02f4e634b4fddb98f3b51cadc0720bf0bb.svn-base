
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using LABMANAGE.ViewModel;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace LABMANAGE.Repository
{
    /// <summary>
    /// 仓储基础
    /// </summary>
    /// <typeparam name="TEntity">表实体类</typeparam>
    public abstract class BaseEFRepositoryImpl<TEntity> : IRepository<TEntity> where TEntity : class,new()
    {
        #region 属性注入DbContext

        /// <summary>
        /// db上下文
        /// </summary>
        public DbContext DbContext { get; set; }

        #endregion

        #region 辅助方法
        public DbContext GetDbContext()
        {
            return this.DbContext;
        }
        /// <summary>
        /// 从上下文中移除实体副本
        /// </summary>
        /// <param name="entity">实体</param>
        private void detachEntityFromContext(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)DbContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (exists)
            {
                objContext.Detach(foundEntity);
            }
        }

        /// <summary>
        /// 从上下文中移除实体副本
        /// </summary>
        /// <param name="entity">实体</param>
        private void detachEntitiesFromContext(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                detachEntityFromContext(item);
            }
        }

        #endregion

        #region IRepository<T> 成员

        /// <summary>
        /// 创建空实体
        /// </summary>
        /// <returns>创建实体</returns>
        public virtual TEntity Create()
        {
            return DbContext.Set<TEntity>().Create();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">待新增实体</param>
        /// <returns>新增后实体</returns>
        public virtual TEntity AddNotCommite(TEntity entity)
        {
            var result = DbContext.Set<TEntity>().Add(entity);
            return result;
        }

        /// <summary>
        /// 插入(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待插入实体</param>
        /// <returns>插入后实体</returns>
        public virtual TEntity Add(TEntity entity)
        {
            var result = DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        public virtual void DeleteNotCommite(object id)
        {
            var dbEneity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(dbEneity);
        }

        /// <summary>
        /// 删除(立即提交到数据库)
        /// </summary>
        /// <param name="id">id</param>
        public virtual void Delete(object id)
        {
            var dbEneity = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(dbEneity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">待删除实体</param>
        public virtual void DeleteNotCommite(TEntity entity)
        {
            //detachEntityFromContext(entity);
            DbContext.Set<TEntity>().Remove(entity);
        }

        /// 删除(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待删除实体</param>
        public virtual void Delete(TEntity entity)
        {
            //detachEntityFromContext(entity);
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// 删除多个(立即提交到数据库)
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            //detachEntitiesFromContext(entities);
            DbContext.Set<TEntity>().RemoveRange(entities);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        public virtual void DeleteRangeNotCommite(IEnumerable<TEntity> entities)
        {
            //detachEntitiesFromContext(entities);
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新实体</param>
        /// <returns>更新实体</returns>
        public virtual TEntity UpdateNotCommite(TEntity entity)
        {
            //detachEntityFromContext(entity);
            DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 更新(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待更新实体</param>
        /// <returns>更新实体</returns>
        public virtual TEntity Update(TEntity entity)
        {
            //detachEntityFromContext(entity);
            DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// 查找单个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>查询到的结果</returns>
        public virtual TEntity Get(object id)
        {
            try
            {
                return DbContext.Set<TEntity>().Find(id);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据条件查询单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// 查询所有符合条件的实体
        /// </summary>
        /// <returns>所有符合条件的实体</returns>
        public virtual IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 查询所有实体并排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query<TKey>(Dictionary<Func<TEntity, TKey>, SortType> orderBy)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();
            //排序
            query = query.SortBy(orderBy).AsQueryable();
            return query;
        }

        /// <summary>
        ///  查询所有实体并分页
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="orderBy">排序</param>
        /// <returns>查询到的结果</returns>
        public virtual IQueryable<TEntity> Query<TKey>(Dictionary<Func<TEntity, TKey>, SortType> orderBy, ref PageModel page)
        {
            if (page == null)
                page = PageModel.Default();
            var query = DbContext.Set<TEntity>().AsQueryable();
            page.RecordCount = query.Count();
            //排序
            query = query.SortBy(orderBy).AsQueryable();
            //获取分页数据
            int skip = (page.PageNo - 1) * page.PageSize;
            query = query.Skip(skip).Take(page.PageSize);
            return query;
        }

        /// <summary>
        /// 根据条件查询所有符合条件的实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate);
        }
        /// <summary>
        /// 根据条件查询并排序
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query<TKey>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TKey>, SortType> orderBy)
        {
            var query = DbContext.Set<TEntity>().Where(predicate);
            query = query.SortBy(orderBy).AsQueryable();
            return query;
        }

        public virtual IQueryable<TEntity> Query<TKey>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TKey> orderBy, SortType sortType, ref PageModel page)
        {
            var orderByP = new Dictionary<Func<TEntity, TKey>, SortType>() { { orderBy, sortType } };
            return Query<TKey>(predicate, orderByP, ref page);
        }

        /// <summary>
        /// 根据条件查询并分页
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="page">分页信息</param>
        /// <returns>查询到的结果</returns>
        public virtual IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TValue>, SortType> orderBy, ref PageModel page)
        {
            if (page == null)
                page = PageModel.Default();
            var query = DbContext.Set<TEntity>().Where(predicate);
            page.RecordCount = query.Count();
            //排序
            query = query.SortBy(orderBy).AsQueryable();
            //获取分页数据
            int skip = (page.PageNo - 1) * page.PageSize;
            query = query.Skip(skip).Take(page.PageSize);
            return query;
        }

        /// <summary>
        /// SQL语句查询
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询到的结果</returns>
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }

        /// <summary>
        /// 保存(批量提交用)
        /// </summary>
        public virtual void Save()
        {
            DbContext.SaveChanges();
        }

        #endregion
    }
}

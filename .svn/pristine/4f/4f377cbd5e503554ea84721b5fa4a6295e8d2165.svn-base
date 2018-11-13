using LABMANAGE.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LABMANAGE.Repository
{
    /// <summary>
    /// 实体相关的仓储接口
    /// </summary>
    /// <typeparam name="TEntity">操作的实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取数据仓储上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// 创建空实体
        /// </summary>
        /// <returns>创建实体</returns>
        TEntity Create();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">待新增实体</param>
        /// <returns>新增后实体</returns>
        TEntity AddNotCommite(TEntity entity);

        /// <summary>
        /// 插入(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待插入实体</param>
        /// <returns>插入后实体</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        void DeleteNotCommite(object id);

        /// <summary>
        /// 删除(立即提交到数据库)
        /// </summary>
        /// <param name="id">id</param>
        void Delete(object id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">待删除实体</param>
        void DeleteNotCommite(TEntity entity);

        /// <summary>
        /// 删除(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待删除实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除多个(立即提交到数据库)
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="entities">待删除的实体集合</param>
        void DeleteRangeNotCommite(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">待更新实体</param>
        /// <returns>更新实体</returns>
        TEntity UpdateNotCommite(TEntity entity);

        /// <summary>
        /// 更新(立即提交到数据库)
        /// </summary>
        /// <param name="entity">待更新实体</param>
        /// <returns>更新实体</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 查找单个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>查询到的结果</returns>
        TEntity Get(object id);

        /// <summary>
        /// 根据条件查询单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询所有实体
        /// </summary>
        /// <returns>所有实体</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// 查询所有实体并排序
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Dictionary<Func<TEntity, TValue>, SortType> orderBy);
        /// <summary>
        /// 分页查询所有的实体
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Dictionary<Func<TEntity, TValue>, SortType> orderBy,ref PageModel page);

        /// <summary>
        /// 根据条件查询所有符合条件的实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件和排序查询实体
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TValue>, SortType> orderBy);

        /// <summary>
        /// 分页查询所有符合条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TValue>, SortType> orderBy, ref PageModel page);

        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TValue> orderBy, SortType sortType, ref PageModel page);

        /// <summary>
        /// SQL语句查询
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        /// <summary>
        /// 保存(批量提交用)
        /// </summary>
        void Save();
    }

    /// <summary>
    /// 与实体无关的仓储接口
    /// </summary>
    public interface ICommonRepository
    {
        /// <summary>
        /// 获取服务器当前时间
        /// </summary>
        /// <returns>获取当前时间</returns>
        DateTime GetServerNow();
        /// <summary>
        /// SQL语句查询
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> SelectQuery<TEntity>(string query, params object[] parameters);
    }
}

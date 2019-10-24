/* ***********************************************
 * CLR版本:  4.0.30319.42000
 * Author:  ZhangXiang
 * Function: 
 * History:  created by Author  2018-02-10 10:46:06
 * ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Utils
{
    public class EfRepositoryImpl<T, TDbContext> : IRepository<T>
        where T : Entity, new() where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        /// <summary>
        /// 仓储构造器
        /// </summary>
        /// <param name="context"></param>
        public EfRepositoryImpl(TDbContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> FindViewByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// 通过设置条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindQueryableByAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> FindViewQueryableByAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        /// <inheritdoc />
        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// 根据条件表示分页获取数据集合
        /// </summary>
        /// <param name="predicate">断言表达式</param>
        /// <param name="sortPredicate">排序断言</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">跳过序列中指定数量的元素，然后返回剩余的元素</param>
        /// <param name="take">从序列的开头返回指定数量的连续元素</param>
        /// <returns>result：数据集合；total：数据总数</returns>
        public virtual async Task<(List<T> result, int total)> GetAllAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int skip, int take)
        {
            IQueryable<T> result = Context.Set<T>().Where(predicate);
            int total = await result.CountAsync();
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    List<T> resultAscPaged = await
                        Context.Set<T>().Where(predicate).OrderBy(sortPredicate).Skip(skip).Take(take).ToListAsync();
                    return (result: resultAscPaged, total);

                case SortOrder.Descending:
                    List<T> resultDescPaged = await
                        Context.Set<T>().Where(predicate)
                            .OrderByDescending(sortPredicate)
                            .Skip(skip)
                            .Take(take).ToListAsync();
                    return (result: resultDescPaged, total);
                case SortOrder.Unspecified:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public async Task<(List<T> result, int total)> GetViewAllAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> sortPredicate, SortOrder sortOrder, int skip, int take)
        {
            int total = await Context.Set<T>().CountAsync(predicate);
            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    List<T> resultAscPaged = await
                        Context.Set<T>().Where(predicate).OrderBy(sortPredicate).Skip(skip).Take(take).ToListAsync();
                    return (result: resultAscPaged, total);

                case SortOrder.Descending:
                    List<T> resultDescPaged = await
                        Context.Set<T>().Where(predicate)
                            .OrderByDescending(sortPredicate)
                            .Skip(skip)
                            .Take(take).ToListAsync();
                    return (result: resultDescPaged, total);
                case SortOrder.Unspecified:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, null);
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await Context.Set<T>().CountAsync(expression);
        }

        /// <inheritdoc />
        /// <summary>
        /// 根据条件表达式检索对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<T> RetrieveAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 根据对象全局唯一标识检索对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> RetrieveAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// 根据对象全局唯一标识检索对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Retrieve(Guid id)
        {
            return Context.Set<T>().Find(id);
        }

        public bool Existed(Guid id)
        {
            DbSet<T> db = Context.Set<T>();
            return db.Any(m => m.ID.Equals(id));
        }

        public async Task<List<T>> GetViewAllAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> sortPredicate, SortOrder sortOrder = SortOrder.Ascending)
        {
            List<T> result;
            if (sortOrder == SortOrder.Ascending)
                result = await Context.Set<T>().Where(predicate).OrderBy(sortPredicate).ToListAsync();
            else
                result = await Context.Set<T>().Where(predicate).OrderByDescending(sortPredicate).ToListAsync();
            return result;
        }

        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

        public T RetrieveView(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> RetrieveViewAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetViewAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetViewAllAsync(Expression<Func<T, object>> sortPredicate,
            SortOrder sortOrder = SortOrder.Ascending)
        {
            List<T> result;
            if (sortOrder == SortOrder.Ascending)
                result = await Context.Set<T>().OrderBy(sortPredicate).ToListAsync();
            else
                result = await Context.Set<T>().OrderByDescending(sortPredicate).ToListAsync();
            return result;
        }
    }
}
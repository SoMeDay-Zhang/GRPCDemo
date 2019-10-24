/* ***********************************************
 * CLR版本: 4.0.30319.42000
 * Author:ZhangXiang
 * function: 定义接口
 * history: created by Author 2018-04-21 20:43:20
 * ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    public interface IPrimitiveRepository<T, in TIdentifier>
    {
        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> FindViewByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过设置条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindQueryableByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 通过设置条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> FindViewQueryableByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据对象全局唯一标识检索对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> RetrieveAsync(TIdentifier id);

        /// <summary>
        /// 根据对象全局唯一标识检索对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Retrieve(TIdentifier id);

        /// <summary>
        /// 对象已存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true：存在，false:不存在</returns>
        bool Existed(TIdentifier id);

        /// <summary>
        /// 计算符合条件的对象数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 根据条件表达式检索对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> RetrieveAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// 根据条件表示分页获取数据集合
        /// </summary>
        /// <param name="predicate">断言表达式</param>
        /// <param name="sortPredicate">排序断言</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">跳过序列中指定数量的元素，然后返回剩余的元素</param>
        /// <param name="take">从序列的开头返回指定数量的连续元素</param>
        /// <returns>result：数据集合；total：数据总数</returns>
        Task<(List<T> result, int total)> GetAllAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int skip, int take);
        
        /// <summary>
        /// 根据条件表示分页获取数据集合
        /// </summary>
        /// <param name="predicate">断言表达式</param>
        /// <param name="sortPredicate">排序断言</param>
        /// <param name="sortOrder">排序方式</param>
        /// <param name="skip">跳过序列中指定数量的元素，然后返回剩余的元素</param>
        /// <param name="take">从序列的开头返回指定数量的连续元素</param>
        /// <returns>result：数据集合；total：数据总数</returns>
        Task<(List<T> result, int total)> GetViewAllAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int skip, int take);


    }
}

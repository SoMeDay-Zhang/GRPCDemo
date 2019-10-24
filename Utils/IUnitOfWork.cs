/* ***********************************************
 * CLR版本:  4.0.30319.42000
 * Author:  ZhangXiang
 * Function: 定义接口
 * History:  created by Author  2018-02-10 10:36:23
 * ***********************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utils
{
    /// <inheritdoc />
    /// <summary>
    /// Unit Of Work Pattern
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交所有更改
        /// </summary>
        Task CommitAsync();

        Task BulkCommitAsync<T>(IList<T> entities) where T : Entity;

        void Commit();

        #region Methods

        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterNew<T>(T obj) where T : Entity;

        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterModified<T>(T obj) where T : Entity;

        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterDeleted<T>(T obj) where T : Entity;

        #endregion
    }
}
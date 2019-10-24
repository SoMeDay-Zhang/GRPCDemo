/* ***********************************************
 * CLR版本:  4.0.30319.42000
 * Author:  ZhangXiang
 * Function: 
 * History:  created by Author  2018-02-10 10:54:03
 * ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Utils
{
    /// <summary>
    /// Unit of Work Pattern Implement
    /// </summary>
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public virtual async Task CommitAsync()
        {
            // Save changes with the default options
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }

        }

        public virtual async Task BulkCommitAsync<T>(IList<T> entities) where T : Entity
        {
            await _dbContext.BulkInsertAsync(entities);
        }

        /// <inheritdoc />
        /// <summary>
        /// Disposes the current object
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void RegisterNew<TEntity>(TEntity obj) where TEntity : Entity
        {
            _dbContext.Set<TEntity>().Add(obj);
        }

        public virtual void RegisterModified<TEntity>(TEntity obj) where TEntity : Entity
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual void RegisterDeleted<TEntity>(TEntity obj) where TEntity : Entity
        {
            _dbContext.Entry(obj).State = EntityState.Deleted;
        }

        public void Commit()
        {
            // Save changes with the default options
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;

            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
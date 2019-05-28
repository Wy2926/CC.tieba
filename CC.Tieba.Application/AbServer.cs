using CC.Tieba.EF;
using CC.Tieba.EF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace CC.Tieba.Application
{
    public abstract class AbServer<TEntity> where TEntity : Entity
    {
        //EF上下文对象
        protected TiebaDbContext DBTieba { get { return DBHelp.GetEFCodeFirstDbContext(); } }

        protected DbSet<TEntity> Entities = DBHelp.GetEFCodeFirstDbContext().Set<TEntity>();

        /// <summary>
        /// 存在更新，不存在添加
        /// </summary>
        /// <param name="user"></param>
        public void AddorUpdate(TEntity entity)
        {
            if (Entities.Any(p => p.Key == entity.Key))
                Entities.Attach(entity);
            else
                Entities.Add(entity);
            DBTieba.SaveChanges();
        }

        /// <summary>
        /// 存在更新，不存在添加
        /// </summary>
        /// <param name="users"></param>
        public void AddorUpdates(IEnumerable<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                if (Entities.Any(p => p.Key == entity.Key))
                {
                    DBTieba.Attach(entity);
                    DBTieba.Entry(entity).State = EntityState.Modified;
                }
                else
                    Entities.Add(entity);
            }
            DBTieba.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键ID</param>
        public void Remove(string key)
        {
            TEntity entity = Entities.Where(p => p.Key == key).FirstOrDefault();
            Entities.Remove(entity);
            DBTieba.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keys">主键ID</param>
        public void Remove(IEnumerable<string> keys)
        {
            IQueryable<TEntity> entity = Entities.Where(p => keys.Contains(p.Key));
            Entities.RemoveRange(entity);
            DBTieba.SaveChanges();
        }

        /// <summary>
        /// 查询在这个时间以后的所有数据
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public IQueryable<TEntity> SelectRecentUpdate(DateTime nowTime)
        {
            return Entities
                .Where(p => p.UpdateTime >= nowTime);
        }

        /// <summary>
        /// 查询在这个时间以后的数据
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public IQueryable<TEntity> SelectRecentUpdate(DateTime nowTime, int index = 1, int pageSize = 50)
        {
            return SelectRecentUpdate(index, pageSize)
                .Where(p => p.UpdateTime >= nowTime);
        }

        /// <summary>
        /// 查询最近更新(分页)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntity> SelectRecentUpdate(int index = 1, int pageSize = 50)
        {
            return Entities
                .OrderByDescending(p => p.UpdateTime)
                .Skip((index - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// 查询所有(更新排序)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> SelectOrderByDescTime()
        {
            return Entities.OrderByDescending(p => p.UpdateTime);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities
                .Where(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> predicate, int index = 1, int pageSize = 50)
        {
            return Entities
                .Where(predicate)
                .Skip((index - 1) * pageSize)
                .Take(pageSize);
        }
    }
}

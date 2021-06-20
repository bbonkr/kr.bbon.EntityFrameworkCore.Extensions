using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kr.bbon.EntityFrameworkCore.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// If dependency is true, use where clause, otherwise does not use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dependency"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereDependsOn<T>(this IQueryable<T> query, bool dependency, Expression<Func<T, bool>> predicate)
        {
            if (dependency)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// If dependency is true, use where clause, otherwise does not use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dependency"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereDependsOn<T>(this IQueryable<T> query, bool dependency, Expression<Func<T,int, bool>> predicate)
        {
            if (dependency)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// If dependency is true, use where clause, otherwise does not use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dependency"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereDependsOn<T>(this IEnumerable<T> query, bool dependency, Func<T, bool> predicate)
        {
            if (dependency)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// If dependency is true, use where clause, otherwise does not use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="dependency"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereDependsOn<T>(this IEnumerable<T> query, bool dependency, Func<T, int, bool> predicate)
        {
            if (dependency)
            {
                query = query.Where(predicate);
            }

            return query;
        }

    }
}

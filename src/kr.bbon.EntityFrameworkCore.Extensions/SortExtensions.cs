using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace kr.bbon.EntityFrameworkCore.Extensions
{
    public static class SortExtensions
    {
        /// <summary>
        /// Sorts the elements of sequence according to a field name that must exist in element.
        /// <para>
        /// Navigation property is Not supported 
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="fieldName">Element's property name</param>
        /// <param name="isAscending">Use ascendant order or not</param>
        /// <returns>An <see cref="IOrderedQueryable{out TEntity}" /></returns>
        public static IOrderedQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, string fieldName, bool isAscending = true)
        where TEntity: class
        {
            var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                     BindingFlags.GetField | BindingFlags.SetField);

            var actualField =
                properties.FirstOrDefault(p => p.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));

            if (actualField == null)
            {
                throw new Exception($"Field {fieldName} does not exist in {typeof(TEntity).FullName} Type.");
            }

            Expression<Func<TEntity, object>> expression = x =>
                EF.Property<object>(x, actualField.Name);

            if (query.Expression.Type == typeof(IOrderedQueryable<TEntity>))
            {
                var orderedQueryable = query as IOrderedQueryable<TEntity>;

                return isAscending
                    ? orderedQueryable.ThenBy(expression)
                    : orderedQueryable.ThenByDescending(expression);
            }
            
            return isAscending
                ? query.OrderBy(expression)
                : query.OrderByDescending(expression);

        }

        /// <summary>
        /// Represents orderBy and OrderByDescending.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="keySelector"></param>
        /// <param name="isAscending"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> Sort<TEntity, TKey>(this IQueryable<TEntity> query,
            Expression<Func<TEntity, TKey>> keySelector, bool isAscending = true)
        where TEntity : class
        {
            if (query.Expression.Type == typeof(IOrderedQueryable<TEntity>))
            {
                var orderedQueryable = query as IOrderedQueryable<TEntity>;

                return isAscending
                    ? orderedQueryable.ThenBy(keySelector)
                    : orderedQueryable.ThenByDescending(keySelector);

            }

            return isAscending
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);

        }
    }
}

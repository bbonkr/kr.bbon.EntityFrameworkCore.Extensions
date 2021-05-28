using System;
using System.Linq;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace kr.bbon.EntityFrameworkCore.Extensions
{
    public static class SortExtensions
    {
        /// <summary>
        /// Sorts the elements of sequence according to a field name that must exist in element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="fieldName">Element's property name</param>
        /// <param name="isAscending">Use ascendant order or not</param>
        /// <returns>An <see cref="IOrderedQueryable<out T>"/></returns>
        public static IOrderedQueryable<T> Sort<T>(this IQueryable<T> query, string fieldName, bool isAscending = true)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField);

            var actualField= properties.FirstOrDefault(p => p.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            
            if (actualField == null)
            {
                throw new Exception($"Field {fieldName} does not exist in {typeof(T).FullName} Type.");
            }

            System.Linq.Expressions.Expression<Func<T, object>> expression = x => EF.Property<object>(x, actualField.Name);

            if (query.Expression.Type == typeof(IOrderedQueryable<T>))
            {
                var orderedQueryable = query as IOrderedQueryable<T>;
                {
                    return isAscending
                        ? orderedQueryable.ThenBy(expression)
                        : orderedQueryable.ThenByDescending(expression);
                }
            }
            else
            {
                return isAscending
                    ? query.OrderBy(expression)
                    : query.OrderByDescending(expression);
            }
        }
    }
}

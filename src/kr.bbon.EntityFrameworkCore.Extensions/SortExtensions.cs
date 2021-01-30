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
            var actualFieldName = String.Empty;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField);

            foreach (var p in properties)
            {
                if (p.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    actualFieldName = p.Name;
                }
            }

            if (String.IsNullOrWhiteSpace(actualFieldName))
            {
                throw new Exception($"Field {fieldName} does not exist in {typeof(T).FullName} Type.");
            }

            if (query is IOrderedQueryable<T>)
            {
                return isAscending
                    ? (query as IOrderedQueryable<T>).ThenBy(x => EF.Property<object>(x, actualFieldName))
                    : (query as IOrderedQueryable<T>).ThenByDescending(x => EF.Property<object>(x, actualFieldName));
            }
            else
            {
                return isAscending
                    ? query.OrderBy(x => EF.Property<object>(x, actualFieldName))
                    : query.OrderByDescending(x => EF.Property<object>(x, actualFieldName));
            }
        }
    }
}

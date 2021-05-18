using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace kr.bbon.EntityFrameworkCore.Extensions
{
    public interface IPagedModel<TModel>
    {
        int CurrentPage { get; init; }
        IEnumerable<TModel> Items { get; init; }
        int Limit { get; init; }
        int TotalItems { get; init; }
        int TotalPages { get; init; }
    }

    public class PagedModel<TModel> : IPagedModel<TModel>
    {
        public int CurrentPage { get; init; }

        public int Limit { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public IEnumerable<TModel> Items { get; init; }
    }

    public static class PagedModelExtensions
    {
        public const int PAGE = 1;
        public const int LIMIT = 10;

        public static async Task<IPagedModel<TModel>> ToPagedModelAsync<TModel>(
            this IQueryable<TModel> query, 
            int page = PAGE, 
            int limit = LIMIT, 
            CancellationToken cancellationToken = default) where TModel : class
        {
            if (limit < 1)
            {
                throw new ArgumentException($"Limit should be greater than 1.", nameof(limit));
            }

            var currentPage = page < 0 ? 1 : page;
            var totalItems = await query.CountAsync(cancellationToken);
            var totalPages = (int)(Math.Ceiling(totalItems / (double)limit));

            var skip = (currentPage - 1) * limit;

            var items = await query.Skip(skip).Take(limit).ToListAsync(cancellationToken);

            return new PagedModel<TModel>
            {
                CurrentPage = currentPage,
                Items = items,
                Limit = limit,
                TotalItems = totalItems,
                TotalPages = totalPages,
            };
        }

        public static TResult ToPagedModel<TModel, TResult>(
            this IQueryable<TModel> query, 
            int page = PAGE, 
            int limit = LIMIT) where TModel : class where TResult : class, IPagedModel<TModel>
        {
            if (limit < 1)
            {
                throw new ArgumentException($"Limit should be greater than 1.", nameof(limit));
            }

            var currentPage = page < 0 ? 1 : page;
            var totalItems = query.Count();
            var totalPages = (int)(Math.Ceiling(totalItems / (double)limit));

            var skip = (currentPage - 1) * limit;

            var items = query.Skip(skip).Take(limit).ToList();

            var result = new PagedModel<TModel>
            {
                CurrentPage = currentPage,
                Items = items,
                Limit = limit,
                TotalItems = totalItems,
                TotalPages = totalPages,
            };

            return result as TResult;
        }
    }
}

﻿using System;
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
        /// <summary>
        /// Current page
        /// </summary>
        int CurrentPage { get; init; }

        /// <summary>
        /// Records
        /// </summary>
        IEnumerable<TModel> Items { get; init; }

        /// <summary>
        /// Items count per page
        /// </summary>
        int Limit { get; init; }

        /// <summary>
        /// Total items count
        /// </summary>
        int TotalItems { get; init; }

        /// <summary>
        /// Total page count
        /// </summary>
        int TotalPages { get; init; }
    }

    public class PagedModel<TModel> : IPagedModel<TModel>
    {
        /// <inheritdoc />
        public int CurrentPage { get; init; }

        /// <inheritdoc />
        public int Limit { get; init; }

        /// <inheritdoc />
        public int TotalItems { get; init; }

        /// <inheritdoc />
        public int TotalPages { get; init; }

        /// <inheritdoc />
        public IEnumerable<TModel> Items { get; init; }
    }

    public static class PagedModelExtensions
    {
        /// <summary>
        /// Default page value
        /// </summary>
        public const int PAGE = 1;
        /// <summary>
        /// Default limit value
        /// </summary>
        public const int LIMIT = 10;

        /// <summary>
        /// Get <see cref="IPagedModel{TModel}"/> instance of query result
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get <see cref="IPagedModel{TModel}"/> instance of query result
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
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

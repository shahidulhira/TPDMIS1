using RapidFireLib.Lib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Extensions
{
    public static class IQueryablePageListExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="pageIndex">The index of the page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <param name="indexFrom">The start index value.</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.</returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0)
        {
            try
            {
                
                var count = source.Count();
                var items = source.Skip((pageIndex - indexFrom) * pageSize)
                                        .Take(pageSize).ToList();
                var pagedList = new PagedList<T>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    IndexFrom = indexFrom,
                    TotalCount = count,
                    Items = items.ToList(),
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize)
                };
                return pagedList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

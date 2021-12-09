using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.Common.Ui
{
    public class Pager<T>
    {
        private readonly IEnumerable<T> items;
        private readonly int pageSize;
        private int page = -1;

        private static int FirstPage => 0;
        private int LastPage => (TotalItems / pageSize);
        private int TotalItems => items.Count();

        public Pager(IEnumerable<T> items, int pageSize)
        {
            this.items = items;
            this.pageSize = pageSize;
        }

        public IEnumerable<T> Next()
        {
            page++;
            return items.Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Prev()
        {
            page--;
            return items.Skip(page * pageSize).Take(pageSize);
        }

        public bool CanExecuteNext => ((page + 1) * pageSize) < TotalItems;

        public bool CanExecutePrev => page > 0;

        public int CurrentPage => page;

        public IEnumerable<T> Page(int pageNumber)
        {
            if (pageNumber <= LastPage)
            {
                page = pageNumber;
                return items.Skip(pageNumber * pageSize).Take(pageSize);
            }
            else
            {
                page = -1;
                return new List<T>();
            }
        }

        public IEnumerable<T> First()
        {
            return Page(Pager<T>.FirstPage);
        }

        public IEnumerable<T> Last()
        {
            return Page(LastPage);
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.Common.Ui
{
    public class Pager<T>
    {
        private readonly IEnumerable<T> items;
        private readonly int pageSize;
        private int page = -1;

        private int firstPage => 0;
        private int lastPage => (totalItems / pageSize);
        private int nextPage => page + 1;
        private int prevPage => page - 1;
        private int totalItems => items.Count();

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

        public bool CanExecuteNext => (((page + 1) * pageSize) < totalItems);

        public bool CanExecutePrev => page > 0;

        public int CurrentPage => page;

        public IEnumerable<T> TryJumpToPage(int pageNumber)
        {
            if (pageNumber <= lastPage)
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

        public IEnumerable<T> FirstPage()
        {
            return TryJumpToPage(firstPage);
        }

        public IEnumerable<T> LastPage()
        {
            return TryJumpToPage(lastPage);
        }
    }
}
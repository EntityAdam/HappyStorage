using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyStorage.Common.Ui
{
    public class Pager<T>
    {
        private readonly IEnumerable<T> items;
        private readonly int pageSize;
        private int page = -1;

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

        private int totalItems => items.Count();

        public bool CanExecuteNext => (((page+1) * pageSize) < totalItems);

        public bool CanExecutePrev => page > 0;
    }
}

using HappyStorage.Common.Ui;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HappyStorage.UnitTests
{
    public class PagerTests
    {
        [Fact]
        public void PagerNext()
        {
            var small = PagerTestsHelper.GetList(5);

            var pager = new Pager<int>(small, 5);
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);

            var page0 = pager.Next();

            Assert.Equal(5, page0.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);

            var page1 = pager.Next();
            Assert.Empty(page1);
            Assert.False(pager.CanExecuteNext);
            Assert.True(pager.CanExecutePrev);

            var page0x = pager.Prev();
            Assert.Equal(5, page0x.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);
        }

        [Fact]
        public void PagerNext2()
        {
            var small = PagerTestsHelper.GetList(12);

            var pager = new Pager<int>(small, 10);
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);

            var page0 = pager.Next();

            Assert.Equal(10, page0.Count());
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);

            var page1 = pager.Next();
            Assert.Equal(2, page1.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.True(pager.CanExecutePrev);

            var page0x = pager.Prev();
            Assert.Equal(10, page0x.Count());
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecutePrev);
        }

        [Fact]
        public void PagerJumpToSpecificPage()
        {
            var big = PagerTestsHelper.GetList(23);
            var pager = new Pager<int>(big, 5);

            Assert.Equal(5, pager.FirstPage().Count());
            Assert.Equal(0, pager.CurrentPage);

            Assert.Equal(3, pager.LastPage().Count());
            Assert.Equal(4, pager.CurrentPage);

            var page = pager.TryJumpToPage(2);
            Assert.Equal(2, pager.CurrentPage);
            Assert.Equal(5, page.Count());

            var page2 = pager.TryJumpToPage(4);
            Assert.Equal(4, pager.CurrentPage);
            Assert.Equal(3, page2.Count());

            var page3 = pager.TryJumpToPage(int.MaxValue);
            Assert.Equal(-1, pager.CurrentPage);
            Assert.Empty(page3);
        }
    }

    public static class PagerTestsHelper
    {
        public static List<int> GetList(int count)
        {
            var list = new List<int>();
            for (var i = 0; i < count; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}
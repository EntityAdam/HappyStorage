using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HappyStorage.Common.Ui;
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
            Assert.False(pager.CanExecuteBack);

            var page0 = pager.Next();

            Assert.Equal(5, page0.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.False(pager.CanExecuteBack);

            var page1 = pager.Next();
            Assert.Empty(page1);
            Assert.False(pager.CanExecuteNext);
            Assert.True(pager.CanExecuteBack);

            var page0x = pager.Prev();
            Assert.Equal(5, page0x.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.False(pager.CanExecuteBack);
        }

        [Fact]
        public void PagerNext2()
        {
            var small = PagerTestsHelper.GetList(12);

            var pager = new Pager<int>(small, 10);
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecuteBack);

            var page0 = pager.Next();

            Assert.Equal(10, page0.Count());
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecuteBack);

            var page1 = pager.Next();
            Assert.Equal(2, page1.Count());
            Assert.False(pager.CanExecuteNext);
            Assert.True(pager.CanExecuteBack);

            var page0x = pager.Prev();
            Assert.Equal(10, page0x.Count());
            Assert.True(pager.CanExecuteNext);
            Assert.False(pager.CanExecuteBack);
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

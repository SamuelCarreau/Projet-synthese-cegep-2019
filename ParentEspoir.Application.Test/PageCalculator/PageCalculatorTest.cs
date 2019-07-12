using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using ParentEspoir.Application;

namespace ParentEspoir.Application.Test.PageCalculatorTest
{
    public class PageCalculatorTest
    {
        private const int COUNT_PER_PAGE = 25;
        private const int COUNT_LAST_PAGE = 8;
        private const int TOTAL_COUNT = 183;
        private const int TOTAL_PAGES = 8;

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void TestPageCalculator(int currentPage)
        {
            PageCalculator page = new PageCalculator(COUNT_PER_PAGE, currentPage, TOTAL_COUNT);
            page.Skip.ShouldBe((currentPage - 1) * COUNT_PER_PAGE);
            page.Take.ShouldBe(COUNT_PER_PAGE);
            page.CurrentPage.ShouldBe(currentPage);
            page.TotalPage.ShouldBe(TOTAL_PAGES);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(100)]
        [InlineData(110012)]
        public void TestPageCaculatorLastPage(int currentPage)
        {
            PageCalculator page = new PageCalculator(COUNT_PER_PAGE, currentPage, TOTAL_COUNT);
            page.Skip.ShouldBe(COUNT_PER_PAGE * 7);
            page.Take.ShouldBe(COUNT_LAST_PAGE);
            page.CurrentPage.ShouldBe(8);
            page.TotalPage.ShouldBe(TOTAL_PAGES);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-12212)]
        public void TestPageCaculatorFirstPAge(int currentPage)
        {
            PageCalculator page = new PageCalculator(COUNT_PER_PAGE, currentPage, TOTAL_COUNT);
            page.Skip.ShouldBe(0);
            page.Take.ShouldBe(COUNT_PER_PAGE);
            page.CurrentPage.ShouldBe(1);
            page.TotalPage.ShouldBe(TOTAL_PAGES);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(8)]
        [InlineData(19)]
        public void TestPageCalculator_TotalCountLessThenCountPerPage(int totalCount)
        {
            PageCalculator page = new PageCalculator(COUNT_PER_PAGE, 1, totalCount);
            page.Skip.ShouldBe(0);
            page.Take.ShouldBe(totalCount);
            page.CurrentPage.ShouldBe(1);
            page.TotalPage.ShouldBe(1);
        }
    }
}

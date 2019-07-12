using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class PageCalculator
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

        public PageCalculator(int countPerPage, int currentPage, int totalCount)
        {
            int totalPages = totalCount / countPerPage;

            if ((totalCount - (totalPages * countPerPage)) % countPerPage != 0)
            {
                totalPages++;
            }

            if(totalPages == 0)
            {
                totalPages = 1;
            }

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            Skip = countPerPage * (currentPage - 1);
            CurrentPage = currentPage;
            TotalPage = totalPages;
            Take = countPerPage;

            if(totalCount < countPerPage)
            {
                Take = totalCount;
            }

            if (currentPage == totalPages)
            {
                Take = totalCount - Skip;
            }
        }
    }
}

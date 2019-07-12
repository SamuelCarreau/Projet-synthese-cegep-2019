using ParentEspoir.Common;
using ParentEspoir.Persistence;
using System;
using System.Linq;

namespace ParentEspoir.Application
{
    public static class CustomerFileNumberCreator
    {
        public static int CreateFileNumberAsync(ParentEspoirDbContext context, IDateTime time)
        {
            int id = GetLastEntryThisMonthAsync(context, time) + 1;
            string idStr = "";
            if (id < 10)
            {
                idStr = '0' + Convert.ToString(id);
            }
            else
            {
                idStr = Convert.ToString(id);
            }

            string year = time.Now.ToString("yyyy");
            string month = time.Now.ToString("MM");
            string fileNumber = year + month + idStr;

            return Convert.ToInt32(fileNumber);
        }

        private static int GetLastEntryThisMonthAsync(ParentEspoirDbContext context, IDateTime time)
        {
            return context.Customers
                .Where(c => c.CreationDate.Year == time.Now.Year && c.CreationDate.Month == time.Now.Month)
                .Count();
        }
    }
}

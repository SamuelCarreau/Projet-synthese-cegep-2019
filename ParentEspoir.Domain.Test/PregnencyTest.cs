using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using System;
using Xunit;

namespace ParentEspoir.Domain.Test
{
    public class PregnencyTest
    {
        [Fact]
        public void WeekCountTestForZeroWeek()
        {
            var pregnency = new Pregnancy
            {
                ChildBirthExpectedDate = DateTime.Now + PregnancyConstant.NB_DAYS_FOR_WHOLE_PREGNENCY
            };

            Assert.Equal(0, pregnency.WeekCount);
        }

        [Fact]
        public void WeekCountTestForTowWeeks()
        {
            var pregnency = new Pregnancy
            {
                ChildBirthExpectedDate = DateTime.Now + (PregnancyConstant.NB_DAYS_FOR_WHOLE_PREGNENCY - TimeSpan.FromDays(14))
            };

            Assert.Equal(2, pregnency.WeekCount);
        }

        [Fact]
        public void WeekCountTestForFortyFourWeeks()
        {
            var pregnency = new Pregnancy
            {
                ChildBirthExpectedDate = DateTime.Now
            };

            Assert.Equal(40, pregnency.WeekCount);
        }
    }
}

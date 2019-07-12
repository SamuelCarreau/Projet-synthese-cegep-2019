using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System.Threading;
using System;
using System.Collections.Generic;
using MediatR;

namespace ParentEspoir.Application.Test
{
    public class MemberTest : TestBase
    {

        private ParentEspoirDbContext _context;

        public MemberTest()
        {
            _context = GetDbContext();

            _context.Add(new CustomerDescription());

            _context.Add(new Member
            {
                AmountByMonth = 1000m,
                Customer = new Customer(),
                SubscriptionDate = new DateTime(2012, 3, 29),
                VolunteeringHourCountByMonth = 3
            });

            _context.Add(new Member
            {
                AmountByMonth = 1500m,
                Customer = new Customer(),
                SubscriptionDate = new DateTime(2012, 3, 29),
                VolunteeringHourCountByMonth = 4
            });

            _context.SaveChanges();
        }

        //[Fact]
        //public void GetListMemberTest()
        //{
        //    var handler = new GetMemberListQueryHandler(_context);

        //    var response = handler.Handle(new GetMemberListQuery(), CancellationToken.None).Result;

        //    response.GetType().GetInterfaces().ShouldContain(typeof(System.Collections.Generic.IEnumerable<Member>));

        //    response.Count().ShouldBe(2);
        //    response.ShouldContain(m => m.AmountByMonth == 1000m);
        //    response.ShouldContain(m => m.SubscriptionDate == new DateTime(2012, 3, 29));
        //}

        //[Fact]
        //public void CreateMemberTest()
        //{
        //    _context.Members.Where(m => m.AmountByMonth == 500m).Any().ShouldBe(false);

        //    var handler = new CreateMemberCommandHandler(_context);

        //    var response = handler.Handle(new CreateMemberCommand
        //    {
        //        AmountByMonth = 500m,
        //        CustomerDescriptionId = 1,
        //        RegisteredDate = new System.DateTime(2015, 6, 25),
        //        VolunteeringHourCountByMonth = 4
        //    }, CancellationToken.None).Result;

        //    response.ShouldBeOfType(typeof(Unit));

        //    _context.Members.Where(m => m.AmountByMonth == 500m).Any().ShouldBe(true);
        //}

        //[Fact]
        //public void UpdateMemberTest()
        //{
        //    var member = _context.Members.Where(m => m.AmountByMonth == 1000m).Single();

        //    var handler = new UpdateMemberCommandHandler(_context);

        //    var response = handler.Handle(new UpdateMemberCommand
        //    {
        //        AmountByMonth = 500m,
        //        CustomerDescriptionId = 1,
        //        RegisteredDate = new System.DateTime(2015, 6, 25),
        //        VolunteeringHourCountByMonth = 4
        //    }, CancellationToken.None).Result;

        //    response.ShouldBeOfType(typeof(Unit));

        //    _context.Members.Where(m => m.AmountByMonth == 500m).Any().ShouldBe(true);
        //}

        //[Fact]
        //public void DeleteMemberTest()
        //{
        //    _context.Members.Find(1).ShouldNotBe(null);

        //    var handler = new DeleteMemberCommandHandler(_context);

        //    var response = handler.Handle(new DeleteMemberCommand
        //    {
        //        MemberId = 1
        //    }, CancellationToken.None).Result;

        //    response.ShouldBeOfType(typeof(Unit));

        //    _context.Members.Find(1).ShouldBe(null);
        //}

    }
}
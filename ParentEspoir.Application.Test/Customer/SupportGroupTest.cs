using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System.Threading;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;

namespace ParentEspoir.Application.Test
{
    public class SupportGroupTest : TestBase
    {
        private static readonly string CREATE = "CREATE";
        private static readonly string CREATE_DESCRIPTION = "CREATE_DESCRITPION";
        private static readonly string FIND = "FIND";
        private static readonly string UPDATE = "UPDATE";
        private static readonly string AFTER_UPDATE = "AFTER_UPDATE";
        private static readonly string DESCRIPTION_UPDATED = "NEW DESCRIPTION";
        private static readonly string DELETE = "DELETE";

        private static readonly string FIRST_CUSTOMER_NAME = "My first customer";
        private static readonly string SECOND_CUSTOMER_NAME = "My second customer";

        private ParentEspoirDbContext _context;

        public SupportGroupTest()
        {
            _context = GetDbContext();

            _context.Add(new SupportGroup
            {
                Name = FIND,
                Description = CREATE_DESCRIPTION
            });

            _context.SaveChanges();

            _context.Add(new SupportGroup
            {
                Name = UPDATE
            });

            _context.SaveChanges();

            _context.Add(new SupportGroup
            {
                Name = DELETE
            });

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetSupportGroupTest()
        {
            var response = await _mediator.Send(new GetSupportGroupQuery
            {
                SupportGroupId = _context.SupportGroups.Where(sg => sg.Name == FIND).Single().SupportGroupId,
            });

            response.ShouldBeOfType(typeof(SupportGroup));

            response.Name.ShouldBe(FIND);
            response.Description.ShouldBe(CREATE_DESCRIPTION);
        }

        //[Fact]
        //public async Task GetListSupportGroupTest()
        //{
        //    var response = await _mediator.Send(new GetSupportGroupListQuery());

        //    response.GetType().GetInterfaces().ShouldContain(typeof(IEnumerable<SupportGroup>));

        //    response.Count().ShouldBe(3);
        //    response.ShouldContain(s => s.Name == FIND);
        //    response.ShouldContain(s => s.Name == UPDATE);
        //    response.ShouldContain(s => s.Name == DELETE);
        //}

        [Fact]
        public async Task CreateSupportGroupTest()
        {
            _context.SupportGroups.Where(a => a.Name == CREATE).SingleOrDefault().ShouldBe(null);

            var result = await _mediator.Send(new CreateSupportGroupCommand
            {
                Name = CREATE,
                Description = CREATE_DESCRIPTION
            });

            result.ShouldBeOfType(typeof(Unit));

            var supportGroupCreated = _context.SupportGroups.Where(s => s.Name == CREATE).SingleOrDefault();
            supportGroupCreated.Description.ShouldBe(CREATE_DESCRIPTION);
            supportGroupCreated.Name.ShouldBe(CREATE);
        }

        [Fact]
        public async Task UpdateSupportGroupTest()
        {
            var supportgroup = _context.SupportGroups.Where(s => s.Name == UPDATE).Single();

            var result = await _mediator.Send(new UpdateSupportGroupCommand
            {
                SupportGroupId = supportgroup.SupportGroupId,
                Name = AFTER_UPDATE,
                Description = DESCRIPTION_UPDATED
            });

            result.ShouldBeOfType(typeof(Unit));

            _context.SupportGroups.Where(s => s.Name == AFTER_UPDATE).SingleOrDefault().ShouldNotBe(null);
            var supportGroupCreated = _context.SupportGroups.Where(s => s.Name == AFTER_UPDATE).Single();
            supportgroup.Name.ShouldBe(AFTER_UPDATE);
            supportgroup.Description.ShouldBe(DESCRIPTION_UPDATED);
        }

        [Fact]
        public async Task DeleteSupportGroupTest()
        {
            var supportgroup = _context.SupportGroups.Where(s => s.Name == DELETE && s.IsDelete == false).Single();

            var result = await _mediator.Send(new DeleteSupportGroupCommand
            {
                SupportGroupId = supportgroup.SupportGroupId
            });

            result.ShouldBeOfType(typeof(Unit));

            _context.SupportGroups
                .Where(sg => sg.IsDelete == true && sg.SupportGroupId == supportgroup.SupportGroupId)
                .ShouldNotBe(null);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void CreateExceptionSupportGroupTest(string name)
        {
            var handler = new CreateSupportGroupCommandHandler(_context);

            var result = _mediator.Send(new CreateSupportGroupCommand { Name = name }, CancellationToken.None)
                .ShouldThrow(typeof(ValidationException));

            _context.SupportGroups.Where(s => string.IsNullOrWhiteSpace(s.Name)).Any().ShouldBe(false);
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("francais", "français")]
        [InlineData(" my group", "    my   group  ")]
        public void CantCreateExistingGroup(string createWithDb, string createWithMediator)
        {
            _context.Add(new SupportGroup { Name = createWithDb });
            _context.SaveChanges();

            _mediator.Send(new CreateSupportGroupCommand
            {
                Name = createWithMediator
            }).ShouldThrow(typeof(ValidationException));
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("francais", "français")]
        [InlineData(" my group", "    my   group  ")]
        public void CanUpdateToNameOfExistingGroup(string createWithDb, string createWithMediator)
        {
            _context.Add(new SupportGroup { Name = createWithDb });
            var selectedGroup = _context.Add(new SupportGroup { Name = "My strange name" });
            _context.SaveChanges();

            _mediator.Send(new UpdateSupportGroupCommand
            {
                SupportGroupId = selectedGroup.Entity.SupportGroupId,
                Name = createWithMediator
            }).ShouldThrow(typeof(ValidationException));
        }

        //[Theory]
        //[InlineData(1, null)]
        //[InlineData(2, "")]
        //[InlineData(3, "  ")]
        //public void UpdateExceptionSupportGroupTest(int id, string name)
        //{
        //    var handler = new UpdateSupportGroupCommandHandler(_context);

        //    var result = handler.Handle(new UpdateSupportGroupCommand { SupportGroupId = id, Name = name }, CancellationToken.None).ShouldThrow(typeof(InvalideNameException));

        //    _context.SupportGroups.Where(s => string.IsNullOrWhiteSpace(s.Name)).Any().ShouldBe(false);
        //}

        [Fact]
        public void GetSupportGroupCustumerListTest()
        {
            _context.Add(new Customer
            {
                FirstName = FIRST_CUSTOMER_NAME,
                SupportGroup = _context.SupportGroups.Where(sg => sg.Name == FIND).Single()
            });

            _context.Add(new Customer
            {
                FirstName = SECOND_CUSTOMER_NAME,
                SupportGroup = _context.SupportGroups.Where(sg => sg.Name == FIND).Single()
            });

            _context.SaveChanges();

            var response = _mediator.Send(new GetSupportGroupQuery
            {
                SupportGroupId = _context.SupportGroups.Where(sg => sg.Name == FIND).Single().SupportGroupId
            }, CancellationToken.None).Result;

            response.Customers.Count.ShouldBe(2);
            response.Customers.ElementAt(0).FirstName.ShouldBe(FIRST_CUSTOMER_NAME);
            response.Customers.ElementAt(1).FirstName.ShouldBe(SECOND_CUSTOMER_NAME);
        }

        //[Fact]
        //public void GetListSupportGroupWithOneDeleteTest()
        //{
        //    _context.Add(new SupportGroup
        //    {
        //        Name = "Anything",
        //        Description = "",
        //        IsDelete = true
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetSupportGroupListQuery()).Result;

        //    response.GetType().GetInterfaces().ShouldContain(typeof(System.Collections.Generic.IEnumerable<SupportGroup>));

        //    response.Count().ShouldBe(3);
        //    response.ShouldContain(s => s.Name == FIND);
        //    response.ShouldContain(s => s.Name == UPDATE);
        //    response.ShouldContain(s => s.Name == DELETE);
        //    response.ShouldNotContain(s => s.Name == "Anything");
        //}

        //[Fact]
        //public void GetSupportGroupDeletedTest()
        //{
        //    _context.Add(new SupportGroup
        //    {
        //        Name = "Anything",
        //        Description = "",
        //        IsDelete = true
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetSupportGroupQuery
        //    {
        //        SupportGroupId = _context.SupportGroups.Where(sg => sg.Name == "Anything").Single().SupportGroupId,
        //    }, CancellationToken.None).Result;

        //    response.ShouldBe(null);
        //}
    }
}
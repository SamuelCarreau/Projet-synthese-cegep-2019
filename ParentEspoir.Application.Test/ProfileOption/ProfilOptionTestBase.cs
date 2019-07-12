using FluentValidation;
using MediatR;
using ParentEspoir.Application.Test;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public abstract class ProfilOptionTestBase<TProfilOption> : TestBase where TProfilOption : class, IProfileOption
    {
        private static readonly string CREATE = "CREATE";
        private static readonly string FIND = "FIND";
        private static readonly string UPDATE = "UPDATE";
        private static readonly string AFTER_UPDATE = "AFTER_UPDATE";
        private static readonly string DELETE = "DELETE";

        private ParentEspoirDbContext _context;

        private TProfilOption InstanciateEntity(string name, bool isDelete = false)
        {
            TProfilOption newObject = (TProfilOption)Activator.CreateInstance(typeof(TProfilOption));

            newObject.Name = name;
            newObject.IsDelete = isDelete;

            return newObject;
        }

        public ProfilOptionTestBase()
        {
            _context = GetDbContext();

            _context.Add(InstanciateEntity(FIND));
            _context.Add(InstanciateEntity(UPDATE));
            _context.Add(InstanciateEntity(DELETE));
            _context.Add(InstanciateEntity("Deleted", true));

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetListTest()
        {
            var response = await _mediator.Send(new GetProfilOptionQuery<TProfilOption>());

            response.GetType().GetInterfaces().ShouldContain(typeof(IEnumerable<TProfilOption>));

            response.Count().ShouldBe(3);
            response.ShouldContain(a => a.Name == FIND);
            response.ShouldContain(a => a.Name == UPDATE);
            response.ShouldContain(a => a.Name == DELETE);
        }

        [Fact]
        public async Task CreateTest()
        {
            _context.Set<TProfilOption>().Where(a => a.Name == CREATE).SingleOrDefault().ShouldBe(null);

            var result = await _mediator.Send(new CreateProfilOptionCommand<TProfilOption> { Name = CREATE });

            result.ShouldBeOfType(typeof(Unit));

            _context.Set<TProfilOption>().Where(a => a.Name == CREATE).SingleOrDefault().ShouldNotBe(null);
        }

        [Fact]
        public async Task UpdateTest()
        {
            var profilOption = _context.Set<TProfilOption>().Where(t => t.Name == UPDATE).Single();

            var result = await _mediator.Send(new UpdateProfilOptionCommand<TProfilOption> { Id = profilOption.Id, Name = AFTER_UPDATE });

            result.ShouldBeOfType(typeof(Unit));

            _context.Set<TProfilOption>().Where(t => t.Name == AFTER_UPDATE).SingleOrDefault().ShouldNotBe(null);
            _context.Set<TProfilOption>().Where(t => t.Name == UPDATE).SingleOrDefault().ShouldBe(null);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var profilOption = _context.Set<TProfilOption>()
                .Where(t => t.Name == DELETE && t.IsDelete == false).Single();

            var result = await _mediator.Send(new DeleteProfilOptionCommand<TProfilOption> { Id = profilOption.Id });

            result.ShouldBeOfType(typeof(Unit));

            profilOption.IsDelete.ShouldBe(true);
            _context.Set<TProfilOption>()
                .Where(t => t.IsDelete == false && t.Name == DELETE)
                .SingleOrDefault()
                .ShouldBe(null);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("\t\n")]
        public void CreateExceptionTest(string name)
        {
            var result = _mediator.Send(new CreateProfilOptionCommand<TProfilOption> { Name = name }).ShouldThrow(typeof(ValidationException));

            _context.Set<TProfilOption>().Where(t => string.IsNullOrWhiteSpace(t.Name)).Any().ShouldBe(false);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(2, "")]
        [InlineData(3, "  ")]
        [InlineData(4, "\t\n")]
        public void UpdateExceptionTest(int id, string name)
        {
            var result = _mediator.Send(new UpdateProfilOptionCommand<TProfilOption> { Id = id, Name = name }).ShouldThrow(typeof(FluentValidation.ValidationException));

            _context.Set<TProfilOption>().Where(t => string.IsNullOrWhiteSpace(t.Name)).Any().ShouldBe(false);
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("test", "  test  ")]
        public async Task IsUniqueOnCreate(string insertedInDb, string insertedInCommand)
        {
            var result = await _context.AddAsync(InstanciateEntity(insertedInDb));

            _context.SaveChanges();

            _mediator.Send(new CreateProfilOptionCommand<TProfilOption> { Name = insertedInCommand })
                .ShouldThrow(typeof(ValidationException));
        }

        [Theory]
        [InlineData("test", "test")]
        [InlineData("test", "  test  ")]
        public async Task IsUniqueOnUpdate(string insertedInDb, string insertedInCommand)
        {
            var result = await _context.AddAsync(InstanciateEntity(insertedInDb));

            _context.SaveChanges();

            var item = _context.Set<TProfilOption>().First(i => i.Name != insertedInDb);

            _mediator.Send(new UpdateProfilOptionCommand<TProfilOption> { Id = item.Id, Name = insertedInCommand })
                .ShouldThrow(typeof(ValidationException));
        }

        public abstract void CantDeleteRelatedData();
    }
}

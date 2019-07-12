using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using MediatR;

namespace ParentEspoir.Application.Test
{
    public class NoteTest : TestBase
    {
        private static readonly string GETNOTENAME = "Mon nom de note";
        private static readonly string GETNOTEBODY = "Voici ma note très longue et détaillé!";
        private static readonly int NOTETYPEID = 1;
        private static readonly string GETNOTESUPERVISOR = "The supervisor";
        private static readonly string SUPPERVISORTITLE = "The supervisor title";

        private static readonly string GETNOTENAME2 = "Mon super nom de note";
        private static readonly string GETNOTEBODY2 = "Voici ma note très longue et détaillé! 2";
        private static readonly int NOTETYPEID2 = 2;
        private static readonly string GETNOTESUPERVISOR2 = "The nicer supervisor";
        private static readonly string SUPPERVISORTITLE2 = "The better supervisor title";

        private ParentEspoirDbContext _context;
        public NoteTest()
        {
            _context = GetDbContext();

            _context.Add(new Customer
            {
                Address = "288, rue principale",
                City = "Saint-Alfred",
                Country = "Canada",
                CustomerDescription = new CustomerDescription(),
                DateOfBirth = new DateTime(1994, 01, 21),
                FirstName = "Frédéric",
                LastName = "Jacques",
                Phone = "418-774-9890",
                PostalCode = "G0M 1L0",
                Province = "Québec"
            });

            _context.SaveChanges();
        }

        [Fact]
        public void GetNoteTest()
        {
            var dateAdded = DateTime.Now;

            _context.Add(new Note
            {
                NoteName = GETNOTENAME,
                Body = GETNOTEBODY,
                NoteType = new NoteType { Id = NOTETYPEID },
                CreationDate = dateAdded,
                SupervisorName = GETNOTESUPERVISOR,
                SupervisorTitle = SUPPERVISORTITLE,
                Customer = _context.Customers.First()
            });

            _context.SaveChanges();

            var result = _mediator.Send(new GetNoteQuery
            {
                NoteId = _context.Notes.First().NoteId
            }).Result;

            result.ShouldBeOfType(typeof(GetNoteModel));

            result.NoteName.ShouldBe(GETNOTENAME);
            result.NoteId.ShouldBe(1);
            result.NoteTypeId.ShouldBe(NOTETYPEID);
            result.Body.ShouldBe(GETNOTEBODY);
            result.CreationDate.ShouldBe(dateAdded);
            result.CustomerId.ShouldBe(1);
            result.SupervisorName.ShouldBe(GETNOTESUPERVISOR);
            result.SupervisorTitle.ShouldBe(SUPPERVISORTITLE);
        }

        [Fact]
        public void GetListNoteTest()
        {
            var dateAdded = DateTime.Now;

            _context.Add(new Note
            {
                NoteName = GETNOTENAME,
                Body = GETNOTEBODY,
                NoteType = new NoteType { Id = NOTETYPEID },
                CreationDate = dateAdded,
                SupervisorName = GETNOTESUPERVISOR,
                SupervisorTitle = SUPPERVISORTITLE,
                Customer = _context.Customers.First()
            });

            _context.Add(new Note
            {
                NoteName = GETNOTENAME2,
                Body = GETNOTEBODY2,
                NoteType = new NoteType { Id = NOTETYPEID2 },
                CreationDate = dateAdded,
                SupervisorName = GETNOTESUPERVISOR2,
                SupervisorTitle = SUPPERVISORTITLE2,
                Customer = _context.Customers.First()
            });

            _context.SaveChanges();

            var response = _mediator.Send(new GetNoteListQuery
            { CustomerId = _context.Customers.Where(n => n.FirstName == "Frédéric").Single().CustomerId }).Result;


            response.GetType().GetInterfaces().ShouldContain(typeof(System.Collections.Generic.IEnumerable<Note>));
            response.Count().ShouldBe(2);

            response.ElementAt(0).Body.ShouldBe(GETNOTEBODY);
            response.ElementAt(0).CreationDate.ShouldBe(dateAdded);
            response.ElementAt(0).Customer.CustomerId.ShouldBe(_context.Customers.Where(n => n.FirstName == "Frédéric").Single().CustomerId);
            response.ElementAt(0).IsDelete.ShouldBe(false);
            response.ElementAt(0).NoteType.Id.ShouldBe(NOTETYPEID);
            response.ElementAt(0).SupervisorName.ShouldBe(GETNOTESUPERVISOR);
            response.ElementAt(0).SupervisorTitle.ShouldBe(SUPPERVISORTITLE);

            response.ElementAt(1).Body.ShouldBe(GETNOTEBODY2);
            response.ElementAt(1).CreationDate.ShouldBe(dateAdded);
            response.ElementAt(1).Customer.CustomerId.ShouldBe(_context.Customers.Where(n => n.FirstName == "Frédéric").Single().CustomerId);
            response.ElementAt(1).IsDelete.ShouldBe(false);
            response.ElementAt(1).NoteType.Id.ShouldBe(NOTETYPEID2);
            response.ElementAt(1).SupervisorName.ShouldBe(GETNOTESUPERVISOR2);
            response.ElementAt(1).SupervisorTitle.ShouldBe(SUPPERVISORTITLE2);
        }

        [Fact]
        public void CreateNoteTest()
        {
            _context.Add(new NoteType { Id = NOTETYPEID });

            _context.SaveChanges();

            var noteType = (_context.NoteTypes.Where(n => n.Id == NOTETYPEID).Single());

            var response = _mediator.Send(new CreateNoteCommand
            {
                NoteName = GETNOTENAME,
                Body = GETNOTEBODY,
                NoteTypeId = noteType.Id,
                SupervisorName = GETNOTESUPERVISOR,
                SupervisorTitle = SUPPERVISORTITLE,
                CustomerId = (_context.Customers.Where(n => n.FirstName == "Frédéric").Single().CustomerId),
            }).Result;

            response.ShouldBeOfType(typeof(Unit));

            var noteCreated = _context.Notes.Where(v => v.NoteName.Equals(GETNOTENAME)).SingleOrDefault();
            noteCreated.Body.ShouldBe(GETNOTEBODY);
            (noteCreated.CreationDate >= DateTime.Now - TimeSpan.FromSeconds(10)).ShouldBe(true);
            (noteCreated.CreationDate <= DateTime.Now).ShouldBe(true);
            noteCreated.NoteType.Id.ShouldBe(_context.NoteTypes.Where(n => n.Id == NOTETYPEID).Single().Id);
            noteCreated.NoteName.ShouldBe(GETNOTENAME);
            noteCreated.SupervisorName.ShouldBe(GETNOTESUPERVISOR);
            noteCreated.SupervisorTitle.ShouldBe(SUPPERVISORTITLE);
        }

        [Fact]
        public void DeleteNoteTest()
        {
            var dateAdded = DateTime.Now;

            _context.Add(new NoteType { Id = NOTETYPEID });

            _context.SaveChanges();

            var noteType = (_context.NoteTypes.Where(n => n.Id == NOTETYPEID).Single());

            _context.Add(new Note
            {
                NoteName = GETNOTENAME,
                Body = GETNOTEBODY,
                NoteType = noteType,
                CreationDate = dateAdded,
                SupervisorName = GETNOTESUPERVISOR,
                SupervisorTitle = SUPPERVISORTITLE,
                Customer = (_context.Customers.Where(n => n.FirstName == "Frédéric").Single()),
            });

            _context.SaveChanges();

            var result = _mediator.Send(new DeleteNoteCommand
            {
                NoteId = _context.Notes.Where(v => v.NoteName == GETNOTENAME).Single().NoteId,
            }).Result;

            _context.Notes.Where(v => v.NoteName == GETNOTENAME && v.IsDelete == true).Any().ShouldBe(true);
            _context.Notes.Where(v => v.NoteName == GETNOTENAME && v.IsDelete == false).SingleOrDefault().ShouldBe(null);
        }

        [Fact]
        public void AddNoteToCustomerWithoutNote()
        {
            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            });

            _context.SaveChanges();

            _mediator.Send(new CreateNoteCommand
            {
                Body = "My body",
                CustomerId = customer.Entity.CustomerId,
                NoteName = "My note name",
                SupervisorName = "Frederic",
                SupervisorTitle = "Jacques"
            }).Result.ShouldBeOfType(typeof(Unit));

            customer.Collection(c => c.Notes).Load();

            var customerNotes = customer.Entity.Notes;

            customerNotes.Count.ShouldBe(1);
        }

        [Fact]
        public void GetNoteToCustomerWithOneNote()
        {
            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            });

            _context.SaveChanges();

            _mediator.Send(new CreateNoteCommand
            {
                Body = "My body",
                CustomerId = customer.Entity.CustomerId,
                NoteName = "My note name",
                SupervisorName = "Frederic",
                SupervisorTitle = "Jacques"
            }).Result.ShouldBeOfType(typeof(Unit));

            customer.Collection(c => c.Notes).Load();

            var customerNotes = customer.Entity.Notes;

            customerNotes.Count.ShouldBe(1);

            var notesList = _mediator.Send(new GetNoteListQuery
            {
                CustomerId = customer.Entity.CustomerId
            }).Result;

            notesList.Count().ShouldBe(1);
        }
    }
}
using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application.Test
{
    public class ParticipantTest : TestBase
    {
        private ParentEspoirDbContext _context;
        public ParticipantTest()
        {
            _context = GetDbContext();
        }

        //[Fact]
        //public void GetListParticipantTest()
        //{
        //    var custo = _context.Add(new Customer
        //    {
        //        FirstName = "Fred",
        //        LastName = "Jacq",
        //        CustomerDescription = new CustomerDescription()
        //    });

        //    var atelier = _context.Add(new Workshop
        //    {
        //        Session = new Session()
        //    });

        //    _context.SaveChanges();

        //    var seance = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });

        //    seance.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo.Entity.CustomerId,
        //        SeanceId = seance.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(1, 0, 0)
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetParticipantListQuery { WorkshopId = atelier.Entity.WorkshopId }).Result;

        //    response.Count().ShouldBe(1);
        //    response.ElementAt(0).Name.ShouldBe("Fred Jacq");
        //    response.ElementAt(0).NbHourLate.ShouldBe(TimeSpan.FromHours(1));
        //}

        //[Fact]
        //public void GetListWithMoreThanOneParticipantTest()
        //{
        //    var custo1 = _context.Add(new Customer
        //    {
        //        FirstName = "Fred",
        //        LastName = "Jacq",
        //        CustomerDescription = new CustomerDescription()
        //    });

        //    var custo2 = _context.Add(new Customer
        //    {
        //        FirstName = "Jean",
        //        LastName = "François"
        //    });

        //    var atelier = _context.Add(new Workshop
        //    {
        //        Session = new Session()
        //    });

        //    _context.SaveChanges();

        //    var seance1 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });

        //    seance1.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo1.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(1,0,0)
        //    });

        //    seance1.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo2.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(0, 0, 0)
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetParticipantListQuery { WorkshopId = atelier.Entity.WorkshopId }).Result;

        //    response.Count().ShouldBe(2);
        //    response.ShouldContain(p => p.Name == "Fred Jacq" && p.NbHourLate.Equals(TimeSpan.FromHours(1)));
        //    response.ShouldContain(p => p.Name == "Jean François" && p.NbHourLate.Equals(TimeSpan.FromHours(0)));
        //}

        //[Fact]
        //public void GetListWithMoreThanOneInTowSeanceParticipantTest()
        //{
        //    var custo1 = _context.Add(new Customer
        //    {
        //        FirstName = "Fred",
        //        LastName = "Jacq",
        //        CustomerDescription = new CustomerDescription()
        //    });

        //    var custo2 = _context.Add(new Customer
        //    {
        //        FirstName = "Jean",
        //        LastName = "François"
        //    });

        //    var atelier = _context.Add(new Workshop
        //    {
        //        Session = new Session()
        //    });

        //    _context.SaveChanges();

        //    var seance1 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });
        //    var seance2 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });

        //    seance1.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo1.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(1, 0, 0)
        //    });

        //    seance2.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo2.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(0, 0, 0)
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetParticipantListQuery { WorkshopId = atelier.Entity.WorkshopId }).Result;

        //    response.Count().ShouldBe(2);
        //    response.ShouldContain(p => p.Name == "Fred Jacq" && p.NbHourLate.Equals(TimeSpan.FromHours(1)));
        //    response.ShouldContain(p => p.Name == "Jean François" && p.NbHourLate.Equals(TimeSpan.FromHours(0)));
        //}

        //[Fact]
        //public void GetListWithMoreThanOneInTowSeanceParticipantWithDeletedTest()
        //{
        //    var custo1 = _context.Add(new Customer
        //    {
        //        FirstName = "Fred",
        //        LastName = "Jacq",
        //        CustomerDescription = new CustomerDescription()
        //    });

        //    var custo2 = _context.Add(new Customer
        //    {
        //        FirstName = "Jean",
        //        LastName = "François"
        //    });

        //    var atelier = _context.Add(new Workshop
        //    {
        //        Session = new Session()
        //    });

        //    _context.SaveChanges();

        //    var seance1 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });
        //    var seance2 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId, IsDelete = true });

        //    seance1.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo1.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(1, 0, 0)
        //    });

        //    seance2.Entity.Participants.Add(new Participant
        //    {
        //        CustomerId = custo2.Entity.CustomerId,
        //        SeanceId = seance1.Entity.SeanceId,
        //        NbHourLate = new TimeSpan(0, 0, 0)
        //    });

        //    _context.SaveChanges();

        //    var response = _mediator.Send(new GetParticipantListQuery { WorkshopId = atelier.Entity.WorkshopId }).Result;

        //    response.Count().ShouldBe(1);
        //    response.ShouldContain(p => p.Name == "Fred Jacq" && p.NbHourLate.Equals(TimeSpan.FromHours(1)));
        //}

        [Fact]
        public void CreateParticipantTest()
        {
            var customer = _context.Add(new Customer
            {
                FirstName = "Fred",
                LastName = "Jacq",
                CustomerDescription = new CustomerDescription()
            });

            var atelier = _context.Add(new Workshop
            {
                Session = new Session()
            });

            _context.SaveChanges();

            var seance1 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });
            var seance2 = _context.Add(new Seance { WorkshopId = atelier.Entity.WorkshopId });

            _context.SaveChanges();

            _mediator.Send(new CreateParticipantCommand
            {
                CustomerId = customer.Entity.CustomerId,
                WorkshopId = atelier.Entity.WorkshopId
            }).Wait();

            var participants = _context.Participants.ToArray();
            participants.Count().ShouldBe(2);

            foreach (var participant in participants)
            {
                participant.CustomerId.ShouldBe(customer.Entity.CustomerId);
                participant.WorkshopId.ShouldBe(atelier.Entity.WorkshopId);

                (participant.SeanceId == seance1.Entity.SeanceId ||
                participant.SeanceId == seance2.Entity.SeanceId).ShouldBe(true);
            }
        }

        //[Fact]
        //public void UpdateParticipantTest()
        //{
        //    true.ShouldBe(false);
        //}

        //[Fact]
        //public void DeleteParticipantTest()
        //{
        //    true.ShouldBe(false);
        //}

    }
}
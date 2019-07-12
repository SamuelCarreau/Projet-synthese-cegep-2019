using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using MediatR;
using ParentEspoir.Domain.Enums;
using FluentValidation;

namespace ParentEspoir.Application.Test
{
    public class SeanceTest : TestBase
    {
        private ParentEspoirDbContext _context;

        private static readonly DateTime SEANCE_DATE = new DateTime(2019, 01, 01);
        private static readonly string SEANCE_DESCRIPTION = "Ma description";
        private static readonly string SEANCE_NAME = "The name";
        private static readonly TimeSpan SEANCE_TIMESPAN = TimeSpan.FromHours(3);

        private static readonly string CUSTOMER_FIRST_NAME = "Frédéric";
        private static readonly string CUSTOMER_LAST_NAME = "Jacques";

        public SeanceTest()
        {
            _context = GetDbContext();

            _context.Add(new Workshop());

            var custo = _context.Add(new Customer
            {
                FirstName = CUSTOMER_FIRST_NAME,
                LastName = CUSTOMER_LAST_NAME,
                CustomerDescription = new CustomerDescription()
            }).Entity;

            custo.CustomerActivations.Add(new CustomerActivation());

            _context.SaveChanges();
        }

        //[Fact]
        //public void GetSeanceTest()
        //{
        //    var seance = _context.Add(new Seance
        //    {
        //        SeanceDate = SEANCE_DATE,
        //        SeanceDescription = SEANCE_DESCRIPTION,
        //        SeanceName = SEANCE_NAME,
        //        SeanceTimeSpan = SEANCE_TIMESPAN,
        //        WorkshopId = _context.Workshops.First().WorkshopId
        //    });

        //    _context.SaveChanges();

        //    var participantEntity = _context.Add(new Participant
        //    {
        //        CustomerId = _context.Customers.First().CustomerId,
        //        SeanceId = _context.Seances.First().SeanceId,
        //        NbHourLate = new TimeSpan(0, 0, 0),
        //        Status = null
        //    });

        //    _context.SaveChanges();

        //    var result = _mediator.Send(new GetSeanceQuery
        //    {
        //        SeanceId = seance.Entity.SeanceId
        //    }).Result;

        //    result.SeanceId.ShouldBe(seance.Entity.SeanceId);
        //    result.SeanceName.ShouldBe(SEANCE_NAME);
        //    result.SeanceDescription.ShouldBe(SEANCE_DESCRIPTION);
        //    result.SeanceTimeSpan.ShouldBe(SEANCE_TIMESPAN);
        //    result.SeanceDate.ShouldBe(SEANCE_DATE);

        //    result.Participants.Count().ShouldBe(1);
        //    var participant = result.Participants.ElementAt(0);
        //    participant.CustomerId.ShouldBe(_context.Customers.First().CustomerId);
        //    participant.CustomerName.ShouldBe(CUSTOMER_FIRST_NAME + " " + CUSTOMER_LAST_NAME);
        //    participant.NbHourLate.ShouldBe(new TimeSpan(0,0,0));
        //    participant.ParticiantId.ShouldBe(participantEntity.Entity.ParticipantId);
        //    participant.ParticipationStatus.ShouldBe(null);
        //    participant.SeanceId.ShouldBe(seance.Entity.SeanceId);
        //}

        [Fact]
        public void CantGetDeletedSeance()
        {
            var deletedSeance = _context.Add(new Seance
            {
                SeanceDate = SEANCE_DATE,
                SeanceDescription = SEANCE_DESCRIPTION,
                SeanceName = SEANCE_NAME,
                SeanceTimeSpan = SEANCE_TIMESPAN,
                IsDelete = true,
                WorkshopId = _context.Workshops.First().WorkshopId
            });

            _context.SaveChanges();

            var result = _mediator.Send(new GetSeanceQuery
            {
                SeanceId = deletedSeance.Entity.SeanceId
            }).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void CantGetUnexistingSeance()
        {
            _mediator.Send(new GetSeanceQuery { SeanceId = -100 }).ShouldThrow(typeof(ValidationException));
        }

        //[Fact]
        //public void GetSeanceWhitOneDeleteInDatabaseTest()
        //{
        //    var seance = _context.Add(new Seance
        //    {
        //        SeanceDate = SEANCE_DATE,
        //        SeanceDescription = SEANCE_DESCRIPTION,
        //        SeanceName = SEANCE_NAME,
        //        SeanceTimeSpan = SEANCE_TIMESPAN,
        //        WorkshopId = _context.Workshops.First().WorkshopId
        //    });

        //    _context.SaveChanges();

        //    _context.Add(new Participant
        //    {
        //        IsDelete = true,
        //        CustomerId = _context.Customers.First().CustomerId,
        //        NbHourLate = new TimeSpan(0,0,0),
        //        Status = ParticipationStatus.Absent,
        //        SeanceId = seance.Entity.SeanceId,
        //    });

        //    var participantEntity = _context.Add(new Participant
        //    {
        //        CustomerId = _context.Customers.First().CustomerId,
        //        SeanceId = _context.Seances.First().SeanceId,
        //        NbHourLate = new TimeSpan(1, 0, 0),
        //        Status = null
        //    });

        //    _context.SaveChanges();

        //    var result = _mediator.Send(new GetSeanceQuery
        //    {
        //        SeanceId = seance.Entity.SeanceId
        //    }).Result;

        //    result.SeanceId.ShouldBe(seance.Entity.SeanceId);
        //    result.SeanceName.ShouldBe(SEANCE_NAME);
        //    result.SeanceDescription.ShouldBe(SEANCE_DESCRIPTION);
        //    result.SeanceTimeSpan.ShouldBe(SEANCE_TIMESPAN);
        //    result.SeanceDate.ShouldBe(SEANCE_DATE);

        //    result.Participants.Count().ShouldBe(1);
        //    var participant = result.Participants.ElementAt(0);
        //    participant.CustomerId.ShouldBe(_context.Customers.First().CustomerId);
        //    participant.CustomerName.ShouldBe(CUSTOMER_FIRST_NAME + " " + CUSTOMER_LAST_NAME);
        //    participant.NbHourLate.ShouldBe(new TimeSpan(0,0,0));
        //    participant.ParticiantId.ShouldBe(participantEntity.Entity.ParticipantId);
        //    participant.ParticipationStatus.ShouldBe(null);
        //    participant.SeanceId.ShouldBe(seance.Entity.SeanceId);
        //}

        //[Fact]
        //public void CreateSeanceTest()
        //{
        //    _context.Seances.Any().ShouldBe(false);

        //    _mediator.Send(new CreateSeanceCommand
        //    {
        //        SeanceDate = SEANCE_DATE,
        //        SeanceDescription = SEANCE_DESCRIPTION,
        //        SeanceName = SEANCE_NAME,
        //        SeanceTimeSpan = TimeSpan.FromHours(3),
        //        WorkshopId = _context.Workshops.First().WorkshopId
        //    }).Result.ShouldBeOfType(typeof(Unit));

        //    var seance = _context.Seances.First();

        //    seance.IsDelete.ShouldBe(false);
        //    seance.SeanceDate.ShouldBe(new DateTime(2019, 01, 01));
        //    seance.SeanceDescription.ShouldBe("Ma description");
        //    seance.SeanceName.ShouldBe("The name");
        //    seance.SeanceTimeSpan.ShouldBe(TimeSpan.FromHours(3));
        //    seance.WorkshopId.ShouldBe(_context.Workshops.First().WorkshopId);
        //}

        [Theory]
        [InlineData("2019-01-10", "", "A name", "3", 45)]
        [InlineData("2019-01-10", "", "", "3", 1)]
        [InlineData("2019-01-10", "", null, "3", 1)]
        public void CreateSeanceException(string startDate, string description, string name, string lenght, int workshopId)
        {
            _mediator.Send(new CreateSeanceCommand
            {
                SeanceDate = DateTime.Parse(startDate),
                SeanceDescription = description,
                SeanceName = name,
                SeanceTimeSpan = TimeSpan.FromHours(int.Parse(lenght)),
                WorkshopId = workshopId
            }).ShouldThrow(typeof(ValidationException));
        }

        //[Fact]
        //public void UpdateSeanceTest()
        //{
        //    var seance = _context.Add(new Seance
        //    {
        //        SeanceDate = new DateTime(2010, 02, 02),
        //        SeanceDescription = "Une description",
        //        SeanceName = "THe name",
        //        SeanceTimeSpan = TimeSpan.FromHours(2)
        //    }).Entity;

        //    _context.SaveChanges();

        //    _mediator.Send(new UpdateSeanceCommand
        //    {
        //        SeanceDate = new DateTime(2012, 03, 03),
        //        SeanceDescription = "",
        //        SeanceTimeSpan = TimeSpan.FromHours(3),
        //        SeanceName = "The new name",
        //        SeanceId = seance.SeanceId
        //    }).Wait();

        //    seance.SeanceDate.ShouldBe(new DateTime(2012, 03, 03));
        //    seance.SeanceDescription.ShouldBe("");
        //    seance.SeanceTimeSpan.ShouldBe(TimeSpan.FromHours(3));
        //    seance.SeanceName.ShouldBe("The new name");
        //}

        //[Fact]
        //public void DeleteSeanceTest()
        //{
        //    var seance = _context.Add(new Seance
        //    {
        //        SeanceDate = new DateTime(2010, 02, 02),
        //        SeanceDescription = "Une description",
        //        SeanceName = "THe name",
        //        SeanceTimeSpan = TimeSpan.FromHours(2)
        //    }).Entity;

        //    _context.SaveChanges();

        //    _mediator.Send(new DeleteSeanceCommand
        //    {
        //        SeanceId = seance.SeanceId
        //    }).Wait();

        //    seance.IsDelete.ShouldBe(true);
        //}
    }
}
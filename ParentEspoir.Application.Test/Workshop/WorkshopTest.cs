using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using ParentEspoir.Domain.Enums;
using FluentValidation;
using MediatR;

namespace ParentEspoir.Application.Test
{
    public class WorkshopTest : TestBase
    {
        private static readonly string WORKSHOP_NAME = "WORKSHOP_NAME";
        private static readonly string WORKSHOP_DESCRIPTION = "MY_DESCRIPTION";
        private static readonly string WORKSHOP_TYPE = "MY_TYPE";
        private static readonly string SEANCE1_NAME = "SEANCE1_NAME";
        private static readonly string SEANCE1_DESCRIPTION = "My seance description";
        private static readonly DateTime SEANCE1_DATETIME = new DateTime(2019, 03, 13);
        private static readonly TimeSpan SEANCE1_TIMESPAN = new TimeSpan(0, 3, 0, 0, 0);
        private static readonly bool ISOPENSTATE = false;

        private static readonly DateTime WORKSHOP_ENDDATE = new DateTime(2019, 05, 05);
        private static readonly DateTime WORKSHOP_STATDATE = new DateTime(2019, 04, 04);

        private ParentEspoirDbContext _context;
        public WorkshopTest()
        {
            _context = GetDbContext();
        }

        [Fact]
        public void GetWorkshopTest()
        {
            var workshop = _context.Add(new Workshop
            {
                EndDate = WORKSHOP_ENDDATE,
                Session = new Session { Year = 2019, Season = Season.Winter },
                StartDate = WORKSHOP_STATDATE,
                WorkshopDescription = WORKSHOP_DESCRIPTION,
                WorkshopName = WORKSHOP_NAME,
                WorkshopType = new WorkshopType { Name = WORKSHOP_TYPE },
                IsOpen = ISOPENSTATE
            });

            _context.SaveChanges();

            _context.Add(new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            });

            _context.SaveChanges();

            var result = _mediator.Send(new GetWorkshopQuery
            {
                WorkshopId = workshop.Entity.WorkshopId
            }).Result;

            result.ShouldBeOfType(typeof(GetWorkshopModel));

            result.EndDate.ShouldBe(WORKSHOP_ENDDATE);
            result.StartDate.ShouldBe(WORKSHOP_STATDATE);
            result.WorkshopDescription.ShouldBe(WORKSHOP_DESCRIPTION);
            result.WorkshopId.ShouldBe(workshop.Entity.WorkshopId);
            result.WorkshopTypeName.ShouldBe(WORKSHOP_TYPE);
            result.Seances.Count.ShouldBe(1);
            result.IsOpen.ShouldBe(ISOPENSTATE);

            var seance = result.Seances.ElementAt(0);
            seance.SeanceId.ShouldBeGreaterThan(0);
            seance.SeanceDate.ShouldBe(SEANCE1_DATETIME);
            seance.SeanceName.ShouldBe(SEANCE1_NAME);
            seance.SeanceTimeSpan.ShouldBe(SEANCE1_TIMESPAN);
        }

        [Fact]
        public void GetWorkshopTestWithDeletedSeance()
        {
            var workshop = _context.Add(new Workshop
            {
                EndDate = WORKSHOP_ENDDATE,
                Session = new Session { Year = 2019, Season = Season.Winter },
                StartDate = WORKSHOP_STATDATE,
                WorkshopDescription = WORKSHOP_DESCRIPTION,
                WorkshopName = WORKSHOP_NAME,
                WorkshopType = new WorkshopType { Name = WORKSHOP_TYPE }
            });

            _context.SaveChanges();

            _context.Add(new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            });

            _context.Add(new Seance
            {
                WorkshopId = workshop.Entity.WorkshopId,
                IsDelete = true
            });

            _context.SaveChanges();

            var result = _mediator.Send(new GetWorkshopQuery
            {
                WorkshopId = workshop.Entity.WorkshopId
            }).Result;

            result.ShouldBeOfType(typeof(GetWorkshopModel));

            result.EndDate.ShouldBe(WORKSHOP_ENDDATE);
            result.StartDate.ShouldBe(WORKSHOP_STATDATE);
            result.WorkshopDescription.ShouldBe(WORKSHOP_DESCRIPTION);
            result.WorkshopId.ShouldBe(workshop.Entity.WorkshopId);
            result.WorkshopTypeName.ShouldBe(WORKSHOP_TYPE);
            result.Seances.Count.ShouldBe(1);

            var seance = result.Seances.ElementAt(0);
            seance.SeanceId.ShouldBeGreaterThan(0);
            seance.SeanceDate.ShouldBe(SEANCE1_DATETIME);
            seance.SeanceName.ShouldBe(SEANCE1_NAME);
            seance.SeanceTimeSpan.ShouldBe(SEANCE1_TIMESPAN);
        }

        [Fact]
        public void CantGetWorkshopWithBadId()
        {
            _mediator.Send(new GetWorkshopQuery
            {
                WorkshopId = 123
            }).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void GetSeanceDuration()
        {
            var workshop = _context.Add(new Workshop
            {
                EndDate = WORKSHOP_ENDDATE,
                Session = new Session { Year = 2019, Season = Season.Winter },
                StartDate = WORKSHOP_STATDATE,
                WorkshopDescription = WORKSHOP_DESCRIPTION,
                WorkshopName = WORKSHOP_NAME,
                WorkshopType = new WorkshopType { Name = WORKSHOP_TYPE }
            });

            _context.SaveChanges();

            _context.AddRange(new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            },

            new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            },

            new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            },

            new Seance
            {
                SeanceDate = SEANCE1_DATETIME,
                SeanceDescription = SEANCE1_DESCRIPTION,
                SeanceName = SEANCE1_NAME,
                SeanceTimeSpan = SEANCE1_TIMESPAN,
                WorkshopId = workshop.Entity.WorkshopId
            });

            _context.SaveChanges();

            var result = _mediator.Send(new GetWorkshopQuery
            {
                WorkshopId = workshop.Entity.WorkshopId
            }).Result;

            result.SeancesDuration.ShouldBe(new TimeSpan(12, 0, 0));
        }

        [Fact]
        public void CantGetWorkshopWithGoodIdButIsDeleteTrue()
        {
            var workshop = _context.Add(new Workshop
            {
                IsDelete = true
            });

            _context.SaveChanges();

            _mediator.Send(new GetWorkshopQuery
            {
                WorkshopId = workshop.Entity.WorkshopId
            }).ShouldThrow(typeof(ValidationException));
        }

        //[Fact]
        //public void CreateWorkshopTest()
        //{
        //    var session = _context.Add(new Session { Season = Season.Summer, Year = 2019 });
        //    var workshopType = _context.Add(new WorkshopType { Name = "Type" });
        //    _context.SaveChanges();

        //    _mediator.Send(new CreateWorkshopCommand
        //    {
        //        DateTimeFirstSeance = new DateTime(2019, 05, 05, 13, 30, 0),
        //        EndDate = new DateTime(2019, 08, 08),
        //        IntervalNbDays = 7,
        //        SeanceCount = 7,
        //        SeanceLenght = TimeSpan.FromHours(3),
        //        SessionId = session.Entity.SessionId,
        //        StartDate = new DateTime(2019, 05, 05),
        //        WorkshopDescription = "A description",
        //        WorkshopName = "Test workshop",
        //        WorkshopTypeId = workshopType.Entity.Id
        //    }).Result.ShouldBeOfType(typeof(Unit));

        //    var result = _mediator.Send(new GetWorkshopQuery
        //    {
        //        WorkshopId = _context.Workshops.First().WorkshopId
        //    }).Result;

        //    result.EndDate.ShouldBe(new DateTime(2019, 08, 08));
        //    result.StartDate.ShouldBe(new DateTime(2019, 05, 05));
        //    result.WorkshopDescription.ShouldBe("A description");
        //    result.WorkshopId.ShouldBe(_context.Workshops.First().WorkshopId);
        //    result.WorkshopName.ShouldBe("Test workshop");
        //    result.WorkshopTypeName.ShouldBe("Type");
        //    result.WorkshopTypeId.ShouldBe(workshopType.Entity.Id);

        //    var seances = result.Seances;
        //    seances.Count.ShouldBe(7);

        //    DateTime dateIncrement = new DateTime(2019, 05, 05, 13, 30, 0);
        //    int idName = 1;
        //    foreach (var seance in seances)
        //    {
        //        seance.SeanceDate.ShouldBe(dateIncrement);
        //        seance.SeanceTimeSpan.ShouldBe(TimeSpan.FromHours(3));
        //        seance.SeanceName.ShouldBe($"Seance {idName++}");

        //        dateIncrement += TimeSpan.FromDays(7);
        //    }
        //}

        //[Fact]
        //public void UpdateWorkshopTest()
        //{
        //    var typeBefore = _context.Add(new WorkshopType { Name = "Before" }).Entity;
        //    var typeAfter = _context.Add(new WorkshopType { Name = "After" }).Entity;

        //    var workshop = _context.Add(new Workshop
        //    {
        //        Session = new Session(),
        //        WorkshopName = "The name",
        //        StartDate = new DateTime(1994, 01, 22),
        //        EndDate = new DateTime(2084, 01, 21),
        //        WorkshopTypeId = typeBefore.Id
        //    }).Entity;

        //    _context.SaveChanges();

        //    _mediator.Send(new UpdateWorkshopCommand
        //    {
        //        EndDate = new DateTime(2094, 01, 21),
        //        StartDate = new DateTime(1994, 01, 21),
        //        WorkshopDescription = "Une description",
        //        WorkshopId = workshop.WorkshopId,
        //        WorkshopName = "The new name",
        //        WorkshopTypeId = typeAfter.Id
        //    }).Wait();

        //    workshop.StartDate.ShouldBe(new DateTime(1994, 01, 21));
        //    workshop.WorkshopDescription.ShouldBe("Une description");
        //    workshop.WorkshopName.ShouldBe("The new name");
        //    workshop.WorkshopTypeId.ShouldBe(typeAfter.Id);
        //    workshop.EndDate.ShouldBe(new DateTime(2094, 01, 21));
        //}

        [Fact]
        public void DeleteWorkshopTest()
        {
            var workshop = _context.Add(new Workshop
            {
                Session = new Session()
            }).Entity;

            _context.SaveChanges();

            _context.Workshops
                .Where(w => w.WorkshopId == workshop.WorkshopId && w.IsDelete == false)
                .SingleOrDefault().ShouldNotBeNull();

            _context.Workshops
               .Where(w => w.WorkshopId == workshop.WorkshopId && w.IsDelete == true)
               .SingleOrDefault().ShouldBeNull();

            _mediator.Send(new DeleteWorkshopCommand
            {
                WorkshopId = workshop.WorkshopId
            }).Wait();

            _context.Workshops
                .Where(w => w.WorkshopId == workshop.WorkshopId && w.IsDelete == false)
                .SingleOrDefault().ShouldBeNull();

            _context.Workshops
               .Where(w => w.WorkshopId == workshop.WorkshopId && w.IsDelete == true)
               .SingleOrDefault().ShouldNotBeNull();
        }
    }
}
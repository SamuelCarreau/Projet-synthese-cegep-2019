using ParentEspoir.Persistence;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using MediatR;
using ParentEspoir.Domain.Enums;
using FluentValidation;
using System;
using ParentEspoir.Domain.Constants;

namespace ParentEspoir.Application.Test
{
    public class SessionTest : TestBase
    {
        private ParentEspoirDbContext _context;
        public SessionTest()
        {
            _context = GetDbContext();
        }

        [Fact]
        public void GetListSessionTest()
        {
            var session2017Fall = new Session { Season = Season.Fall, Year = 2017 };
            var session2018Winter = new Session { Season = Season.Winter, Year = 2018 };
            var session2019Summer = new Session { Season = Season.Summer, Year = 2019 };
            var session2020Spring = new Session { Season = Season.Spring, Year = 2020 };
            var session2021Summer = new Session { Season = Season.Summer, Year = 2021 };
            var session2022Summer = new Session { Season = Season.Summer, Year = 2022 };

            _context.Sessions.AddRange(
                session2017Fall,
                session2018Winter,
                session2019Summer,
                session2020Spring,
                session2021Summer,
                session2022Summer
                );
            _context.SaveChanges();

            var sessionsResult = _mediator.Send(new GetSessionListQuery()).Result;

            sessionsResult.Sessions.Count().ShouldBe(6);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Automne" && s.Year == 2017);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Hiver" && s.Year == 2018);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2019);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Printemps" && s.Year == 2020);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2021);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2022);
        }

        [Fact]
        public void GetListOfNotDeletedSessionTest()
        {
            var session2017Fall = new Session { Season = Season.Fall, Year = 2017, IsDelete = true };
            var session2018Winter = new Session { Season = Season.Winter, Year = 2018, IsDelete = true };
            var session2019Summer = new Session { Season = Season.Summer, Year = 2019 };
            var session2020Spring = new Session { Season = Season.Spring, Year = 2020 };
            var session2021Summer = new Session { Season = Season.Summer, Year = 2021 };
            var session2022Summer = new Session { Season = Season.Summer, Year = 2022 };

            _context.Sessions.AddRange(
                session2017Fall,
                session2018Winter,
                session2019Summer,
                session2020Spring,
                session2021Summer,
                session2022Summer
                );
            _context.SaveChanges();

            var sessionsResult = _mediator.Send(new GetSessionListQuery()).Result;

            sessionsResult.Sessions.Count().ShouldBe(4);
            sessionsResult.Sessions.ShouldNotContain(s => s.SeasonName == "Automne" && s.Year == 2017);
            sessionsResult.Sessions.ShouldNotContain(s => s.SeasonName == "Hiver" && s.Year == 2018);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2019);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Printemps" && s.Year == 2020);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2021);
            sessionsResult.Sessions.ShouldContain(s => s.SeasonName == "Été" && s.Year == 2022);
        }

        [Theory]
        [InlineData(Season.Fall, 2019)]
        [InlineData(Season.Spring, 2018)]
        [InlineData(Season.Summer, 2021)]
        [InlineData(Season.Winter, 2001)]
        public void CreateSessionTest(Season season, int year)
        {
            _context.Sessions.Any().ShouldBe(false);

            _mediator.Send(new CreateSessionCommand
            {
                Season = season,
                Year = year
            }).Result.ShouldBeOfType(typeof(Unit));

            var session = _context.Sessions.First();

            session.Season.ShouldBe(season);
            session.Year.ShouldBe(year);

            switch (season)
            {
                case (Season.Winter):
                    session.StartDate.ShouldBe(new DateTime(year, SessionConstant.WINTER_START_MONTH, SessionConstant.WINTER_START_DAY));
                    break;
                case (Season.Spring):
                    session.StartDate.ShouldBe(new DateTime(year, SessionConstant.SPRING_START_MONTH, SessionConstant.SPRING_START_DAY));
                    break;
                case (Season.Summer):
                    session.StartDate.ShouldBe(new DateTime(year, SessionConstant.SUMMER_START_MONTH, SessionConstant.SUMMER_START_DAY));
                    break;
                case (Season.Fall):
                    session.StartDate.ShouldBe(new DateTime(year, SessionConstant.FALL_START_MONTH, SessionConstant.FALL_START_DAY));
                    break;
            }
        }

        [Fact]
        public void CanAddSessionSameYearDifferentSeason()
        {
            _context.Sessions.Add(new Session
            {
                Season = Season.Fall,
                Year = 2019
            });

            _context.SaveChanges();

            _context.Sessions.Any(s => s.Year == 2019 && s.Season == Season.Spring).ShouldBe(false);

            _mediator.Send(new CreateSessionCommand
            {
                Season = Season.Spring,
                Year = 2019
            });

            _context.Sessions.Any(s => s.Year == 2019 && s.Season == Season.Spring).ShouldBe(true);
        }

        [Fact]
        public void CanAddSessionSameSeasonDifferentYear()
        {
            _context.Sessions.Add(new Session
            {
                Season = Season.Spring,
                Year = 2018
            });

            _context.SaveChanges();

            _context.Sessions.Any(s => s.Year == 2019 && s.Season == Season.Spring).ShouldBe(false);

            _mediator.Send(new CreateSessionCommand
            {
                Season = Season.Spring,
                Year = 2019
            });

            _context.Sessions.Any(s => s.Year == 2019 && s.Season == Season.Spring).ShouldBe(true);
        }

        [Theory]
        [InlineData(Season.Fall, 2019)]
        [InlineData(Season.Summer, 2018)]
        public void CreateSessionException(Season season, int year)
        {
            _context.Add(new Session
            {
                Year = year,
                Season = season
            });

            _context.SaveChanges();

            _mediator.Send(new CreateSessionCommand
            {
                Season = season,
                Year = year
            }).ShouldThrow(typeof(ValidationException));
        }

        [Theory]
        [InlineData(Season.Fall, 2019)]
        [InlineData(Season.Spring, 2018)]
        [InlineData(Season.Summer, 2021)]
        [InlineData(Season.Winter, 2001)]
        public void UpdateSessionTest(Season season, int year)
        {
            var session = new Session
            {
                Year = 2004,
                Season = Season.Winter
            };

            _context.Add(session);

            _context.SaveChanges();

            _mediator.Send(new UpdateSessionCommand
            {
                SessionId = session.SessionId,
                Season = season,
                Year = year
            }).Result.ShouldBeOfType(typeof(Unit));

            session.Year.ShouldBe(year);
            session.Season.ShouldBe(season);
        }

        [Fact]
        public void CanUpdateSessionSameSeasonDifferentYear()
        {
            var session = _context.Sessions.Add(new Session
            {
                Season = Season.Spring,
                Year = 2018
            });

            _context.SaveChanges();

            _context.Sessions.Any(s => s.Year == 2018 && s.Season == Season.Spring).ShouldBe(true);

            _mediator.Send(new UpdateSessionCommand
            {
                SessionId = session.Entity.SessionId,
                Season = Season.Spring,
                Year = 2019
            }).Wait();

            _context.Sessions.Any(s => s.SessionId == session.Entity.SessionId && s.Year == 2019 && s.Season == Season.Spring).ShouldBe(true);
        }

        [Fact]
        public void CanUpdateSessionSameYearDifferentSeason()
        {
            var session = _context.Sessions.Add(new Session
            {
                Season = Season.Spring,
                Year = 2018
            });

            _context.SaveChanges();

            _context.Sessions.Any(s => s.Year == 2018 && s.Season == Season.Spring).ShouldBe(true);

            _mediator.Send(new UpdateSessionCommand
            {
                SessionId = session.Entity.SessionId,
                Season = Season.Spring,
                Year = 2019
            }).Wait();

            _context.Sessions.Any(s => s.Year == 2019 && s.Season == Season.Spring).ShouldBe(true);
        }

        [Theory]
        [InlineData(Season.Fall, 2019)]
        [InlineData(Season.Summer, 2018)]
        public void UpdateSessionException(Season season, int year)
        {
            var session = _context.Add(new Session
            {
                Year = 2004,
                Season = Season.Fall
            });

            _context.Add(new Session
            {
                Year = year,
                Season = season
            });

            _context.SaveChanges();

            _mediator.Send(new UpdateSessionCommand
            {
                SessionId = session.Entity.SessionId,
                Season = season,
                Year = year
            }).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void DeleteSessionTest()
        {
            var session = _context.Add(new Session
            {
                Season = Season.Fall,
                Year = 2019
            });

            _context.SaveChanges();
            _context.Sessions.Any(s => s.IsDelete == true).ShouldBe(false);

            _mediator.Send(new DeleteSessionCommand
            {
                SessionId = session.Entity.SessionId
            }).Result.ShouldBeOfType(typeof(Unit));

            _context.Sessions.Any(s => s.IsDelete == true).ShouldBe(true);
        }

        [Fact]
        public void CanDeleteSessionWhenWorkshopHasBeenDeleted()
        {
            var session = _context.Add(new Session
            {
                Season = Season.Fall,
                Year = 2019
            }).Entity;

            _context.SaveChanges();

            _context.Add(new Workshop
            {
                IsDelete = true,
                SessionId = session.SessionId
            });

            _context.SaveChanges();
            _context.Sessions.Any(s => s.IsDelete == true).ShouldBe(false);

            _mediator.Send(new DeleteSessionCommand
            {
                SessionId = session.SessionId
            }).Result.ShouldBeOfType(typeof(Unit));

            _context.Sessions.Any(s => s.IsDelete == true).ShouldBe(true);
        }

        [Fact]
        public void DeleteSessionExceptionTest()
        {
            _mediator.Send(new DeleteSessionCommand
            {
                SessionId = 1001
            }).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void DeleteSessionWithRelatedDataTest()
        {
            var session = new Session
            {
                Year = 1000,
                Season = Season.Fall
            };

            session.Workshops.Add(new Workshop());

            _context.Add(session);

            _context.SaveChanges();

            _mediator.Send(new DeleteSessionCommand
            {
                SessionId = session.SessionId
            }).ShouldThrow(typeof(ValidationException));
        }

        [Fact]
        public void UpdateSessionWithDeleteSessionWithSameInfo()
        {
            var session = _context.Add(new Session
            {
                Season = Season.Fall,
                Year = 2018
            });

            _context.Add(new Session
            {
                IsDelete = true,
                Season = Season.Fall,
                Year = 2019
            });

            _context.SaveChanges();

            var result = _mediator.Send(new UpdateSessionCommand
            {
                SessionId = session.Entity.SessionId,
                Season = Season.Fall,
                Year = 2019
            }).Result;

            session.Entity.Year.ShouldBe(2019);
            session.Entity.Season.ShouldBe(Season.Fall);
        }

        [Fact]
        public void CanAddSessionWithDeleteSessionWithSameInfo()
        {
            _context.Add(new Session
            {
                IsDelete = true,
                Season = Season.Fall,
                Year = 2019
            });

            _context.SaveChanges();

            var result = _mediator.Send(new CreateSessionCommand
            {
                Season = Season.Fall,
                Year = 2019
            }).Result;

            _context.Sessions.Count(s => s.Season == Season.Fall && s.Year == 2019).ShouldBe(2);
            _context.Sessions.Count(s => s.IsDelete == true).ShouldBe(1);
            _context.Sessions.Count(s => s.IsDelete == false).ShouldBe(1);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(Season.Fall, null)]
        [InlineData(null, 2019)]
        public void CannotAddSessionWithoutInfo(Season? season, int? year)
        {
            _mediator.Send(new CreateSessionCommand
            {
                Season = season,
                Year = year
            }).ShouldThrow(typeof(ValidationException));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(Season.Fall, null)]
        [InlineData(null, 2019)]
        public void CannotUpdateSessionWithoutInfo(Season? season, int? year)
        {
            _mediator.Send(new UpdateSessionCommand
            {
                Season = season,
                Year = year
            }).ShouldThrow(typeof(ValidationException));
        }
    }
}
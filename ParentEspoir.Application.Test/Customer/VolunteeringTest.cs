using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using MediatR;

namespace ParentEspoir.Application.Test
{
    public class VolunteeringTest : TestBase
    {
        private ParentEspoirDbContext _context;
        private static readonly string VOLUNTEERING_TITLE = "Benedictine";

        private static readonly string VOLUNTEERING_TITLE2 = "Pas d'oeuf";

        private Customer Customer { get; set; }
        private Customer Customer2 { get; set; }

        private VolunteeringType VolunteeringType { get; set; }
        private VolunteeringType VolunteeringType2 { get; set; }

        private Volunteering MainVolonteering { get; set; }
        private static int MainVolonteeringAmount = 3;

        public VolunteeringTest()
        {

            _context = GetDbContext();

            Customer = new Customer { FirstName = "Yoo" };
            Customer2 = new Customer { FirstName = "Aaargh" };
            VolunteeringType = new VolunteeringType { Name = "Freeee" };
            VolunteeringType2 = new VolunteeringType { Name = "Freeee" };


            _context.Add(Customer);
            _context.Add(Customer2);
            _context.Add(VolunteeringType);
            _context.Add(VolunteeringType2);

            MainVolonteering = new Volunteering
            {
                Title = VOLUNTEERING_TITLE,
                Date = new DateTime(2012, 3, 29),
                Amount = 3,
                Details = "",
                HourCount = 8,
                Acknowledgment = "WantSome",
                Customer = Customer,
                IsDelete = false,
                Type = VolunteeringType
            };

            _context.Add(MainVolonteering);

            _context.SaveChanges();
        }

        [Fact]
        public void GetVolunteeringTest()
        {
            var response = _mediator.Send(new GetVolunteeringQuery
            {
                VolunteeringId = _context.Volunteerings.Where(v => v.Title == VOLUNTEERING_TITLE).Single().VolunteeringId
            }).Result;

            response.ShouldBeOfType(typeof(GetVolunteeringModel));

            response.Amount.ShouldBe(3);
            response.Title.ShouldBe(VOLUNTEERING_TITLE);
            response.VolunteeringTypeName.ShouldBe(VolunteeringType.Name);
            response.Date.ShouldBe(new DateTime(2012, 3, 29));
            response.Details.ShouldBe("");
            response.HourCount.ShouldBe(8);
            response.Acknowledgment.ShouldBe("WantSome");
            response.CustomerId.ShouldBe(Customer.CustomerId);
        }

        [Fact]
        public void GetListVolunteeringTest()
        {
            _context.Add(new Volunteering
            {
                Title = VOLUNTEERING_TITLE2,
                Date = new DateTime(2014, 3, 29),
                Amount = 5,
                Details = "This is a detail",
                HourCount = 9,
                Acknowledgment = "WantSomeNuts",
                Customer = Customer,
                IsDelete = false,
                Type = VolunteeringType2
            });

            _context.SaveChanges();

            var response = _mediator.Send(new GetVolunteeringListQuery
            { CustomerId = _context.Customers.Where(c => c.FirstName == "Yoo").Single().CustomerId }).Result;

            response.GetType().GetInterfaces().ShouldContain(typeof(System.Collections.Generic.IEnumerable<Volunteering>));
            response.Count().ShouldBe(2);
                
            response.ElementAt(0).Amount.ShouldBe(3);
            response.ElementAt(0).Title.ShouldBe(VOLUNTEERING_TITLE);
            response.ElementAt(0).Type.Name.ShouldBe("Freeee");
            response.ElementAt(0).Date.ShouldBe(new DateTime(2012, 3, 29));
            response.ElementAt(0).Details.ShouldBe("");
            response.ElementAt(0).HourCount.ShouldBe(8);
            response.ElementAt(0).Acknowledgment.ShouldBe("WantSome");
            response.ElementAt(0).Customer.CustomerId.ShouldBe(Customer.CustomerId);
            response.ElementAt(0).VolonteeringTypeId.ShouldBe(VolunteeringType.Id);
            response.ElementAt(0).IsDelete.ShouldBe(false);

            response.ElementAt(1).Amount.ShouldBe(5);
            response.ElementAt(1).Title.ShouldBe(VOLUNTEERING_TITLE2);
            response.ElementAt(1).Type.Name.ShouldBe("Freeee");
            response.ElementAt(1).Date.ShouldBe(new DateTime(2014, 3, 29));
            response.ElementAt(1).Details.ShouldBe("This is a detail");
            response.ElementAt(1).HourCount.ShouldBe(9);
            response.ElementAt(1).Acknowledgment.ShouldBe("WantSomeNuts");
            response.ElementAt(1).Customer.CustomerId.ShouldBe(Customer.CustomerId);
            response.ElementAt(1).VolonteeringTypeId.ShouldBe(VolunteeringType2.Id);
            response.ElementAt(1).IsDelete.ShouldBe(false);

        }

        [Fact]
        public void CreateVolunteeringTest()
        {
            _context.Volunteerings.Find(2).ShouldBe(null);

            var response = _mediator.Send(new CreateVolunteeringCommand
            {
                Title = VOLUNTEERING_TITLE2,
                Date = new DateTime(2014, 3, 29),
                Amount = "5",
                Details = "This is a detail",
                HourCount = 9,
                Acknowledgment = "WantSomeNuts",
                CustomerId = Customer.CustomerId,
                VolunteeringTypeId = VolunteeringType.Id
            }).Result;

            response.ShouldBeOfType(typeof(Unit));

            var volunteeringCreated = _context.Volunteerings.Where(v => v.Title.Equals(VOLUNTEERING_TITLE2)).SingleOrDefault();
            volunteeringCreated.Amount.ShouldBe(5);
            volunteeringCreated.Title.ShouldBe(VOLUNTEERING_TITLE2);
            volunteeringCreated.Type.Name.ShouldBe("Freeee");
            volunteeringCreated.Date.ShouldBe(new DateTime(2014, 3, 29));
            volunteeringCreated.Details.ShouldBe("This is a detail");
            volunteeringCreated.HourCount.ShouldBe(9);
            volunteeringCreated.Acknowledgment.ShouldBe("WantSomeNuts");
            volunteeringCreated.Customer.CustomerId.ShouldBe(Customer.CustomerId);
            volunteeringCreated.VolonteeringTypeId.ShouldBe(VolunteeringType.Id);
        }

        [Fact]
        public void UpdateVolunteeringTest()
        {
            var volunteering = _context.Volunteerings.Where(s => s.Title == VOLUNTEERING_TITLE).Single();

            var response = _mediator.Send(new UpdateVolunteeringCommand
            {
                VolunteeringId = volunteering.VolunteeringId,
                Title = VOLUNTEERING_TITLE2,
                Date = new DateTime(2014, 3, 29),
                Amount = "5",
                Details = "This is a detail",
                HourCount = 9,
                Acknowledgment = "WantSomeNuts",
                CustomerId = Customer.CustomerId,
                VolunteeringTypeId = VolunteeringType.Id
            }).Result;

            response.ShouldBeOfType(typeof(Unit));

            volunteering.Customer.CustomerId.ShouldBe(volunteering.CustomerId);
            volunteering.Amount.ShouldBe(5);
            volunteering.Title.ShouldBe(VOLUNTEERING_TITLE2);
            volunteering.Type.Name.ShouldBe("Freeee");
            volunteering.Date.ShouldBe(new DateTime(2014, 3, 29));
            volunteering.Details.ShouldBe("This is a detail");
            volunteering.HourCount.ShouldBe(9);
            volunteering.Acknowledgment.ShouldBe("WantSomeNuts");
            volunteering.VolonteeringTypeId.ShouldBe(VolunteeringType.Id);
        }

        [Fact]
        public void DeleteVolunteeringTest()
        {
            _context.Add(new Volunteering
            {
                Title = VOLUNTEERING_TITLE2,
                Date = new DateTime(2014, 3, 29),
                Amount = 5,
                Details = "This is a detail",
                HourCount = 9,
                Acknowledgment = "WantSomeNuts",
                Customer = Customer,
                IsDelete = false,
                Type = VolunteeringType2
            });

            _context.SaveChanges();

            var result = _mediator.Send(new DeleteVolunteeringCommand
            {
                VolunteeringId = _context.Volunteerings.Where(v => v.Title == VOLUNTEERING_TITLE2).Single().VolunteeringId,
            }).Result;

            _context.Volunteerings.Where(v => v.Title == VOLUNTEERING_TITLE2 && v.IsDelete == true).Any().ShouldBe(true);
            _context.Volunteerings.Where(v => v.Title == VOLUNTEERING_TITLE2 && v.IsDelete == false).SingleOrDefault().ShouldBe(null);


        }
        [Theory]
        [InlineData(-1)]
        [InlineData(87)]
        public void UpdateTest_Exception_InvalidVolunteeringId(int id)
        {
            var response = _mediator.Send(new UpdateVolunteeringCommand
            {
                VolunteeringId = id,
                Title = VOLUNTEERING_TITLE2,
                Date = new DateTime(2014, 3, 29),
                Amount = "5",
                Details = "This is a detail",
                HourCount = 9,
                Acknowledgment = "WantSomeNuts",
                CustomerId = Customer.CustomerId,
                VolunteeringTypeId = VolunteeringType.Id
            })
            .ShouldThrow(typeof(FluentValidation.ValidationException));
        }

        [Fact]
        public void UpdateTwoTimeToTheSameIntValue()
        {
            for (int i = 0; i < 2; i++)
            {
                var result = _mediator.Send(new UpdateVolunteeringCommand
                {
                    VolunteeringId = MainVolonteering.VolunteeringId,
                    Acknowledgment = MainVolonteering.Acknowledgment,
                    Amount = MainVolonteeringAmount.ToString(),
                    CustomerId = MainVolonteering.CustomerId,
                    Date = MainVolonteering.Date,
                    Details = MainVolonteering.Details,
                    HourCount = MainVolonteering.HourCount,
                    Title = MainVolonteering.Title,
                    VolonteeringTypeName = MainVolonteering.Type.Name,
                    VolunteeringTypeId = MainVolonteering.VolonteeringTypeId
                }).Result;

                result.ShouldBeOfType(typeof(Unit));

                MainVolonteering.Amount.ShouldBe(MainVolonteeringAmount);
            }
        }

        [Fact]
        public void UpdateTwoTimeToTheSameDecimalValue()
        {
            decimal myNewValue = 3.45m;

            for (int i = 0; i < 2; i++)
            {
                var result = _mediator.Send(new UpdateVolunteeringCommand
                {
                    VolunteeringId = MainVolonteering.VolunteeringId,
                    Acknowledgment = MainVolonteering.Acknowledgment,
                    Amount = myNewValue.ToString(),
                    CustomerId = MainVolonteering.CustomerId,
                    Date = MainVolonteering.Date,
                    Details = MainVolonteering.Details,
                    HourCount = MainVolonteering.HourCount,
                    Title = MainVolonteering.Title,
                    VolonteeringTypeName = MainVolonteering.Type.Name,
                    VolunteeringTypeId = MainVolonteering.VolonteeringTypeId
                }).Result;

                result.ShouldBeOfType(typeof(Unit));

                MainVolonteering.Amount.ShouldBe(myNewValue);
            }
        }

        [Theory]
        [InlineData("3.45")]
        [InlineData("3,45")]
        public void UpdateTwoTimeToTheSameTextValue(string text)
        {
            string myNewValue = text;

            for (int i = 0; i < 2; i++)
            {
                var result = _mediator.Send(new UpdateVolunteeringCommand
                {
                    VolunteeringId = MainVolonteering.VolunteeringId,
                    Acknowledgment = MainVolonteering.Acknowledgment,
                    Amount = myNewValue,
                    CustomerId = MainVolonteering.CustomerId,
                    Date = MainVolonteering.Date,
                    Details = MainVolonteering.Details,
                    HourCount = MainVolonteering.HourCount,
                    Title = MainVolonteering.Title,
                    VolonteeringTypeName = MainVolonteering.Type.Name,
                    VolunteeringTypeId = MainVolonteering.VolonteeringTypeId
                }).Result;

                result.ShouldBeOfType(typeof(Unit));

                MainVolonteering.Amount.ShouldBe(3.45m);
            }
        }
    }
}
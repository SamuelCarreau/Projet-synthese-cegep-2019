using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class HeardOfUsFromTest : ProfilOptionTestBase<HeardOfUsFrom>
    {
        private ParentEspoirDbContext _context;
        public HeardOfUsFromTest() : base()
        {
            _context = GetDbContext();

            var customer = _context.Add(new Customer
            {
                CustomerDescription = new CustomerDescription()
            }).Entity;

            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.SaveChanges();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            //throw new System.NotImplementedException();
        }
    }
}
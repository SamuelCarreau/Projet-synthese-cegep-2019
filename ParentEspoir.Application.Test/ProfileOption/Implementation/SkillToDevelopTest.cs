using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System.Threading;
using MediatR;
using System.Collections.Generic;
using System;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class SkillToDevelopTest : ProfilOptionTestBase<SkillToDevelop>
    {
        private ParentEspoirDbContext _context;
        public SkillToDevelopTest() : base()
        {
            _context = GetDbContext();
        }

        [Fact]
        public override void CantDeleteRelatedData()
        {
            //throw new System.NotImplementedException();
        }
    }
}
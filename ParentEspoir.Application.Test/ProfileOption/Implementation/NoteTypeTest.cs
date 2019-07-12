using ParentEspoir.Persistence;
using ParentEspoir.Application;
using System.Linq;
using Xunit;
using Shouldly;
using ParentEspoir.Domain.Entities;
using System;

namespace ParentEspoir.Application.ProfilOption.Test
{
    public class NoteTypeTest : ProfilOptionTestBase<NoteType>
    {
        private ParentEspoirDbContext _context;
        public NoteTypeTest() : base()
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
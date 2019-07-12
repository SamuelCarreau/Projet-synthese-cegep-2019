using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class WorkshopTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static explicit operator WorkshopTypeModel(WorkshopType w)
        {
            var model = new WorkshopTypeModel
            {
                Id = w.Id,
                Code = w.Code,
                Name = w.Name
            };

            return model;
        }
    }
}

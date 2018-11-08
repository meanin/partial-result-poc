using System;
using System.Collections.Generic;

namespace PartialResultPoC.Data.Models
{
    public class ChildModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public static class ChildModelMockup
    {
        public static List<ChildModel> ChildModels = new List<ChildModel>
        {
            new ChildModel
            {
                Id = Guid.Parse("69f09e35-dee1-400a-97cb-f00be1b3d67c"),
                Name = "NameName",
                Value = 12312
            },
            new ChildModel
            {
                Id = Guid.Parse("17767386-388e-4cd8-bdca-036575de5ad4"),
                Name = "Abracadabra",
                Value = 12312
            }
        };
    }
}

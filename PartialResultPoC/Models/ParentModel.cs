using System;
using System.Collections.Generic;

namespace PartialResultPoC.Models
{
    public class ParentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> ChildrenIds { get; set; }
        public Dictionary<Guid, ChildModel> ChildModels { get; set; } = new Dictionary<Guid, ChildModel>();
    }

    public static class ParentModelMockup
    {
        public static List<ParentModel> ParentModels = new List<ParentModel>
        {
            new ParentModel
            {
                Id = Guid.Parse("2f32ee55-c9de-4892-9685-8957aa11a2db"),
                Name = "Name",
                ChildrenIds = new List<Guid>{Guid.Parse("69f09e35-dee1-400a-97cb-f00be1b3d67c") }
            },
            new ParentModel
            {
                Id = Guid.Parse("ea8bc774-7bca-47df-9210-ce221f5e6e72"),
                Name = "Name0",
                ChildrenIds = new List<Guid>{Guid.Parse("17767386-388e-4cd8-bdca-036575de5ad4") }
            },
            new ParentModel
            {
                Id = Guid.Parse("93b3c2ec-e020-4d08-a5c4-1d50ae817454"),
                Name = "Name13",
                ChildrenIds = new List<Guid>
                {
                    Guid.Parse("87bffe07-a04e-425d-b598-55cdc931b62a"),
                    Guid.Parse("a6a3e0c7-b692-47e4-9444-6e870d938417"),
                    Guid.Parse("2e6f5073-8845-4196-b3d6-a8e160173915")

                }
            }
        };
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using PartialResultPoC.Data.Models;

namespace PartialResultPoC.Data.Repositories
{
    public class ChildModelRepository
    {
        private readonly List<ChildModel> _childModels;

        public ChildModelRepository(List<ChildModel> childModels)
        {
            _childModels = childModels;
        }

        public ChildModel GetById(Guid id)
        {
            return _childModels.SingleOrDefault(model => model.Id == id);
        }
    }
}

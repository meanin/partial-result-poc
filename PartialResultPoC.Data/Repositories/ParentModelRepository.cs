using System;
using System.Collections.Generic;
using System.Linq;
using PartialResultPoC.Data.Models;

namespace PartialResultPoC.Data.Repositories
{
    public class ParentModelRepository
    {
        private readonly List<ParentModel> _parentModels;

        public ParentModelRepository(List<ParentModel> parentModels)
        {
            _parentModels = parentModels;
        }

        public ParentModel GetById(Guid id)
        {
            return _parentModels.SingleOrDefault(d => d.Id == id);
        }

        public List<ParentModel> GetAll()
        {
            return _parentModels;
        }
    }
}

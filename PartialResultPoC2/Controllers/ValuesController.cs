using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartialResultPoC.Data.Models;
using PartialResultPoC.Data.Repositories;
using PartialResultPoC2.Extensions;

namespace PartialResultPoC2.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ParentModelRepository _parentModelRepository;
        private readonly ChildModelRepository _childModelRepository;

        public ValuesController(
            ParentModelRepository parentModelRepository,
            ChildModelRepository childModelRepository)
        {
            _parentModelRepository = parentModelRepository;
            _childModelRepository = childModelRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PartialResponse<IEnumerable<ParentModel>>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var parentModels = _parentModelRepository.GetAll();
            var success = true;
            foreach (var parentModel in parentModels)
            {
                foreach (var childId in parentModel.ChildrenIds)
                {
                    var child = _childModelRepository.GetById(childId);
                    if (child == null)
                    {
                        success = false;
                        continue;
                    }
                    parentModel.ChildModels[childId] = child;
                }
            }

            return this.PartialOk(parentModels, success);
        }
    }
}

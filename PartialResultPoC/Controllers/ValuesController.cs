using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartialResultPoC.Repositories;
using PartialResultPoC.Middlewares;
using PartialResultPoC.Models;

namespace PartialResultPoC.Controllers
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
        [ProducesResponseType(typeof(IEnumerable<ParentModel>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var parentModels = _parentModelRepository.GetAll();
            foreach (var parentModel in parentModels)
            {
                foreach (var childId in parentModel.ChildrenIds)
                {
                    var child = _childModelRepository.GetById(childId);
                    if (child == null)
                    {
                        this.SetPartialSuccess(false);
                        continue;
                    }
                    parentModel.ChildModels[childId] = child;
                }
            }

            return Ok(parentModels);
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PartialResultPoC.Repositories;

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
        public IActionResult Get()
        {
            var parentModels = _parentModelRepository.GetAll();
            foreach (var parentModel in parentModels)
            {
                foreach (var childId in parentModel.ChildrenIds)
                {
                    var child = _childModelRepository.GetById(childId);
                    if (child != null)
                    {
                        parentModel.ChildModels[childId] = child;
                    }
                    else
                    {
                        // TODO: Graceful handling
                    }
                }
            }
            return Ok(parentModels);
        }
    }
}

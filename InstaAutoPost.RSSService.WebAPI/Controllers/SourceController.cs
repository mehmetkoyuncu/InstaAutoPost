using InstaAutoPost.RSSService.Core.Abstract;
using InstaAutoPost.RSSService.Data.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.RSSService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _service;
        public SourceController(ISourceService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult AddSource(Source source)
        {
            return Ok(_service.Add(source));
        }
        [HttpDelete]
        public IActionResult DeleteSource(int id)
        {
            return Ok(_service.DeleteById(id));
        }
        [HttpGet]
        public IActionResult GetAllSources()
        {
            return Ok(_service.GetAll());
        }
      
        [HttpGet]
        public IActionResult GetSource(Source source)
        {
            return Ok(_service.GetById(source.Id));
        }
        [HttpGet]
        public IActionResult GetSource(string name)
        {
            return Ok(_service.GetByName(name));
        }
        [HttpGet]
        public IActionResult GetDeletedSources()
        {
            return Ok(_service.GetDeletedSource());
        }
        public IActionResult UpdateSource(Source source)
        {
            return Ok(_service.Update(source, source.Id));
        }

    }
}


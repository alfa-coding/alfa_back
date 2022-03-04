using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfa_back.Infrastructure;
using alfa_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace alfa_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly ILogger<ProblemController> _logger;

        public IConnector<Problem> ProblemContext { get; }

        public ProblemController(ILogger<ProblemController> logger, IConnector<Problem> _problemContext)
        {
            this._logger = logger;
            this.ProblemContext = _problemContext;
        }

        [HttpGet]
        public IEnumerable<Problem> Get()
        {
            return ProblemContext.GetElements();
        }
        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public Problem Get(string id)
        {
            return this.ProblemContext.GetElementById(id);
        }

        // POST api/<CatController>
        [HttpPost]
        public void Post([FromBody] Problem value)
        {
            this.ProblemContext.InsertElement(value);
        }

        // PUT api/<CatController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Problem value)
        {
            this.ProblemContext.Update(id, value);
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.ProblemContext.RemoveElement(id);
        }
    }
}

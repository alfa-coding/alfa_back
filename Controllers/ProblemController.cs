using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfa_back.lib.Infrastructure;
using alfa_back.lib.Models;
using alfa_back.lib.Utils;
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

            var isEmpty = this.ProblemContext.GetElements().ToList().Count == 0;
            if (isEmpty)
            {
                
                List<Problem> tempList = CreateTesProblems();

                foreach (var item in tempList)
                {
                    this.ProblemContext.InsertElementAsync(item);
                }
            }

        }

        [HttpGet]
        public IEnumerable<Problem> Get()
        {
            return ProblemContext.GetElements();
        }
        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public async Task<Problem> Get(string id)
        {
            return await this.ProblemContext.GetElementById(id);
        }

        // POST api/<CatController>
        [HttpPost]
        public async Task Post([FromBody] Problem value)
        {
            await this.ProblemContext.InsertElementAsync(value);
        }

        // PUT api/<CatController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] Problem value)
        {
            await this.ProblemContext.UpdateElementAsync(id, value);
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await this.ProblemContext.DeleteElementByIdAsync(id);
        }
    }
}

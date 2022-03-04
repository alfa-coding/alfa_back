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

            var isEmpty = this.ProblemContext.GetElements().ToList().Count == 0;
            if (isEmpty)
            {
                Problem cat1 = new Problem() { Name = "C1",LanguageInformation=new List<LanguageInfo>(){new LanguageInfo(){Extension="cs",LanguageName="C#",MilisecondsAllowed=200},new LanguageInfo(){Extension="cpp", LanguageName="C++", MilisecondsAllowed=1000}},Description="Given an array of non negative integer numbers, calculate its average" };
                Problem cat2 = new Problem() { Name = "C2",LanguageInformation=new List<LanguageInfo>(){new LanguageInfo(){Extension="cs",LanguageName="C#",MilisecondsAllowed=200},new LanguageInfo(){Extension="cpp", LanguageName="C++", MilisecondsAllowed=1000}},Description="Given an array of integer numbers, count the positives and negatives" };
                Problem cat3 = new Problem() { Name = "C3",LanguageInformation=new List<LanguageInfo>(){new LanguageInfo(){Extension="cs",LanguageName="C#",MilisecondsAllowed=200},new LanguageInfo(){Extension="cpp", LanguageName="C++", MilisecondsAllowed=1000}},Description="Given an array of non negative integer numbers, and, a number x, tell whether x is present on the array" };
                var tempList = new List<Problem>() { cat1, cat2, cat3 };
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

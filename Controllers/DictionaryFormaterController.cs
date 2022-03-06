using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alfa_back.lib.Infrastructure;
using alfa_back.lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace alfa_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictionaryFormaterController : ControllerBase
    {
        private readonly ILogger<DictionaryFormaterController> _logger;

        public IConnector<DictionaryCodeFormater> DictionaryFormatterContext { get; }

        public DictionaryFormaterController(ILogger<DictionaryFormaterController> logger, IConnector<DictionaryCodeFormater> _dictionaryFormatterContext)
        {
            this._logger = logger;
            this.DictionaryFormatterContext = _dictionaryFormatterContext;

            var isEmpty = this.DictionaryFormatterContext.GetElements().ToList().Count == 0;
            if (isEmpty)
            {
                DictionaryCodeFormater cat1 = new DictionaryCodeFormater() { Extension = "cs", DictionaryFormater = this.loadDictionary("cs") };
                DictionaryCodeFormater cat2 = new DictionaryCodeFormater() { Extension = "cpp", DictionaryFormater = this.loadDictionary("cpp") };
                DictionaryCodeFormater cat3 = new DictionaryCodeFormater() { Extension = "py", DictionaryFormater = this.loadDictionary("py") };
                var tempList = new List<DictionaryCodeFormater>() { cat1, cat2, cat3 };
                foreach (var item in tempList)
                {
                    this.DictionaryFormatterContext.InsertElementAsync(item);
                }
            }

        }

        private string loadDictionary(string extension)
        {
            string cpp = @"
const string BoolToString(bool b)
{
    return b ? ""True"" : ""False"";
}
string MyDictionaryToJson(map<string, bool> dict)
{
    string monster = "";        
    for (auto it = dict.begin(); it != dict.cend(); it++)
    {
        string temp = ""'"" + it->first + ""':"" + BoolToString(it->second) + "","";
        monster += temp;
    }
    monster.substr(0, monster.size() - 1) ;       
    return ""{"" + monster.substr(0, monster.size() - 1) + ""}"";
}
";

            string cs= @"
public static string MyDictionaryToJson(Dictionary < string, bool> dict)
{
    var entries = dict.Select(d =>string.Format(""'{0}': {1}"", d.Key, d.Value.ToString()));
    return ""{"" + string.Join("","", entries) + ""}"";
}
";

            if (extension == "cpp")
            {
                return cpp;
            }
            if (extension == "cs")
            {
                return cs;
            }
            if (extension == "py")
            {
                return "";
            }
            return "";
        }

        [HttpGet]
        public IEnumerable<DictionaryCodeFormater> Get()
        {
            return DictionaryFormatterContext.GetElements();
        }
        // GET api/<CatController>/5
        [HttpGet("{id}")]
        public async Task<DictionaryCodeFormater> Get(string id)
        {
            return await this.DictionaryFormatterContext.GetElementById(id);
        }

        // POST api/<CatController>
        [HttpPost]
        public async Task Post([FromBody] DictionaryCodeFormater value)
        {
            await this.DictionaryFormatterContext.InsertElementAsync(value);
        }

        // PUT api/<CatController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] DictionaryCodeFormater value)
        {
            await this.DictionaryFormatterContext.UpdateElementAsync(id, value);
        }

        // DELETE api/<CatController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await this.DictionaryFormatterContext.DeleteElementByIdAsync(id);
        }
    }
}

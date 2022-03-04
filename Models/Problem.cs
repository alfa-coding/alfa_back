using System.Collections.Generic;
using alfa_back.Infrastructure.Concrete;

namespace alfa_back.Models
{
    [BsonCollection("Problem")]
    public class Problem : Document
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LanguageInfo> LanguageInformation { get; set; }
    }
}
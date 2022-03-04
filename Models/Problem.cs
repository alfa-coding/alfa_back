using System.Collections.Generic;
using alfa_back.Infrastructure.Concrete;

namespace alfa_back.Models
{
    public class Problem : Document
    {
        public string Description { get; set; }
        public List<LanguageInfo> LanguageInformation { get; set; }
    }
}
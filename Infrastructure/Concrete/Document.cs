using System;
using MongoDB.Bson;

namespace alfa_back.Infrastructure.Concrete
{
    public class Document : IDocument
    {
        public string Id { get; set; }
        public Document()
        {
            this.Id= Guid.NewGuid().ToString();
        }
    }
}
using Cascade.Head.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cascade.Head.Helpers
{
    public static class SimpleSerializer
    {
        private const char FieldBreak = '\t';
        private const char RecordBreak = '\r';

        public static string Serialize(IEnumerable<Element> elements)
        {
            string raw = String.Empty;
            if (elements != null && elements.Count() > 0)
            {
                foreach (var element in elements)
                {
                    raw += element.Type + FieldBreak + element.Name + FieldBreak + element.Content + RecordBreak;
                }
            }
            return raw;
        }

        public static IList<Element> Deserialize(string raw)
        {
            var elements = new List<Element>();
            if (!String.IsNullOrWhiteSpace(raw))
            {
                var lines = raw.Split(new[] { RecordBreak }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var fields = line.Split(FieldBreak);
                    Element element = new Element
                    {
                        Id = 0,
                        HeadPartRecord_Id = 0,
                        Deleted = false,
                        Type = fields[0],
                        Name = fields[1],
                        Content = fields[2]
                    };
                    elements.Add(element);
                }
            }
            return elements;
        }
 
    }
}
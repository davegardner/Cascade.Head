using System;
using System.Collections.Generic;
using System.Linq;
namespace Cascade.Head.Settings
{
    /// <summary>
    /// Represents one Element in the list of Elements in HeadSettingsVM
    /// </summary>
    public class Element
    {
        // These two properties are not actually used but are required for compatability between the script used to 
        // maintain the frontend
        public int Id { get; set; }
        public int HeadPartRecord_Id { get; set; }
        
        public bool Deleted { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class HeadTypePartSettings
    {
        private const char FieldBreak = '\t';
        private const char RecordBreak = '\r';

        public HeadTypePartSettings()
        {
            Elements = new List<Element>();
        }

        public List<Element> Elements { get; set; }


        /// <summary>
        /// Serialized representation of Elements
        /// Only Type, Name and Content are converted
        /// </summary>
        public string RawElements
        {
            get
            {
                string raw = String.Empty;
                if (Elements != null && Elements.Count() > 0)
                {
                    foreach (var element in Elements)
                    {
                        raw += element.Type + FieldBreak + element.Name + FieldBreak + element.Content + RecordBreak;
                    }
                }
                return raw;
            }
            set
            {
                var elements = new List<Element>();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    var lines = value.Split(new[] { RecordBreak }, StringSplitOptions.RemoveEmptyEntries);
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
                Elements = elements;
            }
        }

    }
}

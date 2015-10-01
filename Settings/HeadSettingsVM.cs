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
        // These three properties are not actually used but are required for compatability between the script used to 
        // maintain the frontend
        public int Id { get; set; }
        public int HeadPartRecord_Id { get; set; }
        public bool Deleted { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class HeadSettingsVM
    {
        private const char FieldBreak = '\t';
        private const char RecordBreak = '\r';

        public HeadSettingsVM(string rawElements = "")
        {
            RawElements = rawElements;
        }

        /// <summary>
        /// Serialized representation of Elements -- use Elements property instead
        /// </summary>
        public string RawElements { get; set; }

        ///<summary>
        /// Head Elements for a specific type must be stored as a string
        ///</summary>
        public IList<Element> Elements
        {
            get
            {
                List<Element> elements = new List<Element>();

                var value = RawElements;
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
                return elements;
            }

            set
            {
                if (value == null || value.Count() == 0)
                {
                    RawElements = String.Empty;
                }
                else
                {
                    string result = string.Empty;
                    foreach (var element in value)
                    {
                        result += element.Type + FieldBreak + element.Name + FieldBreak + element.Content + RecordBreak;
                    }
                    RawElements = result;
                }
            }
        }
    }
}
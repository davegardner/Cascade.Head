using Orchard.ContentManagement;
using System.Collections.Generic;

namespace Cascade.Head.Models
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

    public class HeadSettingsPart: ContentPart
    {
        public HeadSettingsPart()
        {
            Elements = new List<Element>();
        }

        public IList<Element> Elements { get; set; }
        public string RawElements
        {
            get{return this.Retrieve(x => x.RawElements); }
            set { this.Store(x => x.RawElements, value); }
        }
    }
}

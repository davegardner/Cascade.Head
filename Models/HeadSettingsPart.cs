using Orchard.ContentManagement;
using System.Collections.Generic;

namespace Cascade.Head.Models
{
    
    /// <summary>
    /// Site Settings Head Elements 
    /// </summary>
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

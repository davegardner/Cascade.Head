using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.Collections.Generic;

namespace Cascade.Head.Models
{
    /// <summary>
    /// Defines a collection of elements on the Content Item that will be added to the head tag
    /// </summary>
    public class HeadPartRecord : ContentPartRecord
    {
        virtual public string RawElements { get; set;}
    }

    /// <summary>
    /// Content Item Head Elements
    /// </summary>
     public class HeadPart : ContentPart<HeadPartRecord>
    {
        public HeadPart()
        {
            Elements = new List<Element>();
        }

        public IList<Element> Elements { get; set; }
        public string RawElements
        {
            get { return this.Retrieve(x => x.RawElements); }
            set { this.Store(x => x.RawElements, value); }
        }
    }
}
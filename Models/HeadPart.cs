using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.Collections.Generic;

namespace Cascade.Head.Models
{
    /// <summary>
    /// Defines a collection of elements that will be added to the head tag
    /// </summary>
    public class HeadPartRecord : ContentPartRecord
    {
        public HeadPartRecord()
        {
            HeadElementRecords = new List<HeadElementRecord>();
        }

        public virtual IList<HeadElementRecord> HeadElementRecords { get; set; }

    }

    /// <summary>
    /// Defines a collection of element parts that will be added to the head tag
    /// </summary>
    public class HeadPart : ContentPart<HeadPartRecord>
    {
        public IEnumerable<HeadElementRecord> Elements
        {
            get { return Record.HeadElementRecords; }
        }
    }
}
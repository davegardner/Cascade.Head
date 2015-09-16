using System.Collections.Generic;

namespace Cascade.Head.ViewModels
{
    public class HeadVM
    {
        public HeadVM() { Elements = new List<ElementVM>(); }
        public int Id { get; set; }
        public IList<ElementVM> Elements { set; get; }

        public string[] AvailableTypes { get; set; }
    }

    public class ElementVM
    {
        public int Id { get; set; }
        public int HeadPartRecord_Id { get; set; }
        public bool Deleted { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

    }
}
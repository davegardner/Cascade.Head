using Cascade.Head.Models;
using System.Collections.Generic;

namespace Cascade.Head.ViewModels
{
    public class HeadVM
    {
        public HeadVM() { Elements = new List<Element>(); }
        public int Id { get; set; }
        public IList<Element> Elements { set; get; }

        public string[] AvailableTypes { get; set; }
    }

}
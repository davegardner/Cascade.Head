
namespace Cascade.Head.Models
{

    /// <summary>
    /// Defines an element that will be added to the head tag
    /// </summary>
    public class Element
    {
        public bool Deleted { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}



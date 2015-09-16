
namespace Cascade.Head.Models
{

    /// <summary>
    /// Defines an element that will be added to the head tag
    /// </summary>
    public class HeadElementRecord 
    {
        public virtual int Id { get; set; }
        public virtual HeadPartRecord HeadPartRecord { get; set; }
        public virtual string Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string Content { get; set; }
    }
}



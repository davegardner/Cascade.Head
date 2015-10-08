using Cascade.Head.Models;
using System.Collections.Generic;
namespace Cascade.Head.Settings
{
    /// <summary>
    /// Content Type Head Elements
    /// </summary>
    public class HeadTypePartSettings
    {
        public HeadTypePartSettings()
        {
            Elements = new List<Element>();
        }

        public IList<Element> Elements { get; set; }


        /// <summary>
        /// Serialized representation of Elements
        /// Only Type, Name and Content are converted
        /// </summary>
        public string RawElements
        {
            get; set;
        }

    }
}

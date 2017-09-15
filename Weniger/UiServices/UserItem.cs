using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices
{
    public abstract class UserItem
    {
        /// <summary>
        /// A unique Id for this item.
        /// </summary>
        public Guid FiledId { get; set; }


        /// <summary>
        /// The group of which this item is part of. 
        /// a group may affect the positioning and the appreance of an item depending on the Augmentor
        /// </summary>
        public Group Group { get; set; }
    }
}

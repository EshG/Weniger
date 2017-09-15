using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger
{
    public enum SaveType
    {
        /// <summary>
        /// Input will be sent when the user interacts with a confirmation element
        /// </summary>
        Ok,
        /// <summary>
        /// Input will be sent when the item's element lost focus
        /// </summary>
        LostFocus
    };
}

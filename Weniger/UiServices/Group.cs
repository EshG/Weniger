using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices
{
    public class Group : IGroup
    {
        public int Priority { get; set; }

        public string DisplayName { get; set; }

        public int Id { get; set; }

        public IGroup Parent { get; set; }

        public event EventHandler Activated;
    }
}

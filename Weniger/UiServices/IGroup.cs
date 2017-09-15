using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices
{
    public interface IGroup
    {
        int Priority { get; }
        string DisplayName { get; }
        int Id { get; set; }
    }
}

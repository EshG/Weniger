using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices
{
    public interface IHeaderItem
    {
        string Header { get; }
    }

    public interface IValueItem
    {
        object Value { get; set; }
    }
}

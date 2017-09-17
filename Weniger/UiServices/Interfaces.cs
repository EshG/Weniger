using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Weniger.UiServices.ViewModels;

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

    public interface IVmField
    {
        VmField ToVmField();

        PropertyInfo ValueProperty { get; }

        string VmPropertyName { get; }
    }
}

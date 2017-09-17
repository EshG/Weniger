using System;
using System.Collections.Generic;
using System.Reflection;
using Weniger.UiServices.ViewModels;

namespace Weniger.UiServices
{
    public class PropertyUserItem : UserItem, IHeaderItem, IValueItem, IVmField
    {
        public PropertyInfo Property { get; set; }

        public object Value { get; set; }

        public string Header
        {
            get { return Property.Name; }
        }
        

        public static IEnumerable<PropertyUserItem> FromModel(object model)
        {
            foreach (PropertyInfo prop in model.GetType().GetRuntimeProperties())
            {
                yield return new PropertyUserItem()
                {
                    FiledId = Guid.NewGuid(),
                    Property = prop,
                    Value = prop.GetValue(model)
                };
            }
        }


        public PropertyInfo ValueProperty { get { return GetType().GetRuntimeProperty(nameof(Value)); } }

        public string VmPropertyName => Property.Name;

        public VmField ToVmField()
        {
            return new VmField() { isCollection = false, name = VmPropertyName, type = Property.PropertyType };
        }
    }
}

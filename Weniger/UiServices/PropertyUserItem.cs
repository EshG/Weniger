using System;
using System.Collections.Generic;
using System.Reflection;

namespace Weniger.UiServices
{
    public class PropertyUserItem : UserItem, IHeaderItem, IValueItem
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
    }
}

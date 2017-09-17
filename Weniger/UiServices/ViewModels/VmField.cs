using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices.ViewModels
{
    public struct VmField
    {
        public string name;

        public bool isCollection;

        public Type type;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is VmField))
                return false;

            VmField other = (VmField)obj;

            return other.name == name && other.isCollection == isCollection && other.type == type;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + (isCollection ? 1 : 0) + type.GetHashCode();
        }
    }
}

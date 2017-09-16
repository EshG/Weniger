using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml.Controls
{
    static class ControlSelector
    {
        public static IControlGenerator ForInput(object value)
        {
            if (value is DateTime)
                return new DateInput();

            if (value is string)
                return new TextBox();
            
            throw new NotImplementedException($"No control implemented for type {value.GetType()}");
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}

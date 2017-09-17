using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Text;

namespace Weniger.UiServices.ViewModels
{
    public static class VMFactory
    {
        static object thisLock = new object();
        
        public static object CreateViewModel(IEnumerable<IVmField> data)
        {
            List<VmField> fields = new List<VmField>();

            foreach(IVmField item in data)
            {
                fields.Add(item.ToVmField());
            }
            
            object instance = CreateViewModel(fields);

            PopulateProperties(instance, data);

            return instance;
        }

        private static void PopulateProperties(object vm, IEnumerable<IVmField> items)
        {
            Type vmType = vm.GetType();

            foreach (IVmField item in items)
            {
                object value = item.ValueProperty.GetValue(item);
                PropertyInfo vmProperty = vmType.GetRuntimeProperty(item.VmPropertyName);
                vmProperty.SetValue(vm,value);
            }
        }

        public static object CreateViewModel(IReadOnlyCollection<VmField> fields)
        {
            string name = "GEN" + Guid.NewGuid().ToString().Replace("-",string.Empty);

            TypeBuilder builder = VmTypeBuilder.GetTypeBuilder(name);

            Type vmType = TryGetExistingType(fields);

            if (vmType == null)
            {
                vmType = BuildType(fields, builder).AsType();
                CreatedTypes[fields] = vmType;
            }

            object instance = Activator.CreateInstance(vmType);

            return instance;
        }

        private static TypeInfo BuildType(IReadOnlyCollection<VmField> fields, TypeBuilder builder)
        {
            foreach (VmField field in fields)
            {
                Type fieldType = field.type;

                if (field.isCollection)
                {
                    fieldType = typeof(ObservableCollection<>).MakeGenericType(field.type);
                }

                VmTypeBuilder.CreateProperty(builder, field.name, fieldType);
            }

            TypeInfo vmType = builder.CreateTypeInfo();
            return vmType;
        }

        static Type TryGetExistingType(IReadOnlyCollection<VmField> fields)
        {
            foreach(var pair in CreatedTypes)
            {
                if (!fields.Except(pair.Key).Any())
                    return pair.Value;
            }

            return null;
        }

        static  Dictionary<IReadOnlyCollection<VmField>, Type> _createdTypes = new Dictionary<IReadOnlyCollection<VmField>, Type>();
        static Dictionary<IReadOnlyCollection<VmField>, Type> CreatedTypes
        {
            get
            {
                lock (thisLock)
                    return _createdTypes;
            }
        }
    }
}

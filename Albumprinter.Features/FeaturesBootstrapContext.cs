using System;
using System.Collections.Generic;

namespace Albumprinter.Features
{
    public sealed class FeaturesBootstrapContext
    {
        public FeaturesBootstrapContext()
        {
            Properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            Pulse = new FeaturesBootstrapPulse(this);
        }

        public FeaturesBootstrapPulse Pulse { get; private set; }

        public Dictionary<string, object> Properties { get; private set; }

        public T Get<T>()
        {
            object instance;
            var type = typeof (T);
            var typeName = GetTypeName(type);
            if (Properties.TryGetValue(typeName, out instance))
            {
                return (T) instance;
            }
            foreach (var kvp in Properties)
            {
                if (type.IsAssignableFrom(Type.GetType(kvp.Key)))
                {
                    return (T) kvp.Value;
                }
            }
            throw new KeyNotFoundException(string.Format("Could not resolve the instance of '{0}'.", typeName));
        }

        public void Set<T>(T instance)
        {
            Properties[GetTypeName(typeof(T))] = instance;
        }


        public void Set(params object[] instances)
        {
            foreach (var instance in instances)
            {
                Properties[GetTypeName(instance.GetType())] = instance;
            }
        }

        private static string GetTypeName(Type type)
        {
            return type.AssemblyQualifiedName ?? type.FullName;
        }
    }
}
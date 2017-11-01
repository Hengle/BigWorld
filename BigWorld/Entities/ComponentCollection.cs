using System;
using System.Collections.Generic;
using BigWorld.Entities.Components;

namespace BigWorld.Entities
{
    public class ComponentCollection
    {
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();
        
        public T CreateComponent<T>()
            where T: Component, new()
        {
            if (components.ContainsKey(typeof(T)))
                return (T)components[typeof(T)];
             
            
            var instance = new T();
            components.Add(typeof(T),instance);
            return instance;
        }
        
        public bool TryGetComponent<T>(out T component)
            where T: Component
        {
            if (components.TryGetValue(typeof(T),out var comp))
            {
                component = (T)comp;
                return true;
            }

            component = default(T);
            return false;
        }
    }
}
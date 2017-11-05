using System;

namespace BigWorld.Entities.Components.AI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenDefinitionAttribute : Attribute
    {
        public readonly uint Probabillity;
        
        public GenDefinitionAttribute(uint probabillity)
        {
            Probabillity = probabillity;
        }
    }
}
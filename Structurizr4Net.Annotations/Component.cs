using System;

namespace Structurizr4Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum)]
    public class Component : Attribute
    {
        public string Description;
    }
}

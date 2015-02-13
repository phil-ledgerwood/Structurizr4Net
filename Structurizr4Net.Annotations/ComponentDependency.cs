using System;

namespace Structurizr4Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Field)]
    public class ComponentDependency : Attribute
    {
        public string Description;
    }
}

using System;

namespace Structurizr4Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Field)]
    public class UsedBy : Attribute
    {
        public string Person;
        public string Description;
    }
}

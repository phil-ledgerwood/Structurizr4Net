﻿using System;

namespace Structurizr4Net.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Field)]
    public class ContainerDependency : Attribute
    {
        public string Target;
        public string Description;
    }
}

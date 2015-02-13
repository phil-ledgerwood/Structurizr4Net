using System.Collections.Generic;
using Structurizr4Net.Core.Model;


namespace Structurizr4Net.Core.ComponentFinding
{
    public interface IComponentFinderStrategy
    {
        IEnumerable<Component> FindComponent();
    }
}

using System.Collections.Generic;
using Structurizr4Net.Core.Model;


namespace Structurizr4Net.Core.ComponentFinding
{
    public interface IComponentFinderStrategy
    {
        IEnumerable<Component> FindComponents();
        void FindDependencies();
        void SetComponentFinder(ComponentFinder componentFinder);
    }
}

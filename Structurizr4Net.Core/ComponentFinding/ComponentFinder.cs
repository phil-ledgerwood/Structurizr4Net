using System.Collections.Generic;
using System.Linq;
using Structurizr4Net.Core.Model;

namespace Structurizr4Net.Core.ComponentFinding
{
    public class ComponentFinder
    {
        private IList<IComponentFinderStrategy> _componentFinderStrategies = new List<IComponentFinderStrategy>();

        public ComponentFinder(Container container, string packageToScan,
            IEnumerable<IComponentFinderStrategy> componentFinderStrategies)
        {
            Container = container;
            PackageToScan = packageToScan;

            foreach (var strategy in componentFinderStrategies)
            {
                _componentFinderStrategies.Add(strategy);
                strategy.SetComponentFinder(this);
            }
        }

        internal Container Container { get; private set; }
        internal string PackageToScan { get; private set; }

        public Component FoundComponent(string interfaceType, string implementationType, string description,
            string technology)
        {
            var type = interfaceType;
            if (string.IsNullOrEmpty(type))
                type = implementationType;

            var component = Container.GetComponentOfType(type);

            if (component != null)
                MergeInformation(component, interfaceType, implementationType, description, technology);
            else
            {
                var name = type.Substring(type.LastIndexOf(".", System.StringComparison.Ordinal) + 1);
                component = Container.GetComponentWithName(name);
                if(component == null)
                    component = Container.AddComponentOfType(interfaceType, implementationType, description, technology);
                else
                    MergeInformation(component, interfaceType, implementationType, description, technology);
            }

            return component;
        }

        private void MergeInformation(Component component, string interfaceType, string implementationType, string description, string technology)
        {
            if (component.InterfaceType != null && component.InterfaceType.Trim().Length == 0)
                component.InterfaceType = interfaceType;

            if (component.ImplementationType != null && component.ImplementationType.Trim().Length == 0)
                component.ImplementationType = implementationType;

            if (component.Description != null && component.Description.Trim().Length == 0)
                component.Description = description;

            if (component.Technology != null && component.Technology.Trim().Length == 0)
                component.Technology = technology;
        }

        public IEnumerable<Component> FindComponents()
        {
            var componentsFound = new LinkedList<Component>();

            foreach (var strategy in _componentFinderStrategies)
                componentsFound.Union(strategy.FindComponents());

            foreach (var strategy in _componentFinderStrategies)
                strategy.FindDependencies();

            return componentsFound;
        }
    }
}

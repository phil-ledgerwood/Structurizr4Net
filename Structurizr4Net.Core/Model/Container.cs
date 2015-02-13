using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Structurizr4Net.Core.Model
{
    public class Container : Element
    {
        private ISet<Component> _components = new HashSet<Component>();

        [JsonIgnore]
        public SoftwareSystem Parent { get; internal set; }
        public string Technology { get; set; }

        public void Uses(Container destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(SoftwareSystem destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(SoftwareSystem destination, string description, string protocol, int port, string version)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(Component destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public Component AddComponentOfType(string interfaceType, string implementationType, string description, string technology)
        {
            var component = Model.AddComponentOfType(this, interfaceType, implementationType, description);
            component.Technology = technology;
            return component;
        }

        public Component AddComponent(string name, string description)
        {
            return Model.AddComponent(this, name, description);
        }

        public Component AddComponent(string name, string description, string technology)
        {
            var component = Model.AddComponent(this, name, description);
            component.Technology = technology;
            return component;
        }

        internal void Add(Component component)
        {
            if (GetComponentWithName(component.Name) == null)
                _components.Add(component);
        }

        public ISet<Component> Components
        {
            get { return _components; }
        }

        public Component GetComponentWithName(string name)
        {
            return _components.FirstOrDefault(x => x.Name.Equals(name));
        }

        public Component GetComponentOfType(string type)
        {
            var component = _components.FirstOrDefault(x => x.InterfaceType.Equals(type)) ??
                            _components.FirstOrDefault(x => x.ImplementationType.Equals(type));

            return component;
        }

        public override ElementType Type
        {
            get { return ElementType.Container; }
        }

        public override string CanonicalName
        {
            get { return string.Format("{0}{1}{2}", Parent.CanonicalName, CanonicalNameSeparator, Name); }
        }
    }
}

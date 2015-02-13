using System.Collections.Generic;
using System.Linq;

namespace Structurizr4Net.Core.Model
{
    public class SoftwareSystem : Element
    {
        private Location _location = Location.Unspecified;
        private ISet<Container> _containers = new HashSet<Container>();

        internal SoftwareSystem()
        {
            AddTags(Core.Model.Tags.SoftwareSystem);
        }

        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }

        internal void Add(Container container)
        {
            _containers.Add(container);
        }

        public ISet<Container> Containers
        {
            get { return new HashSet<Container>(_containers); }
        }

        public Container GetContainerWithName(string name)
        {
            return _containers.FirstOrDefault(x => x.Name.Equals(name));
        }

        public Container GetContainerWithId(string id)
        {
            return _containers.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Uses(SoftwareSystem destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(Container destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Delivers(Person destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public override ElementType Type
        {
            get { return ElementType.SoftwareSystem; }
        }

        public override string CanonicalName
        {
            get { return CanonicalNameSeparator + Name; }
        }
    }
}

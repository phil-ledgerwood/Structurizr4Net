using System.Collections.Generic;
using System.Linq;
using Structurizr4Net.Core.Collections;

namespace Structurizr4Net.Core.Model
{
    public class Model
    {
        private SequentialIntegerIdGeneratorStrategy _idGenerator = new SequentialIntegerIdGeneratorStrategy();

        private readonly IDictionary<string, Element> _elementsById = new HashMap<string, Element>();
        private readonly IDictionary<string, Relationship> _relationshipsById = new HashMap<string, Relationship>();
        private ISet<Person> _people = new SortedSet<Person>();
        private ISet<SoftwareSystem> _softwareSystems = new SortedSet<SoftwareSystem>();

        public Model() { }

        public SoftwareSystem AddSoftwareSystem(Location location, string name, string description)
        {
            if (GetSoftwareSystemWithName(name) != null) return null;

            var softwareSystem = new SoftwareSystem
            {
                Location = location,
                Name = name,
                Description = description
            };

            _softwareSystems.Add(softwareSystem);
            softwareSystem.Id = _idGenerator.GenerateId(softwareSystem);
            AddElementToInternalStructures(softwareSystem);
            return softwareSystem;
        }

        public Person AddPerson(Location location, string name, string description)
        {
            if (GetPersonWithName(name) != null) return null;

            var person = new Person
            {
                Location = location,
                Name = name,
                Description = description
            };

            _people.Add(person);
            person.Id = _idGenerator.GenerateId(person);
            AddElementToInternalStructures(person);
            return person;
        }

        internal Container AddContainer(SoftwareSystem parent, string name, string description, string technology)
        {
            if (parent.GetContainerWithName(name) != null) return null;

            var container = new Container
            {
                Name = name,
                Description = description,
                Technology = technology,
                Parent = parent
            };

            parent.Add(container);
            container.Id = _idGenerator.GenerateId(container);
            AddElementToInternalStructures(container);
            return container;
        }

        internal Component AddComponentOfType(Container parent, string interfaceType, string implementationType, string description)
        {
            var component = new Component
            {
                InterfaceType = interfaceType,
                ImplementationType = implementationType,
                Description = description,
                Parent = parent
            };

            parent.Add(component);
            component.Id = _idGenerator.GenerateId(component);
            AddElementToInternalStructures(component);
            return component;
        }

        internal Component AddComponent(Container parent, string name, string description)
        {
            var component = new Component
            {
                Name = name,
                Description = description,
                Parent = parent
            };

            parent.Add(component);
            component.Id = _idGenerator.GenerateId(component);
            AddElementToInternalStructures(component);
            return component;
        }

        internal void AddRelationship(Relationship relationship)
        {
            if (relationship.Source.Has(relationship)) return;

            relationship.Id = _idGenerator.GenerateId(relationship);
            relationship.Source.AddRelationship(relationship);
        }

        private void AddElementToInternalStructures(Element element)
        {
            _elementsById.Add(element.Id, element);
            element.Model = this;
            _idGenerator.Found(element.Id);
        }

        private void AddRelationshipToInternalStructures(Relationship relationship)
        {
            _relationshipsById.Add(relationship.Id, relationship);
            _idGenerator.Found(relationship.Id);
        }

        public Element GetElement(string id)
        {
            return _elementsById.FirstOrDefault(x => x.Key == id).Value;
        }

        public IEnumerable<Person> GetPeople()
        {
            return new SortedSet<Person>(_people);
        }

        public ISet<SoftwareSystem> GetSoftwareSystem()
        {
            return new SortedSet<SoftwareSystem>(_softwareSystems);
        }

        public void Hydrate()
        {
            foreach(var person in _people)
                AddElementToInternalStructures(person);

            foreach (var softwareSystem in _softwareSystems)
            {
                AddElementToInternalStructures(softwareSystem);

                foreach (var container in softwareSystem.Containers)
                {
                    softwareSystem.Add(container);
                    AddElementToInternalStructures(container);
                    container.Parent = softwareSystem;

                    foreach (var component in container.Components)
                    {
                        container.Add(component);
                        AddElementToInternalStructures(component);
                        component.Parent = container;
                    }
                }
            }

            foreach (var person in _people)
                HydrateRelationships(person);

            foreach (var softwareSystem in _softwareSystems)
            {
                HydrateRelationships(softwareSystem);

                foreach (var container in softwareSystem.Containers)
                {
                    HydrateRelationships(container);

                    foreach (var component in container.Components)
                    {
                        HydrateRelationships(component);
                    }
                }
            }
        }

        private void HydrateRelationships(Element element)
        {
            foreach (var relationship in element.Relationships)
            {
                relationship.Source = GetElement(relationship.SourceId);
                relationship.Destination = GetElement(relationship.DestinationId);
                AddRelationshipToInternalStructures(relationship);
            }
        }

        public bool Contains(Element element)
        {
            return _elementsById.Values.Contains(element);
        }

        public SoftwareSystem GetSoftwareSystemWithName(string name)
        {
            return _softwareSystems.FirstOrDefault(x => x.Name == name);
        }

        public SoftwareSystem GetSoftwareSystemWithId(string id)
        {
            return _softwareSystems.FirstOrDefault(x => x.Id == id);
        }

        public Person GetPersonWithName(string name)
        {
            return GetPeople().FirstOrDefault(x => x.Name == name);
        }
    }
}

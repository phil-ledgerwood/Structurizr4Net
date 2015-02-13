namespace Structurizr4Net.Core.Model
{
    public class Person : Element
    {
        private Location _location = Location.Unspecified;

        internal Person()
        {
            AddTags(Core.Model.Tags.Person);
        }

        public Location Location { get { return _location; } set { _location = value; } }

        public void Uses(SoftwareSystem destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(Container destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public void Uses(Component destination, string description)
        {
            Model.AddRelationship(new Relationship(this, destination, description));
        }

        public override ElementType Type
        {
            get { return ElementType.Person; }
        }

        public override string CanonicalName
        {
            get { return CanonicalNameSeparator + Name; }
        }
    }
}

using Newtonsoft.Json;

namespace Structurizr4Net.Core.Model
{
    public class Component : Element
    {
        private string _technology = string.Empty;

        public Component()
        {
            AddTags(Core.Model.Tags.Component);
        }

        [JsonIgnore]
        public Container Parent { get; set; }
        public string Technology { get { return _technology; } set { _technology = value; } }
        public string InterfaceType { get; set; }
        public string ImplementationType { get; set; }

        public override string Name
        {
            get
            {
                if (InternalName != null) return base.Name;
                if (InterfaceType != null) return InterfaceType.Substring(InterfaceType.LastIndexOf(".", System.StringComparison.Ordinal) + 1);
                if (ImplementationType != null) return ImplementationType.Substring(ImplementationType.LastIndexOf(".", System.StringComparison.Ordinal) + 1);
                return string.Empty;
            }
            set { base.Name = value; }
        }

        public string Package { get { return InterfaceType.Substring(0, InterfaceType.LastIndexOf(".", System.StringComparison.Ordinal)); } }

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
            get { return ElementType.Component; }
        }

        public override string CanonicalName
        {
            get { return string.Format("{0}{1}{2}", Parent.CanonicalName, CanonicalNameSeparator, Name); }
        }
    }
}

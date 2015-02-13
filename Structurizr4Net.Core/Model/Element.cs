using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Structurizr4Net.Core.Model
{
    public abstract class Element
    {
        // TODO: what happens if an element name includes a forward slash character?
        public const string CanonicalNameSeparator = "/";

        protected string InternalId = string.Empty;
        protected string InternalName;
        protected string InternalDescription;
        protected ISet<string> InternalTags = new SortedSet<string>();
        protected ISet<Relationship> InternalRelationships = new SortedSet<Relationship>();
        
        [JsonIgnore]
        public Model Model { get; protected internal set; }

        public string Id
        {
            get { return InternalId; }
            internal set { InternalId = value; }
        }

        public virtual string Name
        {
            get { return InternalName; }
            set { InternalName = value; }
        }

        public string Description
        {
            get { return InternalDescription; }
            set { InternalDescription = value; }
        }

        public string Tags
        {
            get
            {
                if (!InternalTags.Any()) return string.Empty;

                var tagBuffer = new StringBuilder();
                foreach (var tag in InternalTags)
                    tagBuffer.AppendFormat("{0},", tag);

                var tagsAsString = tagBuffer.ToString();
                return tagsAsString.Substring(0, tagsAsString.Length - 1);
            }
            internal set
            {
                if (value == null) return;
                
                InternalTags.Clear();
                InternalTags.UnionWith(value.Split(Convert.ToChar(",")));
            }
        }

        public ISet<Relationship> Relationships
        {
            get { return new SortedSet<Relationship>(InternalRelationships); }
        }

        public void AddTags(string tag)
        {
            AddTags(new [] { tag });
        }

        public void AddTags(IEnumerable<string> tags)
        {
            if (tags == null) return;

            foreach (var tag in tags.Where(x => x != null))
                InternalTags.Add(tag);
        }

        internal bool Has(Relationship relationship)
        {
            return InternalRelationships.Contains(relationship);
        }

        internal void AddRelationship(Relationship relationship)
        {
            InternalRelationships.Add(relationship);
        }

        public override string ToString()
        {
            return string.Format("{{{0} | {1} | {2}}}", Id, Name, Description);
        }

        public abstract ElementType Type { get; }

        [JsonIgnore]
        public abstract string CanonicalName { get; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is Element)) return false;

            var element = (Element) obj;
            return CanonicalName.Equals(element.CanonicalName);
        }

        public override int GetHashCode()
        {
            return CanonicalName.GetHashCode();
        }
    }
}

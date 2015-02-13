using Newtonsoft.Json;

namespace Structurizr4Net.Core.Model
{
    public class Relationship
    {
        protected string InteralId = string.Empty;
        private string _sourceId;
        private string _destinationId;

        internal Relationship() { }

        internal Relationship(Element source, Element destination, string description)
        {
            Source = source;
            Destination = destination;
            Description = description;
        }

        public string Id { get; internal set; }
        public string Description { get; set; }

        public Element Source { get; set; }

        [JsonIgnore]
        public Element Destination { get; internal set; }

        public string SourceId
        {
            get { return Source != null ? Source.Id : _sourceId; }
            internal set { _sourceId = value; }
        }
       
        public string DestinationId
        {
            get { return Destination != null ? Destination.Id : _destinationId; }
            internal set { _destinationId = value; }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            var that = (Relationship) obj;
            return Description.Equals(that.Description) && DestinationId.Equals(that.DestinationId) && SourceId.Equals(that.SourceId);
        }

        public override int GetHashCode()
        {
            var result = SourceId.GetHashCode();
            result = 31 * result + DestinationId.GetHashCode();
            result = 31 * result + Description.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0} ---[{1}]---> {2}", Source, Description, Destination);
        }
    }
}

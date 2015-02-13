namespace Structurizr4Net.Core.Model
{
    public interface IIdGenerator
    {
        string GenerateId(Element element);
        string GenerateId(Relationship relationship);
        void Found(string id);
    }
}

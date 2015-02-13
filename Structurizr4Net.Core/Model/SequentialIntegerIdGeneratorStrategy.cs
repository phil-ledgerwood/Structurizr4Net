using System.Globalization;
using System.Runtime.CompilerServices;

namespace Structurizr4Net.Core.Model
{
    public class SequentialIntegerIdGeneratorStrategy : IIdGenerator
    {
        private int _id = 0;

        public void Found(string id)
        {
            var idAsInt = int.Parse(id);
            if (idAsInt > _id) _id = idAsInt;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GenerateId(Element element)
        {
            return (++_id).ToString(CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GenerateId(Relationship relationship)
        {
            return (++_id).ToString(CultureInfo.InvariantCulture);
        }
    }
}

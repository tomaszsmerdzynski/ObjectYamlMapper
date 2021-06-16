using System.IO;

namespace ObjectYamlMapper.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);
    }
}

using ProtoBuf;

namespace Redis.Models;

[ProtoContract]
public struct Data
{
    [ProtoMember(1)]
    public int Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; }

    public Data()
    {
    }

    public Data(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

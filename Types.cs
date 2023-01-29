using ProtoBuf;

namespace StackOverflowException
{
    [ProtoContract]
    public class Recursive
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public Recursive Child { get; set; }
    }
}

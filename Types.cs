using Bond;
using MessagePack;
using ProtoBuf;

namespace StackOverflowException
{
    [ProtoContract]
    public class Recursive
    {
        [ProtoMember(1)] public string Id { get; set; }
        [ProtoMember(2)] public Recursive Child { get; set; }
    }

    [Schema]
    public class Struct
    {
        [Id(0)] public uint Value;
        [Id(1), Bond.Type(typeof(Bond.Tag.nullable<Struct>))] public Struct Child;
    }

    public class Foo
    {
        public Foo A;
    }

    [MessagePackObject]
    public class Message
    {
        [Key(0)] public int Key { get; set; }

        [Key(1)] public Message Value { get; set; }
    }
}

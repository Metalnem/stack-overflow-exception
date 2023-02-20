# StackOverflowException

Examples of **StackOverflowException** triggered by malicious data. Vulnerable libraries:

- [protobuf-net 3.0.131](https://www.nuget.org/packages/protobuf-net/3.0.131)
- [FlatSharp 6.2.1](https://www.nuget.org/packages/FlatSharp/6.2.1)
- [Bond.CSharp 10.0.0](https://www.nuget.org/packages/Bond.CSharp/10.0.0)
- [Newtonsoft.Json 12.0.3](https://www.nuget.org/packages/Newtonsoft.Json/12.0.3)
- [MessagePack 2.1.80](https://www.nuget.org/packages/MessagePack/2.1.80)
- [System.Xml.XmlSerializer 7.0.0](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-7.0)

Read my blog post to learn more: [How safe are .NET serialization libraries against StackOverflowException](https://mijailovic.net/2023/02/20/stack-overflow-exception/).

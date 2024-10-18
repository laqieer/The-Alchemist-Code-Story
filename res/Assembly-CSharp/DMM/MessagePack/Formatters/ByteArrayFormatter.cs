// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ByteArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ByteArrayFormatter : IMessagePackFormatter<byte[]>, IMessagePackFormatter
  {
    public static readonly ByteArrayFormatter Instance = new ByteArrayFormatter();

    private ByteArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      byte[] value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteBytes(ref bytes, offset, value);
    }

    public byte[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadBytes(bytes, offset, out readSize);
    }
  }
}

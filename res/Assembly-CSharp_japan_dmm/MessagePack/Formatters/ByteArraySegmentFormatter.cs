// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ByteArraySegmentFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ByteArraySegmentFormatter : 
    IMessagePackFormatter<ArraySegment<byte>>,
    IMessagePackFormatter
  {
    public static readonly ByteArraySegmentFormatter Instance = new ByteArraySegmentFormatter();

    private ByteArraySegmentFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ArraySegment<byte> value,
      IFormatterResolver formatterResolver)
    {
      return value.Array == null ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteBytes(ref bytes, offset, value.Array, value.Offset, value.Count);
    }

    public ArraySegment<byte> Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return new ArraySegment<byte>();
      }
      byte[] array = MessagePackBinary.ReadBytes(bytes, offset, out readSize);
      return new ArraySegment<byte>(array, 0, array.Length);
    }
  }
}

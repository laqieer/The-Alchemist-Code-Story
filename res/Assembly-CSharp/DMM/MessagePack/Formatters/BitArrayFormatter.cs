// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.BitArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class BitArrayFormatter : IMessagePackFormatter<BitArray>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<BitArray> Instance = (IMessagePackFormatter<BitArray>) new BitArrayFormatter();

    private BitArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BitArray value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      int length = value.Length;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, length);
      for (int index = 0; index < length; ++index)
        offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Get(index));
      return offset - num;
    }

    public BitArray Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BitArray) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      BitArray bitArray = new BitArray(length);
      for (int index = 0; index < length; ++index)
      {
        bitArray[index] = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return bitArray;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.OByteFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class OByteFormatter : IMessagePackFormatter<OByte>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public OByteFormatter()
    {
      this.____keyMapping = new AutomataDictionary();
      this.____stringByteKeys = new byte[0][];
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      OByte value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 0);
      return offset - num;
    }

    public OByte Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        readSize = this.____keyMapping.TryGetValueSafe(key, out int _) ? MessagePackBinary.ReadNextBlock(bytes, offset) : MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new OByte();
    }
  }
}

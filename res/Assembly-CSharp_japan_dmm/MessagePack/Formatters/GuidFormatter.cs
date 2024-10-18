// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.GuidFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class GuidFormatter : IMessagePackFormatter<Guid>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<Guid> Instance = (IMessagePackFormatter<Guid>) new GuidFormatter();

    private GuidFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Guid value,
      IFormatterResolver formatterResolver)
    {
      MessagePackBinary.EnsureCapacity(ref bytes, offset, 38);
      bytes[offset] = (byte) 217;
      bytes[offset + 1] = (byte) 36;
      new GuidBits(ref value).Write(bytes, offset + 2);
      return 38;
    }

    public Guid Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return new GuidBits(MessagePackBinary.ReadStringSegment(bytes, offset, out readSize)).Value;
    }
  }
}

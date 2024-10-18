﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ByteFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ByteFormatter : IMessagePackFormatter<byte>, IMessagePackFormatter
  {
    public static readonly ByteFormatter Instance = new ByteFormatter();

    private ByteFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      byte value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteByte(ref bytes, offset, value);
    }

    public byte Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadByte(bytes, offset, out readSize);
    }
  }
}

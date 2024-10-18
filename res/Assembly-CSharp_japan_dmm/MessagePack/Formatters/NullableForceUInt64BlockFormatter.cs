﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableForceUInt64BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableForceUInt64BlockFormatter : 
    IMessagePackFormatter<ulong?>,
    IMessagePackFormatter
  {
    public static readonly NullableForceUInt64BlockFormatter Instance = new NullableForceUInt64BlockFormatter();

    private NullableForceUInt64BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ulong? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteUInt64ForceUInt64Block(ref bytes, offset, value.Value);
    }

    public ulong? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new ulong?(MessagePackBinary.ReadUInt64(bytes, offset, out readSize));
      readSize = 1;
      return new ulong?();
    }
  }
}

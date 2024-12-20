﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableInt32Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableInt32Formatter : IMessagePackFormatter<int?>, IMessagePackFormatter
  {
    public static readonly NullableInt32Formatter Instance = new NullableInt32Formatter();

    private NullableInt32Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      int? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteInt32(ref bytes, offset, value.Value);
    }

    public int? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new int?(MessagePackBinary.ReadInt32(bytes, offset, out readSize));
      readSize = 1;
      return new int?();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableDateTimeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableDateTimeFormatter : 
    IMessagePackFormatter<DateTime?>,
    IMessagePackFormatter
  {
    public static readonly NullableDateTimeFormatter Instance = new NullableDateTimeFormatter();

    private NullableDateTimeFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DateTime? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteDateTime(ref bytes, offset, value.Value);
    }

    public DateTime? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new DateTime?(MessagePackBinary.ReadDateTime(bytes, offset, out readSize));
      readSize = 1;
      return new DateTime?();
    }
  }
}

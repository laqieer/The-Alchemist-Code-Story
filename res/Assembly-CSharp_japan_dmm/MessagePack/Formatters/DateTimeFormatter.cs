// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DateTimeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DateTimeFormatter : IMessagePackFormatter<DateTime>, IMessagePackFormatter
  {
    public static readonly DateTimeFormatter Instance = new DateTimeFormatter();

    private DateTimeFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DateTime value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteDateTime(ref bytes, offset, value);
    }

    public DateTime Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
    }
  }
}

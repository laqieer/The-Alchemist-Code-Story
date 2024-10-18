// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DateTimeOffsetFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DateTimeOffsetFormatter : 
    IMessagePackFormatter<DateTimeOffset>,
    IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<DateTimeOffset> Instance = (IMessagePackFormatter<DateTimeOffset>) new DateTimeOffsetFormatter();

    private DateTimeOffsetFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DateTimeOffset value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteDateTime(ref bytes, offset, new DateTime(value.Ticks, DateTimeKind.Utc));
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, (short) value.Offset.TotalMinutes);
      return offset - num;
    }

    public DateTimeOffset Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      if (num2 != 2)
        throw new InvalidOperationException("Invalid DateTimeOffset format.");
      DateTime dateTime = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
      offset += readSize;
      short num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
      offset += readSize;
      readSize = offset - num1;
      return new DateTimeOffset(dateTime.Ticks, TimeSpan.FromMinutes((double) num3));
    }
  }
}

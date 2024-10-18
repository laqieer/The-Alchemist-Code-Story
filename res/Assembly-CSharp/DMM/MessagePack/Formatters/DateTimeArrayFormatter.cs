// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DateTimeArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DateTimeArrayFormatter : 
    IMessagePackFormatter<DateTime[]>,
    IMessagePackFormatter
  {
    public static readonly DateTimeArrayFormatter Instance = new DateTimeArrayFormatter();

    private DateTimeArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DateTime[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteDateTime(ref bytes, offset, value[index]);
      return offset - num;
    }

    public DateTime[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (DateTime[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      DateTime[] dateTimeArray = new DateTime[length];
      for (int index = 0; index < dateTimeArray.Length; ++index)
      {
        dateTimeArray[index] = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return dateTimeArray;
    }
  }
}

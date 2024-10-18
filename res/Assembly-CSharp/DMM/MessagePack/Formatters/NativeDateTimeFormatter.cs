// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NativeDateTimeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NativeDateTimeFormatter : 
    IMessagePackFormatter<DateTime>,
    IMessagePackFormatter
  {
    public static readonly NativeDateTimeFormatter Instance = new NativeDateTimeFormatter();

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DateTime value,
      IFormatterResolver formatterResolver)
    {
      long binary = value.ToBinary();
      return MessagePackBinary.WriteInt64(ref bytes, offset, binary);
    }

    public DateTime Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.GetMessagePackType(bytes, offset) == MessagePackType.Extension ? DateTimeFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize) : DateTime.FromBinary(MessagePackBinary.ReadInt64(bytes, offset, out readSize));
    }
  }
}

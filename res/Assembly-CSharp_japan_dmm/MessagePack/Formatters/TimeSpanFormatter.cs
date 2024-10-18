// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TimeSpanFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class TimeSpanFormatter : IMessagePackFormatter<TimeSpan>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<TimeSpan> Instance = (IMessagePackFormatter<TimeSpan>) new TimeSpanFormatter();

    private TimeSpanFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      TimeSpan value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt64(ref bytes, offset, value.Ticks);
    }

    public TimeSpan Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return new TimeSpan(MessagePackBinary.ReadInt64(bytes, offset, out readSize));
    }
  }
}

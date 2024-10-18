// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceInt64BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceInt64BlockFormatter : IMessagePackFormatter<long>, IMessagePackFormatter
  {
    public static readonly ForceInt64BlockFormatter Instance = new ForceInt64BlockFormatter();

    private ForceInt64BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      long value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt64ForceInt64Block(ref bytes, offset, value);
    }

    public long Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadInt64(bytes, offset, out readSize);
    }
  }
}

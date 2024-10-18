// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceUInt64BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceUInt64BlockFormatter : IMessagePackFormatter<ulong>, IMessagePackFormatter
  {
    public static readonly ForceUInt64BlockFormatter Instance = new ForceUInt64BlockFormatter();

    private ForceUInt64BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ulong value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteUInt64ForceUInt64Block(ref bytes, offset, value);
    }

    public ulong Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
    }
  }
}

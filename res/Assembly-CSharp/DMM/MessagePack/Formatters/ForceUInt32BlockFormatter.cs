// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceUInt32BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceUInt32BlockFormatter : IMessagePackFormatter<uint>, IMessagePackFormatter
  {
    public static readonly ForceUInt32BlockFormatter Instance = new ForceUInt32BlockFormatter();

    private ForceUInt32BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      uint value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteUInt32ForceUInt32Block(ref bytes, offset, value);
    }

    public uint Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
    }
  }
}

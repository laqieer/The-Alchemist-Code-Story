// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceInt16BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceInt16BlockFormatter : IMessagePackFormatter<short>, IMessagePackFormatter
  {
    public static readonly ForceInt16BlockFormatter Instance = new ForceInt16BlockFormatter();

    private ForceInt16BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      short value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt16ForceInt16Block(ref bytes, offset, value);
    }

    public short Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadInt16(bytes, offset, out readSize);
    }
  }
}

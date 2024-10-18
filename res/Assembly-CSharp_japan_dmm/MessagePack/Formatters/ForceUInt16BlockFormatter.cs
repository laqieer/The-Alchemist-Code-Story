// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceUInt16BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceUInt16BlockFormatter : 
    IMessagePackFormatter<ushort>,
    IMessagePackFormatter
  {
    public static readonly ForceUInt16BlockFormatter Instance = new ForceUInt16BlockFormatter();

    private ForceUInt16BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ushort value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteUInt16ForceUInt16Block(ref bytes, offset, value);
    }

    public ushort Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadUInt16(bytes, offset, out readSize);
    }
  }
}

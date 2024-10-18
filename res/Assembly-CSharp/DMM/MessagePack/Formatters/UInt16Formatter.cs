// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UInt16Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class UInt16Formatter : IMessagePackFormatter<ushort>, IMessagePackFormatter
  {
    public static readonly UInt16Formatter Instance = new UInt16Formatter();

    private UInt16Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ushort value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteUInt16(ref bytes, offset, value);
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

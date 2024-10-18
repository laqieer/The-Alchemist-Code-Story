// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Int16Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class Int16Formatter : IMessagePackFormatter<short>, IMessagePackFormatter
  {
    public static readonly Int16Formatter Instance = new Int16Formatter();

    private Int16Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      short value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt16(ref bytes, offset, value);
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

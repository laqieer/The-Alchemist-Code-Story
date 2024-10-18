// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Int32Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class Int32Formatter : IMessagePackFormatter<int>, IMessagePackFormatter
  {
    public static readonly Int32Formatter Instance = new Int32Formatter();

    private Int32Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      int value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt32(ref bytes, offset, value);
    }

    public int Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadInt32(bytes, offset, out readSize);
    }
  }
}

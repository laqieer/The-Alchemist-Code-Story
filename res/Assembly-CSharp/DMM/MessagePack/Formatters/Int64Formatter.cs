// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.Int64Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class Int64Formatter : IMessagePackFormatter<long>, IMessagePackFormatter
  {
    public static readonly Int64Formatter Instance = new Int64Formatter();

    private Int64Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      long value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteInt64(ref bytes, offset, value);
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

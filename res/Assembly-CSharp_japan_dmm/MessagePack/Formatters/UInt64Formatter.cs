// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.UInt64Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class UInt64Formatter : IMessagePackFormatter<ulong>, IMessagePackFormatter
  {
    public static readonly UInt64Formatter Instance = new UInt64Formatter();

    private UInt64Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ulong value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteUInt64(ref bytes, offset, value);
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

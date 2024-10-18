// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableUInt64Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableUInt64Formatter : IMessagePackFormatter<ulong?>, IMessagePackFormatter
  {
    public static readonly NullableUInt64Formatter Instance = new NullableUInt64Formatter();

    private NullableUInt64Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ulong? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteUInt64(ref bytes, offset, value.Value);
    }

    public ulong? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new ulong?(MessagePackBinary.ReadUInt64(bytes, offset, out readSize));
      readSize = 1;
      return new ulong?();
    }
  }
}

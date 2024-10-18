// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableUInt32Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableUInt32Formatter : IMessagePackFormatter<uint?>, IMessagePackFormatter
  {
    public static readonly NullableUInt32Formatter Instance = new NullableUInt32Formatter();

    private NullableUInt32Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      uint? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteUInt32(ref bytes, offset, value.Value);
    }

    public uint? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new uint?(MessagePackBinary.ReadUInt32(bytes, offset, out readSize));
      readSize = 1;
      return new uint?();
    }
  }
}

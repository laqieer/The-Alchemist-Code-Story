// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableUInt16Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableUInt16Formatter : IMessagePackFormatter<ushort?>, IMessagePackFormatter
  {
    public static readonly NullableUInt16Formatter Instance = new NullableUInt16Formatter();

    private NullableUInt16Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ushort? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteUInt16(ref bytes, offset, value.Value);
    }

    public ushort? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new ushort?(MessagePackBinary.ReadUInt16(bytes, offset, out readSize));
      readSize = 1;
      return new ushort?();
    }
  }
}

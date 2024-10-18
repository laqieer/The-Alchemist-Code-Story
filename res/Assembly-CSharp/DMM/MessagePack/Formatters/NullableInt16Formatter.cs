// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableInt16Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableInt16Formatter : IMessagePackFormatter<short?>, IMessagePackFormatter
  {
    public static readonly NullableInt16Formatter Instance = new NullableInt16Formatter();

    private NullableInt16Formatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      short? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteInt16(ref bytes, offset, value.Value);
    }

    public short? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new short?(MessagePackBinary.ReadInt16(bytes, offset, out readSize));
      readSize = 1;
      return new short?();
    }
  }
}

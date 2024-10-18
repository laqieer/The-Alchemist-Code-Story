// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableByteFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableByteFormatter : IMessagePackFormatter<byte?>, IMessagePackFormatter
  {
    public static readonly NullableByteFormatter Instance = new NullableByteFormatter();

    private NullableByteFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      byte? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteByte(ref bytes, offset, value.Value);
    }

    public byte? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new byte?(MessagePackBinary.ReadByte(bytes, offset, out readSize));
      readSize = 1;
      return new byte?();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableForceByteBlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableForceByteBlockFormatter : 
    IMessagePackFormatter<byte?>,
    IMessagePackFormatter
  {
    public static readonly NullableForceByteBlockFormatter Instance = new NullableForceByteBlockFormatter();

    private NullableForceByteBlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      byte? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteByteForceByteBlock(ref bytes, offset, value.Value);
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

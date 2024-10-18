// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableForceInt64BlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableForceInt64BlockFormatter : 
    IMessagePackFormatter<long?>,
    IMessagePackFormatter
  {
    public static readonly NullableForceInt64BlockFormatter Instance = new NullableForceInt64BlockFormatter();

    private NullableForceInt64BlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      long? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteInt64ForceInt64Block(ref bytes, offset, value.Value);
    }

    public long? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new long?(MessagePackBinary.ReadInt64(bytes, offset, out readSize));
      readSize = 1;
      return new long?();
    }
  }
}

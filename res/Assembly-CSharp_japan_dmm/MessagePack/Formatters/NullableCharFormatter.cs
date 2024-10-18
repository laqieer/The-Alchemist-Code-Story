// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableCharFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableCharFormatter : IMessagePackFormatter<char?>, IMessagePackFormatter
  {
    public static readonly NullableCharFormatter Instance = new NullableCharFormatter();

    private NullableCharFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      char? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteChar(ref bytes, offset, value.Value);
    }

    public char? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new char?(MessagePackBinary.ReadChar(bytes, offset, out readSize));
      readSize = 1;
      return new char?();
    }
  }
}

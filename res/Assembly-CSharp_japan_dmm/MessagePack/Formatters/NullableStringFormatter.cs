// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableStringFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableStringFormatter : IMessagePackFormatter<string>, IMessagePackFormatter
  {
    public static readonly NullableStringFormatter Instance = new NullableStringFormatter();

    private NullableStringFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      string value,
      IFormatterResolver typeResolver)
    {
      return MessagePackBinary.WriteString(ref bytes, offset, value);
    }

    public string Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver typeResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadString(bytes, offset, out readSize);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CharFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class CharFormatter : IMessagePackFormatter<char>, IMessagePackFormatter
  {
    public static readonly CharFormatter Instance = new CharFormatter();

    private CharFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      char value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteChar(ref bytes, offset, value);
    }

    public char Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadChar(bytes, offset, out readSize);
    }
  }
}

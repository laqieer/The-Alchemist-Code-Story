// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SingleFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class SingleFormatter : IMessagePackFormatter<float>, IMessagePackFormatter
  {
    public static readonly SingleFormatter Instance = new SingleFormatter();

    private SingleFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      float value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteSingle(ref bytes, offset, value);
    }

    public float Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadSingle(bytes, offset, out readSize);
    }
  }
}

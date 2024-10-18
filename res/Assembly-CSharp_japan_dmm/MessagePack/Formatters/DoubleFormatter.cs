// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.DoubleFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class DoubleFormatter : IMessagePackFormatter<double>, IMessagePackFormatter
  {
    public static readonly DoubleFormatter Instance = new DoubleFormatter();

    private DoubleFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      double value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteDouble(ref bytes, offset, value);
    }

    public double Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadDouble(bytes, offset, out readSize);
    }
  }
}

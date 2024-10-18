// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableDoubleFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableDoubleFormatter : IMessagePackFormatter<double?>, IMessagePackFormatter
  {
    public static readonly NullableDoubleFormatter Instance = new NullableDoubleFormatter();

    private NullableDoubleFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      double? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteDouble(ref bytes, offset, value.Value);
    }

    public double? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new double?(MessagePackBinary.ReadDouble(bytes, offset, out readSize));
      readSize = 1;
      return new double?();
    }
  }
}

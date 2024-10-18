// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableSingleFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableSingleFormatter : IMessagePackFormatter<float?>, IMessagePackFormatter
  {
    public static readonly NullableSingleFormatter Instance = new NullableSingleFormatter();

    private NullableSingleFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      float? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteSingle(ref bytes, offset, value.Value);
    }

    public float? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new float?(MessagePackBinary.ReadSingle(bytes, offset, out readSize));
      readSize = 1;
      return new float?();
    }
  }
}

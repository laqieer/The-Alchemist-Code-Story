// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableForceSByteBlockFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableForceSByteBlockFormatter : 
    IMessagePackFormatter<sbyte?>,
    IMessagePackFormatter
  {
    public static readonly NullableForceSByteBlockFormatter Instance = new NullableForceSByteBlockFormatter();

    private NullableForceSByteBlockFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      sbyte? value,
      IFormatterResolver formatterResolver)
    {
      return !value.HasValue ? MessagePackBinary.WriteNil(ref bytes, offset) : MessagePackBinary.WriteSByteForceSByteBlock(ref bytes, offset, value.Value);
    }

    public sbyte? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (!MessagePackBinary.IsNil(bytes, offset))
        return new sbyte?(MessagePackBinary.ReadSByte(bytes, offset, out readSize));
      readSize = 1;
      return new sbyte?();
    }
  }
}

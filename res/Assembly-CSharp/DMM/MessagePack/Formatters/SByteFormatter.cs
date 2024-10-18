// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SByteFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class SByteFormatter : IMessagePackFormatter<sbyte>, IMessagePackFormatter
  {
    public static readonly SByteFormatter Instance = new SByteFormatter();

    private SByteFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      sbyte value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteSByte(ref bytes, offset, value);
    }

    public sbyte Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadSByte(bytes, offset, out readSize);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableNilFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public class NullableNilFormatter : IMessagePackFormatter<Nil?>, IMessagePackFormatter
  {
    public static readonly IMessagePackFormatter<Nil?> Instance = (IMessagePackFormatter<Nil?>) new NullableNilFormatter();

    private NullableNilFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Nil? value,
      IFormatterResolver typeResolver)
    {
      return MessagePackBinary.WriteNil(ref bytes, offset);
    }

    public Nil? Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver typeResolver,
      out int readSize)
    {
      return new Nil?(MessagePackBinary.ReadNil(bytes, offset, out readSize));
    }
  }
}

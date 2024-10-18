// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.BooleanFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class BooleanFormatter : IMessagePackFormatter<bool>, IMessagePackFormatter
  {
    public static readonly BooleanFormatter Instance = new BooleanFormatter();

    private BooleanFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      bool value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteBoolean(ref bytes, offset, value);
    }

    public bool Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      return MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
    }
  }
}

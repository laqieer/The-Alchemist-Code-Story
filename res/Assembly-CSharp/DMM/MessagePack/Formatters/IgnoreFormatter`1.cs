// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.IgnoreFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class IgnoreFormatter<T> : IMessagePackFormatter<T>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      T value,
      IFormatterResolver formatterResolver)
    {
      return MessagePackBinary.WriteNil(ref bytes, offset);
    }

    public T Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
      return default (T);
    }
  }
}

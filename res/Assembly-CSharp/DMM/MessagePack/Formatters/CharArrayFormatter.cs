// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.CharArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class CharArrayFormatter : IMessagePackFormatter<char[]>, IMessagePackFormatter
  {
    public static readonly CharArrayFormatter Instance = new CharArrayFormatter();

    private CharArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      char[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteChar(ref bytes, offset, value[index]);
      return offset - num;
    }

    public char[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (char[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      char[] chArray = new char[length];
      for (int index = 0; index < chArray.Length; ++index)
      {
        chArray[index] = MessagePackBinary.ReadChar(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return chArray;
    }
  }
}

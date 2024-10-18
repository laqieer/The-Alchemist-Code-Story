// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.NullableStringArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class NullableStringArrayFormatter : 
    IMessagePackFormatter<string[]>,
    IMessagePackFormatter
  {
    public static readonly NullableStringArrayFormatter Instance = new NullableStringArrayFormatter();

    private NullableStringArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      string[] value,
      IFormatterResolver typeResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteString(ref bytes, offset, value[index]);
      return offset - num;
    }

    public string[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver typeResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (string[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      string[] strArray = new string[length];
      for (int index = 0; index < strArray.Length; ++index)
      {
        strArray[index] = MessagePackBinary.ReadString(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return strArray;
    }
  }
}

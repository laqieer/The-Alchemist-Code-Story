// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceInt16BlockArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceInt16BlockArrayFormatter : 
    IMessagePackFormatter<short[]>,
    IMessagePackFormatter
  {
    public static readonly ForceInt16BlockArrayFormatter Instance = new ForceInt16BlockArrayFormatter();

    private ForceInt16BlockArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      short[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteInt16ForceInt16Block(ref bytes, offset, value[index]);
      return offset - num;
    }

    public short[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (short[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      short[] numArray = new short[length];
      for (int index = 0; index < numArray.Length; ++index)
      {
        numArray[index] = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return numArray;
    }
  }
}

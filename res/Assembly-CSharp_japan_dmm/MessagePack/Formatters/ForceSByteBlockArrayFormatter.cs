// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ForceSByteBlockArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ForceSByteBlockArrayFormatter : 
    IMessagePackFormatter<sbyte[]>,
    IMessagePackFormatter
  {
    public static readonly ForceSByteBlockArrayFormatter Instance = new ForceSByteBlockArrayFormatter();

    private ForceSByteBlockArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      sbyte[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteSByteForceSByteBlock(ref bytes, offset, value[index]);
      return offset - num;
    }

    public sbyte[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (sbyte[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      sbyte[] numArray = new sbyte[length];
      for (int index = 0; index < numArray.Length; ++index)
      {
        numArray[index] = MessagePackBinary.ReadSByte(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return numArray;
    }
  }
}

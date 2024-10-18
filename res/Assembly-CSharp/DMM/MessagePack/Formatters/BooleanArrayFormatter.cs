// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.BooleanArrayFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class BooleanArrayFormatter : IMessagePackFormatter<bool[]>, IMessagePackFormatter
  {
    public static readonly BooleanArrayFormatter Instance = new BooleanArrayFormatter();

    private BooleanArrayFormatter()
    {
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      bool[] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      for (int index = 0; index < value.Length; ++index)
        offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value[index]);
      return offset - num;
    }

    public bool[] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (bool[]) null;
      }
      int num = offset;
      int length = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      bool[] flagArray = new bool[length];
      for (int index = 0; index < flagArray.Length; ++index)
      {
        flagArray[index] = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
        offset += readSize;
      }
      readSize = offset - num;
      return flagArray;
    }
  }
}

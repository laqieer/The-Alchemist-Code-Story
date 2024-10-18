// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.TwoDimentionalArrayFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class TwoDimentionalArrayFormatter<T> : 
    IMessagePackFormatter<T[,]>,
    IMessagePackFormatter
  {
    private const int ArrayLength = 3;

    public int Serialize(
      ref byte[] bytes,
      int offset,
      T[,] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int length1 = value.GetLength(0);
      int length2 = value.GetLength(1);
      int num = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, length1);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, length2);
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      T[,] objArray = value;
      int length3 = objArray.GetLength(0);
      int length4 = objArray.GetLength(1);
      for (int index1 = 0; index1 < length3; ++index1)
      {
        for (int index2 = 0; index2 < length4; ++index2)
        {
          T obj = objArray[index1, index2];
          offset += formatterWithVerify.Serialize(ref bytes, offset, obj, formatterResolver);
        }
      }
      return offset - num;
    }

    public T[,] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (T[,]) null;
      }
      int num1 = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      if (num2 != 3)
        throw new InvalidOperationException("Invalid T[,] format");
      int length1 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
      offset += readSize;
      int length2 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
      offset += readSize;
      int num3 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      T[,] objArray = new T[length1, length2];
      int index1 = 0;
      int index2 = -1;
      for (int index3 = 0; index3 < num3; ++index3)
      {
        if (index2 < length2 - 1)
        {
          ++index2;
        }
        else
        {
          index2 = 0;
          ++index1;
        }
        objArray[index1, index2] = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
      }
      readSize = offset - num1;
      return objArray;
    }
  }
}

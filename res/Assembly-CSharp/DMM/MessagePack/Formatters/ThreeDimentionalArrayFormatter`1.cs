// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ThreeDimentionalArrayFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ThreeDimentionalArrayFormatter<T> : 
    IMessagePackFormatter<T[,,]>,
    IMessagePackFormatter
  {
    private const int ArrayLength = 4;

    public int Serialize(
      ref byte[] bytes,
      int offset,
      T[,,] value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int length1 = value.GetLength(0);
      int length2 = value.GetLength(1);
      int length3 = value.GetLength(2);
      int num = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, length1);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, length2);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, length3);
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Length);
      T[,,] objArray = value;
      int length4 = objArray.GetLength(0);
      int length5 = objArray.GetLength(1);
      int length6 = objArray.GetLength(2);
      for (int index1 = 0; index1 < length4; ++index1)
      {
        for (int index2 = 0; index2 < length5; ++index2)
        {
          for (int index3 = 0; index3 < length6; ++index3)
          {
            T obj = objArray[index1, index2, index3];
            offset += formatterWithVerify.Serialize(ref bytes, offset, obj, formatterResolver);
          }
        }
      }
      return offset - num;
    }

    public T[,,] Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (T[,,]) null;
      }
      int num1 = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      if (num2 != 4)
        throw new InvalidOperationException("Invalid T[,,] format");
      int length1 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
      offset += readSize;
      int length2 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
      offset += readSize;
      int length3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
      offset += readSize;
      int num3 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      T[,,] objArray = new T[length1, length2, length3];
      int index1 = 0;
      int index2 = 0;
      int index3 = -1;
      for (int index4 = 0; index4 < num3; ++index4)
      {
        if (index3 < length3 - 1)
          ++index3;
        else if (index2 < length2 - 1)
        {
          index3 = 0;
          ++index2;
        }
        else
        {
          index3 = 0;
          index2 = 0;
          ++index1;
        }
        objArray[index1, index2, index3] = formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize);
        offset += readSize;
      }
      readSize = offset - num1;
      return objArray;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ArraySegmentFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ArraySegmentFormatter<T> : 
    IMessagePackFormatter<ArraySegment<T>>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      ArraySegment<T> value,
      IFormatterResolver formatterResolver)
    {
      if (value.Array == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, value.Count);
      T[] array = value.Array;
      for (int index = 0; index < value.Count; ++index)
      {
        T obj = array[value.Offset + index];
        offset += formatterWithVerify.Serialize(ref bytes, offset, obj, formatterResolver);
      }
      return offset - num;
    }

    public ArraySegment<T> Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return new ArraySegment<T>();
      }
      T[] array = formatterResolver.GetFormatterWithVerify<T[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
      return new ArraySegment<T>(array, 0, array.Length);
    }
  }
}

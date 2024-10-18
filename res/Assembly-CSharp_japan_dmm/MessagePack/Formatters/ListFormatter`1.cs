// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.ListFormatter`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class ListFormatter<T> : IMessagePackFormatter<List<T>>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      List<T> value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      int count = value.Count;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, count);
      for (int index = 0; index < count; ++index)
        offset += formatterWithVerify.Serialize(ref bytes, offset, value[index], formatterResolver);
      return offset - num;
    }

    public List<T> Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (List<T>) null;
      }
      int num = offset;
      IMessagePackFormatter<T> formatterWithVerify = formatterResolver.GetFormatterWithVerify<T>();
      int capacity = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      List<T> objList = new List<T>(capacity);
      for (int index = 0; index < capacity; ++index)
      {
        objList.Add(formatterWithVerify.Deserialize(bytes, offset, formatterResolver, out readSize));
        offset += readSize;
      }
      readSize = offset - num;
      return objList;
    }
  }
}

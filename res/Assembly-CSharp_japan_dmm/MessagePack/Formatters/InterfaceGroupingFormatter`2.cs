// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.InterfaceGroupingFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class InterfaceGroupingFormatter<TKey, TElement> : 
    IMessagePackFormatter<IGrouping<TKey, TElement>>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      IGrouping<TKey, TElement> value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref bytes, offset, value.Key, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<IEnumerable<TElement>>().Serialize(ref bytes, offset, (IEnumerable<TElement>) value, formatterResolver);
      return offset - num;
    }

    public IGrouping<TKey, TElement> Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (IGrouping<TKey, TElement>) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      if (num2 != 2)
        throw new InvalidOperationException("Invalid Grouping format.");
      TKey key = formatterResolver.GetFormatterWithVerify<TKey>().Deserialize(bytes, offset, formatterResolver, out readSize);
      offset += readSize;
      IEnumerable<TElement> elements = formatterResolver.GetFormatterWithVerify<IEnumerable<TElement>>().Deserialize(bytes, offset, formatterResolver, out readSize);
      offset += readSize;
      readSize = offset - num1;
      return (IGrouping<TKey, TElement>) new Grouping<TKey, TElement>(key, elements);
    }
  }
}

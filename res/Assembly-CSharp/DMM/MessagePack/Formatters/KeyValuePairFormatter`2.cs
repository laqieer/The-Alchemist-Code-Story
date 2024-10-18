// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.KeyValuePairFormatter`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class KeyValuePairFormatter<TKey, TValue> : 
    IMessagePackFormatter<KeyValuePair<TKey, TValue>>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      KeyValuePair<TKey, TValue> value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref bytes, offset, value.Key, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<TValue>().Serialize(ref bytes, offset, value.Value, formatterResolver);
      return offset - num;
    }

    public KeyValuePair<TKey, TValue> Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      if (num2 != 2)
        throw new InvalidOperationException("Invalid KeyValuePair format.");
      TKey key = formatterResolver.GetFormatterWithVerify<TKey>().Deserialize(bytes, offset, formatterResolver, out readSize);
      offset += readSize;
      TValue obj = formatterResolver.GetFormatterWithVerify<TValue>().Deserialize(bytes, offset, formatterResolver, out readSize);
      offset += readSize;
      readSize = offset - num1;
      return new KeyValuePair<TKey, TValue>(key, obj);
    }
  }
}
